using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private List<GameObject> weapons;
    public int activeWeapon;

    
    void Start()
    {
        weapons = new List<GameObject>();

        //Haciendo el bucle a transform obtenemos los hijos del gameObject directamente.
        foreach (Transform weapon in transform) 
        {
            weapons.Add(weapon.gameObject);
        }

        for (int i = 0; i < weapons.Count; i++) 
        {
            weapons[i].SetActive(i==activeWeapon);
        }
    }

    public void ChangeWeapon(int newWeapon) 
    {
        weapons[activeWeapon].SetActive(false);
        weapons[newWeapon].SetActive(true);
        activeWeapon = newWeapon;
    }

    public List<GameObject> GetAllWeapons()
    {
        return weapons;
    }

    public WeaponDamage GetWeaponAt(int pos) 
    {
        return weapons[pos].GetComponent<WeaponDamage>(); ;
    }
}
