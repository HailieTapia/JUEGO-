using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    [Header("Daño")]
    public int damage = 1;

    [Header("Efecto especial")]
    public bool trapsPlayer = false;
    public float trapDuration = 2f;

    [Header("Sonido")]
    public float detectionRange = 6f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // SONIDO SOLO ARI CERCA (IGUAL TURRET)
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= detectionRange)
        {
            // Reproduce sonido cada 0.5s (zumbido constante)
            if (Time.time % 0.5f < Time.deltaTime)
            {
                AudioManager.instance.PlaySound(AudioManager.instance.obstacleSound);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }

            PlayerMovement playerMove = other.GetComponent<PlayerMovement>();
            if (trapsPlayer && playerMove != null)
            {
                playerMove.Trap(trapDuration);
            }
        }
    }
}