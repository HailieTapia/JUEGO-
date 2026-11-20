using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    public float shootInterval = 3f;
    public GameObject greyBallPrefab;
    public Transform shootPoint;
    private Animator animator;
    private float shootTimer;
    public float detectionRange = 8f;

    private Transform player;
    void Start()
    {
        animator = GetComponent<Animator>();
        shootTimer = shootInterval;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        //  SONIDO SOLO CUANDO ARI CERCA
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
        {
            return; // Ari lejos  silencio
        }

        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            animator.SetTrigger("Shoot");
            Shoot();
            shootTimer = shootInterval;
        }
    }

    void Shoot()
    {
        if (greyBallPrefab && shootPoint)
        {
            Instantiate(greyBallPrefab, shootPoint.position, shootPoint.rotation);
            AudioManager.instance.PlaySound(AudioManager.instance.turretShootSound); 
        }
    }
}