using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> quests;
    private DialogueManager dialogueManager;


    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();

        foreach (Transform t in transform)
        {
            //Añadimos todas las misiones a la lista.
            quests.Add(t.gameObject.GetComponent<Quest>());
        }
    }

    public void ShowQuestText(string questText) 
    {
        dialogueManager.ShowDialogue(new string[] { questText });
    }
}
