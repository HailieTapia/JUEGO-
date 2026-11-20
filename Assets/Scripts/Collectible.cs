using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType { YellowBall, RedBall, BlueBall, Brush, PaintCan, Roller, Palette, PaintPuddle, ColorLauncher }
    public CollectibleType collectibleType;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            // ANIMACIÓN disappear para bolas de color (Yellow/Red/Blue)
            Animator anim = GetComponent<Animator>();
            if (anim && (collectibleType == CollectibleType.YellowBall ||
                         collectibleType == CollectibleType.RedBall ||
                         collectibleType == CollectibleType.BlueBall))
            {
                anim.SetBool("IsCollected", true);
            }
            //BOLAS COLOR
            if (collectibleType == CollectibleType.YellowBall)
            {
                other.GetComponent<PlayerCollectibles>().AddYellowBall();
                Debug.Log("recogió Bola amarillo, monedas");
                AudioManager.instance.PlaySound(AudioManager.instance.yellowSound);
            }
            else if (collectibleType == CollectibleType.RedBall)
            {
                PlayerHealth health = other.GetComponent<PlayerHealth>();
                if (health.currentHealth < health.maxHealth)
                {
                    health.TakeDamage(-1);
                    Debug.Log("recogió Bola roja, +1 vida");
                } 
                AudioManager.instance.PlaySound(AudioManager.instance.redSound);
            }
            else if (collectibleType == CollectibleType.BlueBall)
            {
                other.GetComponent<PlayerMovement>().ActivateInvincibility(5f);
                Debug.Log("recogió bola azul, INMORTALIDAD");
                AudioManager.instance.PlaySound(AudioManager.instance.blueSound);
            }
            //Otros objetos
            else if (collectibleType == CollectibleType.Brush)
            {
                other.GetComponent<PlayerMovement>().EnableDoubleJump();
                Debug.Log("Ari recogió una Brocha, doble salto activado");
                AudioManager.instance.PlaySound(AudioManager.instance.brushSound);
            }
            else if (collectibleType == CollectibleType.PaintCan)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) < 5f)
                    {
                        Destroy(enemy);
                    }
                }
                Debug.Log("Ari usó un Bote de pintura, enemigos cercanos destruidos");
            }
            else if (collectibleType == CollectibleType.Roller)
            {
                other.GetComponent<PlayerMovement>().ActivateSpeedBoost();
                Debug.Log("Ari recogió un Rodillo, velocidad duplicada");
                AudioManager.instance.PlaySound(AudioManager.instance.rollerSound);
            }
            else if (collectibleType == CollectibleType.Palette)
            {
                other.GetComponent<PlayerCollectibles>().AddPalette();
                Debug.Log("Ari recogió una Paleta de pintura");
                AudioManager.instance.PlaySound(AudioManager.instance.paletteSound);
            }
            else if (collectibleType == CollectibleType.PaintPuddle)
            {
                other.GetComponent<PlayerMovement>().TeleportToPipe();
                Debug.Log("Charco de pintura, teletransportado a tubería jeje");
            }
            else if (collectibleType == CollectibleType.ColorLauncher)
            {
                other.GetComponent<PlayerMovement>().EquipColorLauncher();
                Debug.Log("Ari recogió una Lanzadora de color");
            }


            // Destroy DESPUÉS de animación (1.2s para bolas, inmediato para otros)
            if (collectibleType == CollectibleType.YellowBall ||
                collectibleType == CollectibleType.RedBall ||
                collectibleType == CollectibleType.BlueBall)
            {
                Destroy(gameObject, 1.2f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}