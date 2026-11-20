using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public static bool juegoEnPausa = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        // Detectar tecla ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoEnPausa)
            {
                Continuar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Continuar()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Reanudar tiempo
        juegoEnPausa = false;
    }

    public void Pausar()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pausar tiempo
        juegoEnPausa = true;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1f; // Asegurar que el tiempo se reanude
        juegoEnPausa = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CargarMenuPrincipal()
    {
        Time.timeScale = 1f;
        juegoEnPausa = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}