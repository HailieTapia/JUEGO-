using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Textos del HUD")]
    public TextMeshProUGUI monedasText;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI paletasText;

    [Header("Valores iniciales")]
    public int monedasInicial = 0;
    public int vidasInicial = 3;
    public int paletasInicial = 0;

    private int monedasActual;
    private int vidasActual;
    private int paletasActual;

    void Start()
    {
        // Inicializar valores
        monedasActual = monedasInicial;
        vidasActual = vidasInicial;
        paletasActual = paletasInicial;

        // Actualizar HUD
        UpdateMonedas(monedasActual);
        UpdateVidas(vidasActual);
        UpdatePaletas(paletasActual);
    }

    // Actualizar monedas
    public void UpdateMonedas(int cantidad)
    {
        monedasActual = cantidad;
        if (monedasText != null)
            monedasText.text = monedasActual.ToString();
    }

    // Actualizar vidas
    public void UpdateVidas(int cantidad)
    {
        vidasActual = cantidad;
        if (vidasText != null)
            vidasText.text = vidasActual.ToString();
    }

    // Actualizar paletas
    public void UpdatePaletas(int cantidad)
    {
        paletasActual = cantidad;
        if (paletasText != null)
            paletasText.text = paletasActual.ToString();
    }

    // Métodos para agregar items
    public void AddMoneda()
    {
        monedasActual++;
        UpdateMonedas(monedasActual);
    }

    public void AddVida()
    {
        vidasActual++;
        UpdateVidas(vidasActual);
    }

    public void AddPaleta()
    {
        paletasActual++;
        UpdatePaletas(paletasActual);
    }

    // Métodos para quitar items
    public void QuitarVida()
    {
        vidasActual--;
        if (vidasActual < 0) vidasActual = 0;
        UpdateVidas(vidasActual);
    }
}