using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hace el arma")]
    public int damage;

    public GameObject bloodAnimation;
    private GameObject hitPoint;
    public GameObject canvasDamage;

    private CharacterStats stats;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
        stats = GameObject.Find("Player").GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            CharacterStats enemyStats = collision.gameObject.GetComponent<CharacterStats>();
            float plaFac = (1 + stats.strengthLevels[stats.level] / CharacterStats.MAX_STAT_VALUE);
            float eneFac = (1 - enemyStats.defenseLevels[enemyStats.level] / CharacterStats.MAX_STAT_VALUE);
            int totalDamage = (int)(damage * eneFac* plaFac);

            if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) < stats.accuracyLevels[stats.level]) 
            {
                if (Random.Range(0,CharacterStats.MAX_STAT_VALUE) < enemyStats.luckLevels[enemyStats.level]) 
                {
                    totalDamage = 0;
                }
                else 
                { 
                    totalDamage *= 5;
                }
            }

            if (bloodAnimation != null && hitPoint!=null) 
            {
                Destroy(Instantiate(bloodAnimation, hitPoint.transform.position, hitPoint.transform.rotation),0.5f);
            }

            var clone = (GameObject)Instantiate(canvasDamage, hitPoint.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoint = damage;

            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            
        }
    }
}
