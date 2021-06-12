using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    public enum ItemType { WEAPON = 0, ITEM = 1, ARMOR = 2, RING = 3, SPECIAL_ITEMS = 4 };
    
    public int itemIndex;
    public ItemType type;


    public void ActivateButton() 
    {
        switch (type) 
        {
            case ItemType.WEAPON:
                FindObjectOfType<WeaponManager>().ChangeWeapon(itemIndex);
                FindObjectOfType<UIManager>().inventoryText.text = FindObjectOfType<WeaponManager>().GetWeaponAt(itemIndex).weaponName;

                break;

            case ItemType.ITEM:
                Debug.Log("En el futuro...");
                break;

            case ItemType.RING:
                Debug.Log("En el futuro...");
                break;

            case ItemType.ARMOR:
                Debug.Log("En el futuro...");
                break;

            case ItemType.SPECIAL_ITEMS:
               QuestItem item = FindObjectOfType<ItemsManager>().GetItemAt(itemIndex);
                FindObjectOfType<UIManager>().inventoryText.text = (item.itemName);
                break;
        }
    }
}
