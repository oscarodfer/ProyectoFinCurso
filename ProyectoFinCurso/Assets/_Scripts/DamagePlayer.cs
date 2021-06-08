using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    [Tooltip("Tiempo que tarda el jugador en poder revivir")]
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;
    private GameObject thePlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            //Desactivamos al jugador
            collision.gameObject.SetActive(false);
            //Le indicamos que está reviviendo
            playerReviving = true;
            //iniciamos el contador de revivir
            timeRevivalCounter = timeToRevivePlayer;
            //Guardamos una referencia
            thePlayer = collision.gameObject;
        }
    }

    void Update()
    {
        if (playerReviving) 
        {
            //Cuentra atrás para revivir.
            timeRevivalCounter -= Time.deltaTime;
            //Si la cuenta atrás ha finalizado
            if (timeRevivalCounter < 0) 
            {
                //Le indicamos que ha revivido
                playerReviving = false;
                //Reactivamos al jugador.
                thePlayer.SetActive(true);
            }
        }
    }
}
