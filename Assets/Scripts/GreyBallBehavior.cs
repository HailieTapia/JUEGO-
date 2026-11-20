using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyBallBehavior : MonoBehaviour
{
    public float speed = 4f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().SlowDown();
            Destroy(gameObject);
        }
    }
}