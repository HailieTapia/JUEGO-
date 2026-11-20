using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyMachineBehavior : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;
    public float shootInterval = 3f;
    public float shootDuration = 1f;
    public GameObject greyRayPrefab; // Prefab del rayo gris
    public Transform shootPoint; // Punto de disparo
    private Vector3 startPosition;
    private Animator animator;
    private float shootTimer;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
        shootTimer = shootInterval;
    }

    void Update()
    {
        // Movimiento horizontal
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(movement, 0, 0);

        // Disparo
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            animator.SetBool("IsShooting", true);
            Invoke("Shoot", shootDuration / 2);
            Invoke("StopShooting", shootDuration);
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (greyRayPrefab && shootPoint)
        {
            Instantiate(greyRayPrefab, shootPoint.position, Quaternion.identity);
        }
    }

    void StopShooting()
    {
        animator.SetBool("IsShooting", false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(2); // 2 vidas por contacto
        }
    }
}