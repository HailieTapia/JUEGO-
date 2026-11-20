using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int maxHealth = 3;
    public int currentHealth;
    [Header("Muerte")]
    public float deathDelay = 1.5f;
    private Animator animator;


    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateUIVidas();
        Debug.Log("Vidas iniciales de Ari: " + currentHealth);
    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0 || GetComponent<PlayerMovement>().isInvincible) return;
        currentHealth -= damage;
        UpdateUIVidas();

        // CAMBIO 1: Hurt + SONIDO
        animator.SetBool("IsHurt", true);
        AudioManager.instance.PlaySound(AudioManager.instance.hurtSound);  //  SONIDO FALTABA
        Invoke("EndHurt", 0.33f);

        Debug.Log("Ari recibió daño, vidas restantes: " + currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateUIVidas();
            DieEnemy();
        }
    }

    void EndHurt()
    {
        animator.SetBool("IsHurt", false);
    }

    public void GetHurt(int damage)
    {
        TakeDamage(damage);
    }
    public void DieEnemy()
    {
        Debug.Log("Ari murió por enemigo");
        animator.SetTrigger("DieEnemy");  //  TRIGGER, no Bool
        AudioManager.instance.PlaySound(AudioManager.instance.dieSound);
        Invoke("RestartLevel", deathDelay);
    }

    public void DieFall()
    {
        Debug.Log("Ari cayó al vacío");
        animator.SetTrigger("DieFall");   //  TRIGGER, no Bool
        AudioManager.instance.PlaySound(AudioManager.instance.dieSound);
        Invoke("RestartLevel", deathDelay);
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUIVidas()
    {
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdateVidas(currentHealth);
        }
    }
}