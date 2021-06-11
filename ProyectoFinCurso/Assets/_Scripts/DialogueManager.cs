using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public Image avatarImage;
    public bool dialogueActive;
    private string namePlayer = "Archibald";
    private string startDialogue = "Por fin en casa!! Que ganas tenia de ir al casino!!\n Presiento que hoy ganare mucho dinero...";

    public string[] dialogueLines;
    public int currentDialogueLine;

    private PlayerController playerController;

    void Start()
    {
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = namePlayer + "\n"+ startDialogue;
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (dialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            currentDialogueLine++;

            if (currentDialogueLine >= dialogueLines.Length)
            {
                playerController.isTalking = false;
                currentDialogueLine = 0;
                dialogueActive = false;
                avatarImage.enabled = false;
                dialogueBox.SetActive(false);
            }
            else 
            {
                dialogueText.text = dialogueLines[currentDialogueLine];
            }
        }

    }

    public void ShowDialogue(string[] lines) 
    {
        currentDialogueLine = 0;
        dialogueLines = lines;
        dialogueActive = true;
        dialogueBox.SetActive(true);
        dialogueText.text = dialogueLines[currentDialogueLine];
        playerController.isTalking = true;
    }

    public void ShowDialogue(string[] lines, Sprite sprite) 
    {
        ShowDialogue(lines);
        avatarImage.enabled = true;
        avatarImage.sprite = sprite;
    }
}
