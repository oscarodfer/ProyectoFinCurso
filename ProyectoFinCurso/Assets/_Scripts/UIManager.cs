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
    public TMP_Text playerLevelText;
    public HealthManager playerHealthManager;
    public CharacterStats playerStats;



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
    }
}
