using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Método para el botón Jugar
    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel1");
    }
    // Método para el botón Manual
    public void OpenManual()
    {
        SceneManager.LoadScene("Manual");
    }

    // Método para el botón Créditos
    public void OpenCreditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    // Método para el botón Salir
    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");

        // Si está en el editor de Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}