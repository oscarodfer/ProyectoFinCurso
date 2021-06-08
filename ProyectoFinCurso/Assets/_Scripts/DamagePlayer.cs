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
    private CharacterStats stats;

    private void Start()
    {
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            int totalDamage = Mathf.Clamp(damage / stats.defenseLevels[stats.level], CharacterStats.MIN_HEALTH_VALUE, CharacterStats.MAX_HEALTH_VALUE);

            if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) < stats.luckLevels[stats.level]) {
                totalDamage = 0;
            }
    
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
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
