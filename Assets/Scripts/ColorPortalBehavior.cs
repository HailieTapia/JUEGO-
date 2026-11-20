using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPortalBehavior : MonoBehaviour
{
    public Transform destination; // Punto de teletransporte
    public int requiredPalettes = 5;
    private bool playerOnPortal = false;
    private GameObject player;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPortal = true;
            player = other.gameObject;
            Debug.Log("Jugador sobre el portal - Presiona S para entrar");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPortal = false;
            player = null;
            Debug.Log("Jugador salió del portal");
        }
    }

    void Update()
    {
        // Verificar si el jugador está sobre el portal y presiona S
        if (playerOnPortal && player != null)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                PlayerCollectibles collectibles = player.GetComponent<PlayerCollectibles>();
                if (collectibles != null && collectibles.palettes >= requiredPalettes)
                {
                    player.transform.position = destination.position;
                    Debug.Log("Teletransportado por Portal de colores");
                    playerOnPortal = false;
                    player = null;
                }
                else
                {
                    Debug.Log("Necesitas " + requiredPalettes + " paletas para usar el portal");
                }
            }
        }
    }
}