using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn;
    public float delay = 2.0f;
    public float fireRate = 2.0f;
    public bool characterVision = false;
    
    void Start()
    {

    }

    
    void Update()
    {
        
    }

    void Fire() 
    {
            //Instanciamos el gameObject, su posici�n y rotaci�n.
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            characterVision = true;
            if(characterVision == true) 
            {
                InvokeRepeating("Fire", delay, fireRate);
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        characterVision = false;
        CancelInvoke();
    }

   
}
