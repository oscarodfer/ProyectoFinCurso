using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    public string[] npcDialogueLines;
    public Sprite npcSprite;
    public GameObject weaponQuest;

    private DialogueManager dialogueManager;
    private bool playerInTheZone;

    
    void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    
    void Update()
    {
        if (playerInTheZone && Input.GetKeyDown(KeyCode.E))
        {
            string[] finalDialogue = new string[npcDialogueLines.Length];
            int i = 0;
            foreach (string line in npcDialogueLines) 
            {
                finalDialogue[i++] = ((npcName != null) ? npcName + "\n" : "") + line;
            }

            if (npcSprite != null)
            {
                dialogueManager.ShowDialogue(finalDialogue, npcSprite);
            }
            else 
            {
                dialogueManager.ShowDialogue(finalDialogue);
            }
            if (gameObject.GetComponentInParent<NPCMovement>() != null) 
            {
                gameObject.GetComponentInParent<NPCMovement>().isTalking = true;
            }
            if (npcName == "Capitan Bartholomew") 
            {
                GameObject.Find("Weapon").GetComponent<WeaponManager>().ActivateWeapon(0);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player")) 
        {
            playerInTheZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            playerInTheZone = false;
        }
    }
}
