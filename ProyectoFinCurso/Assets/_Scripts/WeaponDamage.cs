using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hace el arma")]
    public int damage;

    public GameObject bloodAnimation;
    private GameObject hitPoint;

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
            int totalDamage = damage + stats.strengthLevels[stats.level];

            if (Random.Range(0, CharacterStats.MAX_STAT_VALUE) < stats.accuracyLevels[stats.level]) 
            {
                totalDamage = 0;
            }

            if (bloodAnimation != null && hitPoint!=null) 
            {
                Instantiate(bloodAnimation, hitPoint.transform.position, hitPoint.transform.rotation);
            }
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(totalDamage);
            
        }
    }
}
