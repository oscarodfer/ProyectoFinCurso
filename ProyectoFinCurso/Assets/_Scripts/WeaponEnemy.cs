using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
{
    public GameObject shot;
    public Transform shotSpawn;
    public float delay = 1.0f;
    public float fireRate = 0.5f;
    public bool characterVision = false;
    
    void Start()
    {

    }

    
    void Update()
    {
        
    }

    void Fire() 
    {
            //Instanciamos el gameObject, su posición y rotación.
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            InvokeRepeating("Fire", delay, fireRate);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            CancelInvoke();
        }
        
    }

   
}
