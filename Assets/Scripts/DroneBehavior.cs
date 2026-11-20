using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehavior : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 3f;
    public float shootInterval = 4f;
    public float shootDuration = 1f;
    public GameObject greyRayPrefab;
    public Transform shootPoint;
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
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(movement, 0, 0);

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
            other.GetComponent<PlayerHealth>().TakeDamage(1);
        }
    }
}