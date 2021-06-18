using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public string oldMusic = "Old music here";
    public string newMusic = "New music here";
    public string newPlaceName = "New scene name here";
    public bool needsAction = false;
    public string destinationUuid;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (transform.childCount > 1)
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }      
        }
        TeleportByTag(collision.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            if (transform.childCount > 1)
            {
                gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        TeleportByTag(collision.gameObject.tag);
    }

    private void TeleportByTag(string collisionTag)
    {
        if (collisionTag == "Player")
        {
            if (!needsAction || (needsAction && Input.GetKey(KeyCode.E)))
            {
                FindObjectOfType<AudioManager>().Stop(oldMusic);
                FindObjectOfType<AudioManager>().Play(newMusic);

                FindObjectOfType<PlayerController>().nextUuid = this.destinationUuid;
                StartCoroutine(LoadLevel());       
            }
        }    
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        FindObjectOfType<AudioManager>().Play("TransitionSound");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(newPlaceName);
    }
}
