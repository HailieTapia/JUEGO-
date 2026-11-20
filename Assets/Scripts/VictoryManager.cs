using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    [Header("Siguiente Nivel")]
    public string nextLevelName = "Nivel2";

    [Header("Delay en segundos")]
    public float delay = 2f;

    private bool triggered = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        // SONIDO DE VICTORIA (usando SOLO AudioManager)
        AudioManager.instance.PlaySound(AudioManager.instance.victorySound);

        // Opcional: animación de victoria de Ari
        other.GetComponent<Animator>()?.SetTrigger("Victory");

        Invoke("LoadNextLevel", delay);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}