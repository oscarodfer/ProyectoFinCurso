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
            //activaci�n autom�tica o pulsado de tecla.
            if (automaticCatch || (!automaticCatch && Input.GetKeyDown(KeyCode.E))) 
            { 
                //Qu� Q es?
                Quest q = questManager.QuestWithID(questID);
                if (q == null) 
                {
                    Debug.LogErrorFormat("La misi�n con ID {0} no existe", questID);
                    return;
                }
                //Si entramos aqu�, la misi�n existe.
                if (!q.questCompleted) //Si borro esta l�nea la misi�n sera repetible.
                {
                    //Zona de inicio misi�n
                    if (startPoint) 
                    {
                        if (!q.gameObject.activeInHierarchy) 
                        {
                            q.gameObject.SetActive(true);
                            q.StartQuest();
                        }
                    }
                    //Zona fin misi�n
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
