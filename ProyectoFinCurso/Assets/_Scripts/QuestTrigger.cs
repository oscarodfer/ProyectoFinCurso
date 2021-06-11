using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    private QuestManager questManager;
    public int questID;
    public bool startPoint, endPoint;
    private bool playerInZone;
    public bool automaticCatch;

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    private void Update()
    {
        if (playerInZone)
        {
            //activación automática o pulsado de tecla.
            if (automaticCatch || (!automaticCatch && Input.GetKeyDown(KeyCode.E))) 
            { 
                //Qué Q es?
                Quest q = questManager.QuestWithID(questID);
                if (q == null) 
                {
                    Debug.LogErrorFormat("La misión con ID {0} no existe", questID);
                    return;
                }
                //Si entramos aquí, la misión existe.
                if (!q.questCompleted) //Si borro esta línea la misión sera repetible.
                {
                    //Zona de inicio misión
                    if (startPoint) 
                    {
                        if (!q.gameObject.activeInHierarchy) 
                        {
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    //Zona fin misión
                    if (endPoint) 
                    {
                        if (q.gameObject.activeInHierarchy) 
                        {
                            q.CompleteQuest();
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            playerInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInZone = false;
        }
    }
}
