using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New scene name here";
    public bool needsAction = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(!needsAction)
            {
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (needsAction && Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(newPlaceName);
            }
        }
    }
}
