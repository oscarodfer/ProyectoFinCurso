using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private List<GameObject> weapons;
    public int activeWeapon;

    private List<GameObject> armors;
    public int activeArmor;

    private List<GameObject> rings;
    public int activeRing1, activeRing2;


    void Start()
    {
        weapons = new List<GameObject>();

        //Haciendo el bucle a transform obtenemos los hijos del gameObject directamente.
        foreach (Transform weapon in transform) 
        {
            weapons.Add(weapon.gameObject);
        }

        //Rellenar con las armaduras del personaje, a futuro.
        armors = new List<GameObject>();
        //Rellenar con los anillos del personaje, a futuro.
        rings = new List<GameObject>();
    }

    private void Update()
    {
        
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
    public List<GameObject> GetAllArmors()
    {
        return armors;
    }
    public List<GameObject> GetAllRings()
    {
        return rings;
    }

    public WeaponDamage GetWeaponAt(int pos) 
    {
        return weapons[pos].GetComponent<WeaponDamage>(); ;
    }

    public WeaponDamage GetArmorAt(int pos)
    {
        return armors[pos].GetComponent<WeaponDamage>(); ;
    }

    public WeaponDamage GetRingAt(int pos)
    {
        return rings[pos].GetComponent<WeaponDamage>(); ;
    }

    public void ActivateWeapon (int i)
    {
        weapons[i].gameObject.GetComponent<WeaponDamage>().isInInventory = true;
    }
}
