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
                    break;
                }
            }
            if (itemsNeeded.Count == 0) 
            {
                questManager.itemCollected = null;
                CompleteQuest();
            }
        }
    }
}
