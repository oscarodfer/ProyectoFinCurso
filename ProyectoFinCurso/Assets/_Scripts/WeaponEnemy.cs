using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemy : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn;
    public float delay;
    public float fireRate;
    
    void Start()
    {
        //Repetimos el método Fire.
        InvokeRepeating("Fire", delay, fireRate);
    }

    
    void Update()
    {
        
    }

    void Fire() 
    {
        //Instanciamos el gameObject, su posición y rotación.
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }
}
