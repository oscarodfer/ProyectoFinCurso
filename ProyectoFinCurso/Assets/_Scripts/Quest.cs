using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public int questID;
    public bool questCompleted;
    private QuestManager questManager;
    public string title;
    public string startText;
    public string completeText;
    public bool needsItem;
    public List<QuestItem> itemsNeeded;
    public bool killsEnemy;
    public List<QuestEnemy> enemies;
    public List<int> numberOfEnemies;

    public void StartQuest() 
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + startText);
    }

    public void CompleteQuest() 
    {
        questManager = FindObjectOfType<QuestManager>();
        questManager.ShowQuestText(title + "\n" + completeText);
        questCompleted = true;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (needsItem && questManager.itemCollected!=null) 
        {
            for (int i = 0; i < itemsNeeded.Count; i++) 
            {
                if (itemsNeeded[i].itemName == questManager.itemCollected.itemName) 
                {
                    itemsNeeded.RemoveAt(i);
                    questManager.itemCollected = null;
                    break;
                }
            }
            if (itemsNeeded.Count == 0) 
            {
                CompleteQuest();
            }
        }

        if (killsEnemy && questManager.enemyKilled != null) 
        {
            for (int i = 0; i < enemies.Count;i++) 
            {
                if (enemies[i].enemyName == questManager.enemyKilled.enemyName) 
                {
                    numberOfEnemies[i]--;
                    questManager.enemyKilled= null;
                    if (numberOfEnemies[i] <= 0) 
                    {
                        enemies.RemoveAt(i);
                        numberOfEnemies.RemoveAt(i);
                    }
                    break;
                }
            }
            if (enemies.Count == 0) 
            {
                CompleteQuest();
            }
        }
    }
}
