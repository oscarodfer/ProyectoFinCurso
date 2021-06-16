using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hace el arma")]
    public int damage;
    public string weaponName;
    public GameObject bloodAnimation;
    private GameObject hitPoint;
    public GameObject canvasDamage;

    private CharacterStats stats;

    public float critDamage;
    public float critChance;
    public int weaponAccuracy;
    public bool isCritical = false;
    public bool isInInventory = false;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.tag.Equals("Enemy") || collision.gameObject.tag.Equals("EnemyRanged")) && GameObject.Find("Player").GetComponent<PlayerController>().isAttacking && GameObject.Find("Player").GetComponent<PlayerController>().isFirstAttack)
        {
            isCritical = false;
            EnemyBehaviour enemy = collision.gameObject.GetComponent<EnemyBehaviour>();
            enemy.GetStunned();
            GameObject.Find("Player").GetComponent<PlayerController>().isFirstAttack = false;
            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();
            float plaFac = (1 + stats.strengthLevels[stats.level]);
            float eneFac = (1 - enemyStats.defenseLevels[enemyStats.level]);
            int totalDamage = (int)(damage + plaFac  - eneFac);

            if (Random.Range(0, 100) < (stats.accuracyLevels[stats.level] + weaponAccuracy - enemyStats.luckLevels[enemyStats.level])) 
            {
                if (Random.Range(0, 100) < ( stats.luckLevels[stats.level] + critChance))
                {
                    //Crítico
                    float totalCriticalDamage = totalDamage;
                    totalCriticalDamage *= critDamage;
                    totalDamage = (int)totalCriticalDamage;
                    isCritical = true;
                }
            }
            else
            {
                totalDamage = 0;
            }

            if (bloodAnimation != null && hitPoint!=null) 
            {
                Destroy(Instantiate(bloodAnimation, hitPoint.transform.position, hitPoint.transform.rotation),0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoint = totalDamage;
            clone.GetComponent<DamageNumber>().isCritical = isCritical;
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);            
        }
    }
}
