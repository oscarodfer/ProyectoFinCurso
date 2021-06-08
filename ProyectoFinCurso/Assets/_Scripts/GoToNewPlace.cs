using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToNewPlace : MonoBehaviour
{
    public string newPlaceName = "New scene name here";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(newPlaceName);
        }*/
    }
}
