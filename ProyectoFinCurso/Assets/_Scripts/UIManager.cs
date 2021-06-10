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
    public GameObject invetoryPanel;
    public Button inventoryButton;



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
        //Llamamos al manager.
        WeaponManager manager = FindObjectOfType<WeaponManager>();
        //Cargamos todas las armas que tenemos en el manager
        List<GameObject> weapons = manager.GetAllWeapons();
        int i = 0;
        //Hacemos un bucle para cada arma.
        foreach (GameObject w in weapons) 
        {
            //Instanciamos el boton procedente del prefab dentro del inventario, como hijo del inventario.
            Button tempB = Instantiate(inventoryButton, invetoryPanel.transform);
            //Incovamos al temporallyButton para cambiar el tipo y el index, así copiamos el valor de la i.
            tempB.GetComponent<InventoryButton>().type = InventoryButton.ItemType.WEAPON;
            tempB.GetComponent<InventoryButton>().itemIndex = i;
            //Añadimos Onclick para llamar al changeweapon para cambiar de arma.
            tempB.onClick.AddListener(()=> tempB.GetComponent<InventoryButton>().ActivateButton());
            //Cambiamos la imagen al botón.
            tempB.image.sprite = w.GetComponent<SpriteRenderer>().sprite;
            //Incrementamos el valor de la i
            i++;
        }
    }
}
