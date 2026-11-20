using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Power-ups")]
    public float doubleJumpDuration = 10f;
    public float speedBoostDuration = 5f;
    public float invincibilityDuration = 5f;

    [Header("Disparo")]
    public GameObject colorShotPrefab;
    public Transform shootPoint;

    [Header("Teletransporte")]
    public Transform pipeDestination;

    // Componentes
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private PlayerHealth health;

    // Estado
    private bool facingRight = true;
    private bool isGrounded;
    private bool canDoubleJump;
    private float doubleJumpTimer;
    private float speedBoostTimer;
    public bool isInvincible { get; private set; }
    private bool hasColorLauncher;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        health = GetComponent<PlayerHealth>();
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
        HandleAttack();
        HandlePowerUps();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float currentSpeed = CalculateSpeed();
        rb.velocity = new Vector2(moveInput * currentSpeed, rb.velocity.y);

        if (moveInput != 0)
            FlipSprite(moveInput);
    }

    float CalculateSpeed()
    {
        float speed = moveSpeed;
        if (speedBoostTimer > 0) speed *= 2f;
        return speed;
    }

    void FlipSprite(float moveInput)
    {
        if (moveInput > 0 && !facingRight) Flip();
        else if (moveInput < 0 && facingRight) Flip();
    }

    void Flip()
    {
        facingRight = !facingRight;
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump && doubleJumpTimer > 0)
            {
                Jump();
                canDoubleJump = false;
            }
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isGrounded = false;
        animator.SetBool("IsJumping", true);
        AudioManager.instance.PlaySound(AudioManager.instance.jumpSound);
    }

    void HandleCrouch()
    {
        bool crouching = Input.GetKey(KeyCode.S);
        animator.SetBool("IsCrouching", crouching);
    }

    void HandleAttack()
    {
        if (Input.GetKeyDown(KeyCode.F) && hasColorLauncher && colorShotPrefab && shootPoint)
        {
            animator.SetBool("IsAttacking", true);  // ? BOOL, no Trigger
            AudioManager.instance.PlaySound(AudioManager.instance.attackSound);  // ? DISPARO
            Invoke("EndAttack", 0.33f); // Duración animación Attack
        }
    }
    void EndAttack()
    {
        animator.SetBool("IsAttacking", false);  // ? Reset BOOL
    }
    void HandlePowerUps()
    {
        // Speed Boost
        if (speedBoostTimer > 0)
            speedBoostTimer -= Time.deltaTime;

        // Double Jump
        if (doubleJumpTimer > 0)
        {
            doubleJumpTimer -= Time.deltaTime;
            canDoubleJump = true;
        }
        else
        {
            canDoubleJump = false;
        }

        // Inmortalidad parpadeo
        if (isInvincible)
        {
            spriteRenderer.enabled = Mathf.Sin(Time.time * 10f) > 0;
        }
    }

    void UpdateAnimations()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        float speed = Mathf.Abs(moveInput);
        animator.SetFloat("Speed", speed);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("IsJumping", false);
        }
        else if (collision.gameObject.CompareTag("Enemy") && !isInvincible)
        {
            health.GetHurt(1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.CompareTag("Projectile") && !isInvincible)
        {
            health.GetHurt(2);
            Destroy(other.gameObject);
        }
    }

    // === POWER-UPS PÚBLICOS ===
    public void EnableDoubleJump() => doubleJumpTimer = doubleJumpDuration;
    public void ActivateSpeedBoost() => speedBoostTimer = speedBoostDuration;

    public void ActivateInvincibility(float duration)
    {
        isInvincible = true;
        Invoke("EndInvincibility", duration);
    }

    void EndInvincibility()
    {
        isInvincible = false;
        spriteRenderer.enabled = true;
    }

    public void EquipColorLauncher()
    {
        hasColorLauncher = true;
        Invoke("RemoveColorLauncher", 10f);
    }

    void RemoveColorLauncher() => hasColorLauncher = false;

    public void TeleportToPipe()
    {
        if (pipeDestination)
            transform.position = pipeDestination.position;
    }

    public void Trap(float duration)
    {
        moveSpeed = 0f;
        Invoke("ReleaseTrap", duration);
    }

    void ReleaseTrap() => moveSpeed = 5f;

    public void SlowDown()
    {
        moveSpeed *= 0.5f;
        Invoke("RestoreSpeed", 3f);
    }

    void RestoreSpeed() => moveSpeed = 5f;

    public void ResetPowerUps()
    {
        doubleJumpTimer = 0f;
        speedBoostTimer = 0f;
        isInvincible = false;
        hasColorLauncher = false;
        spriteRenderer.enabled = true;
        animator.SetBool("IsJumping", false);
        animator.SetBool("IsCrouching", false);
        Debug.Log("Power-ups reseteados");
    }
}