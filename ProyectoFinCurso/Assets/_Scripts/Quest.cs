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

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    public void StartQuest() 
    {
        questManager.ShowQuestText(title + "\n" + startText);
    }

    public void CompleteQuest() 
    {
        questManager.ShowQuestText(title + "\n" + completeText);
        questCompleted = true;
        gameObject.SetActive(false);
    }

}
