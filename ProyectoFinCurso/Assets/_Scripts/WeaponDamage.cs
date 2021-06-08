using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [Tooltip("Cantidad de daño que hace el arma")]
    public int damage;

    public GameObject bloodAnimation;
    private GameObject hitPoint;

    private void Start()
    {
        hitPoint = transform.Find("Hit Point").gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy")) 
        {
            if (bloodAnimation != null && hitPoint!=null) 
            {
                Instantiate(bloodAnimation, hitPoint.transform.position, hitPoint.transform.rotation);
            }
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            
        }
    }
}
