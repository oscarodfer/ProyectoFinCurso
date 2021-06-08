using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
   /* [Tooltip("Tiempo que tarda el jugador en poder revivir")]
    public float timeToRevivePlayer;
    private float timeRevivalCounter;
    private bool playerReviving;
    private GameObject thePlayer;
   */
    [Tooltip("Daño que hace el enemigo")]
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
        }
    }

   /* void Update()
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
   */
}
