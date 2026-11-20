using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private bool infiniteHorizontal = true;

    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    private float spriteWidth;
    private float startPositionX;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPositionX = transform.position.x;
    }

    void LateUpdate()
    {
        Vector3 cameraPosition = cameraTransform.position;
        float deltaX = (cameraPosition.x - previousCameraPosition.x) * parallaxMultiplier;

        transform.Translate(new Vector3(deltaX, 0, 0));
        previousCameraPosition = cameraPosition;

        if (infiniteHorizontal)
        {
            // Cálculo de reposición infinita mejorado
            float parallaxOffset = (cameraPosition.x - startPositionX) * (1 - parallaxMultiplier);
            float relativeCameraDistance = cameraPosition.x - transform.position.x;

            if (relativeCameraDistance > spriteWidth * 0.8f)
            {
                transform.Translate(new Vector3(spriteWidth, 0, 0));
            }
            else if (relativeCameraDistance < -spriteWidth * 0.8f)
            {
                transform.Translate(new Vector3(-spriteWidth, 0, 0));
            }
        }
    }
}