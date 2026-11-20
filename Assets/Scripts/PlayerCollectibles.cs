using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollectibles : MonoBehaviour
{
    public int yellowBalls = 0;
    public int palettes = 0;

    public void AddYellowBall()
    {
        yellowBalls++;
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdateMonedas(yellowBalls);
        }
        Debug.Log("Bolas amarillas: " + yellowBalls);
    }

    public void AddPalette()
    {
        palettes++;
        UIManager ui = FindObjectOfType<UIManager>();
        if (ui != null)
        {
            ui.UpdatePaletas(palettes);
        }
        Debug.Log("Paletas de pintura: " + palettes);
    }
}