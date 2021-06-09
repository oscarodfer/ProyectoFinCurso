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
        //Repetimos el m�todo Fire.
        InvokeRepeating("Fire", delay, fireRate);
    }

    
    void Update()
    {
        
    }

    void Fire() 
    {
        //Instanciamos el gameObject, su posici�n y rotaci�n.
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
    }
}
