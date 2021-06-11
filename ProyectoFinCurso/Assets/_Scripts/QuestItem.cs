using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestItem : MonoBehaviour
{
    public int questID;
    private QuestManager manager;
    public string itemName;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            manager = FindObjectOfType<QuestManager>();
            Quest q = manager.QuestWithID(questID);
            if (q == null) 
            {
                Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                return;
            }
            if (q.gameObject.activeInHierarchy && !q.questCompleted) 
            {
                manager.itemCollected = this;
                gameObject.SetActive(false);
            }
        }
    }
}
