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
                break;

            case ItemType.ITEM:
                Debug.Log("Consumir item (En el futuro...)");
                break;

            case ItemType.RING:
                Debug.Log("Equipar anillo (En el futuro...)");
                break;

            case ItemType.ARMOR:
                Debug.Log("Cambiar armadura (En el futuro...)");
                break;
        }
        ShowDescription();
    }


    //Añadimos la descripción del objeto en el inventario para cuando pasemos el ratón por encima.
   public void ShowDescription() 
    {
        string description = "";
        switch (type)
        {
            case ItemType.WEAPON:
                description = FindObjectOfType<WeaponManager>().GetWeaponAt(itemIndex).weaponName;
                break;

            case ItemType.ITEM:
                description = "Item consumible";
                break;

            case ItemType.RING:
                description = FindObjectOfType<WeaponManager>().GetRingAt(itemIndex).name;
                break;

            case ItemType.ARMOR:
                description = FindObjectOfType<WeaponManager>().GetArmorAt(itemIndex).name;
                break;

            case ItemType.SPECIAL_ITEMS:
                QuestItem item = FindObjectOfType<ItemsManager>().GetItemAt(itemIndex);
                description = item.itemName;
                break;
        }
        FindObjectOfType<UIManager>().inventoryText.text = description;
    }

    public void ClearDescription() 
    {
        FindObjectOfType<UIManager>().inventoryText.text = "";
    }
}
