using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D jugadorRM;
    // Start is called before the first frame update
    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        jugadorRM = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = (jugadorRM.velocity.x*0.1f)*velocidadMovimiento* Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
