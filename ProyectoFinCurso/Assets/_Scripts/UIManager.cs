using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UIManager : MonoBehaviour
{

    public Slider playerHealthBar;
    public TMP_Text playerHealthText;
    public Slider playerExpBar;
    public TMP_Text playerExpText;
    public TMP_Text playerLevelText;
    public HealthManager playerHealthManager;
    public CharacterStats playerStats;
    public WeaponManager weaponManager;
    public GameObject invetoryPanel, menuPanel;
    public Button inventoryButton;
    private ItemsManager itemsManager;
    public Text inventoryText;

    private void Start()
    {
        weaponManager = FindObjectOfType<WeaponManager>();
        invetoryPanel.SetActive(false);
        menuPanel.SetActive(false);
        itemsManager = FindObjectOfType<ItemsManager>();
    }

    void Update()
    {
        playerHealthBar.maxValue = playerHealthManager.maxHealth;
        playerHealthBar.value = playerHealthManager.Health;

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("HP: ").Append(playerHealthManager.Health).Append(" / ").Append(playerHealthManager.maxHealth);

        playerHealthText.text = stringBuilder.ToString();

        playerLevelText.text = "Nivel: " + playerStats.level;

        if (playerStats.level >= playerStats.expToLevelUp.Length) 
        {
            playerExpBar.enabled = false;
            return;
        }
        playerExpBar.maxValue = playerStats.expToLevelUp[playerStats.level];
        playerExpBar.minValue = playerStats.expToLevelUp[playerStats.level - 1];
        playerExpBar.value = playerStats.exp;
        
        StringBuilder stringBuilder2 = new StringBuilder();
        stringBuilder2.Append("EXP: ").Append(playerStats.exp).Append(" / ").Append(playerStats.expToLevelUp[playerStats.level]);

        playerExpText.text = stringBuilder2.ToString();

        if (Input.GetKeyDown(KeyCode.I)) 
        {
            ToggleInventory();
        }
    }

    //Abrir/cerrar inventario.
    public void ToggleInventory() 
    {
        
        invetoryPanel.SetActive(!invetoryPanel.activeInHierarchy);
        menuPanel.SetActive(!menuPanel.activeInHierarchy);
        inventoryText.text = "";
        
        if (invetoryPanel.activeInHierarchy) 
        {
            //Antes de rellenarlo destuimos todo lo viejo.
            foreach (Transform t in invetoryPanel.transform) 
            {
                Destroy(t.gameObject);
            }
            FillInventory();
        }
    }

    // Rellenar inventario
    public void FillInventory() 
    {
        //Cargamos todas las armas que tenemos en el manager
        List<GameObject> weapons = weaponManager.GetAllWeapons();
        int i = 0;
        //Hacemos un bucle para cada arma.
        foreach (GameObject w in weapons) 
        {
            if (w.gameObject.GetComponent<WeaponDamage>().isInInventory == true)
            {
                AddItemToInventory(w, InventoryButton.ItemType.WEAPON, i);
            }
            i++;
        }
        i = 0;
        List<GameObject> keyItems = itemsManager.GetQuestItems();
        foreach (GameObject item in keyItems) 
        {
            AddItemToInventory(item, InventoryButton.ItemType.SPECIAL_ITEMS, i);
            i++;
        }
    }

    private void AddItemToInventory(GameObject item, InventoryButton.ItemType type, int pos) 
    {
        //Instanciamos el boton procedente del prefab dentro del inventario, como hijo del inventario.
        Button tempB = Instantiate(inventoryButton, invetoryPanel.transform);
        //Incovamos al temporallyButton para cambiar el tipo y el index, así copiamos el valor de la i.
        tempB.GetComponent<InventoryButton>().type = type;
        tempB.GetComponent<InventoryButton>().itemIndex = pos;
        //Añadimos Onclick para llamar al método
        tempB.onClick.AddListener(() => tempB.GetComponent<InventoryButton>().ActivateButton());
        //Cambiamos la imagen al botón.
        tempB.image.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void ShowOnly(int type) 
    {
        foreach (Transform t in invetoryPanel.transform) 
        {
            //Comparamos si el tipo del botón es del tipo que quiero mostrar.
            t.gameObject.SetActive((int)t.GetComponent<InventoryButton>().type == type);
        }
    }

    public void ShowAll() 
    {
        foreach (Transform t in invetoryPanel.transform) 
        {
            t.gameObject.SetActive(true);
        }
    }
}

