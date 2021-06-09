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
    private CharacterStats playerStats;
    private CharacterStats enemyStats;
    public GameObject canvasDamage;

    private void Start()
    {
        playerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        enemyStats = GetComponent<CharacterStats>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            float FacEnemy = 1 + enemyStats.strengthLevels[enemyStats.level] / CharacterStats.MAX_STAT_VALUE;
            float FacPlayer = 1 - playerStats.defenseLevels[playerStats.level] / CharacterStats.MAX_STAT_VALUE;

            int totalDamage = Mathf.Clamp((int)(damage * FacEnemy * FacPlayer), CharacterStats.MIN_HEALTH_VALUE, CharacterStats.MAX_HEALTH_VALUE);

            if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) < playerStats.luckLevels[playerStats.level]) 
            {
                if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) > enemyStats.accuracyLevels[enemyStats.level]) 
                {
                    totalDamage = 0;
                }
            }

            var clone = (GameObject)Instantiate(canvasDamage, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoint = damage;
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
