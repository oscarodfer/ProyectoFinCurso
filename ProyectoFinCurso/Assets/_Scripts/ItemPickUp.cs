using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    private const int SWORD = 0;
    private const int BSWORD = 1;
    private const int LSWORD = 2;
    private const int TRIDENT = 3;
    private const int POLEARM = 4;
    private const int HAMMER = 5;
    private const int AXE = 6;

    public string item;
    private bool destroyAfterInteract = true;
    private bool isActivated = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") && Input.GetKeyDown(KeyCode.E))
        {
            switch (this.gameObject.GetComponent<ItemPickUp>().item)
            {
                case "SWORD":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(SWORD);
                    break;
                case "BSWORD":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(BSWORD);
                    break;
                case "LSWORD":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(LSWORD);
                    break;
                case "TRIDENT":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(TRIDENT);
                    break;
                case "POLEARM":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(POLEARM);
                    break;
                case "HAMMER":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(HAMMER);
                    break;
                case "AXE":
                    GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(AXE);
                    break;
                case "ELEVATOR_DOORS":
                    destroyAfterInteract = false;
                    ElevatorSwitch();
                    break;
                case "ELEVATOR_KEY":
                    destroyAfterInteract = true;
                    GetKey();
                    break;
                default:
                    break;
            }
            
            if (destroyAfterInteract)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void GetKey()
    {

    }

    private void ElevatorSwitch ()
    {
        if(isActivated)
        {
            isActivated = false;         
        }
        else
        {
            isActivated = true;
        }

        FindObjectOfType<AudioManager>().Play("TransitionSound");
        GameObject.Find("Grid").transform.Find("Elevator Doors").gameObject.SetActive(isActivated);
    }
}
