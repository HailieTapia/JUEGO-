using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoParallax : MonoBehaviour
{
    public float parallaxEfecto;
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 movimientoCam = cam.position - lastCamPos;
        transform.position += new Vector3(movimientoCam.x * parallaxEfecto, movimientoCam.y * parallaxEfecto, 0);
        lastCamPos = cam.position;
    }
}
