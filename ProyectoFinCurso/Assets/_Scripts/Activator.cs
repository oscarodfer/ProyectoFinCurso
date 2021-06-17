using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public string objectName;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals("Player"))
        {
            GameObject.Find("Grid").transform.Find(objectName).gameObject.SetActive(true);
            GameObject.Find("Grid").transform.Find("Alien Mother").gameObject.SetActive(true);
            FindObjectOfType<AudioManager>().Play("Laser Door");
            FindObjectOfType<AudioManager>().Stop("Level 1");
            FindObjectOfType<AudioManager>().Play("Level 3");
            Destroy(this.gameObject);
        }
    }
}
