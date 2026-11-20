using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }

    [Header("Movimiento")]
    public MoveDirection direction = MoveDirection.Horizontal;
    public float speed = 2f;
    public float distance = 4f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);

        Vector3 newPos = startPos;

        if (direction == MoveDirection.Horizontal)
            newPos.x += offset;
        else
            newPos.y += offset;

        transform.position = newPos;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}