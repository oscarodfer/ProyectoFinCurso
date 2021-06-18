using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager questManager;
    public string itemName;
    private ItemsManager itemManager;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            questManager = FindObjectOfType<QuestManager>();
            itemManager = FindObjectOfType<ItemsManager>();
            Quest q = questManager.QuestWithID(questID);
            if (q == null) 
            {
                Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                return;
            }
            if (q.gameObject.activeInHierarchy && !q.questCompleted) 
            {
                questManager.itemCollected = this;
                itemManager.AddQuestItem(this.gameObject);
                GameObject.Find("Player").GetComponent<PlayerController>().hasKey = true;
                gameObject.SetActive(false);
            }
        }
    }
}
