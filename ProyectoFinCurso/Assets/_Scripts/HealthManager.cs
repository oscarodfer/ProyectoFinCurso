using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int Health
    {
        get {
            return currentHealth;
        }
    }

    public bool flashActive;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;
    public GameObject shot;

    public int expWhenDefeated;

    public bool inmune = false;

    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
        shot = GameObject.Find("Enemy3");
    }

    public void DamageCharacter(int damage) 
    {
        
        if(inmune)
        {
            return;
        }

        if(!inmune)
        {
            if(damage != 1)
            currentHealth -= damage;
        } 

        if (currentHealth <= 0) 
        {
            inmune = true;
            if (gameObject.tag.Equals("Enemy")) 
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);

                if (gameObject.name.Equals("Enemy3") && currentHealth <= 0) 
                {
                    Destroy(shot);
                }
            }
            gameObject.SetActive(false);
        }
        if (flashLength > 0) 
        {
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth) 
    {
        maxHealth = newMaxHealth;
        currentHealth = maxHealth;
    }

    void ToggleColor(bool visible) 
    {
        _characterRenderer.color = new Color(255, 0, 0, (visible ? 1 : 0));
    }

    private void FixedUpdate()
    {
        if (flashActive)
        {
            inmune = true;
            ToggleColor(true);
            flashCounter -= Time.fixedDeltaTime;

            if (flashCounter < flashLength && flashCounter >= flashLength * 0.8f)
            {
                ToggleColor(true);

                PlayerController player = GameObject.Find("Player").GetComponent<PlayerController>();
                Vector2 lastPlayerMovement = player.lastMovement;
                Vector3 currentPosition = player.transform.position;
                Vector3 endPosition = currentPosition;

                if (lastPlayerMovement.x == 1.0f && lastPlayerMovement.y == 0.0f)
                {
                    endPosition.x = endPosition.x - 0.05f;
                }
                else if (lastPlayerMovement.x == -1.0f && lastPlayerMovement.y == 0.0f)
                {
                    endPosition.x = endPosition.x + 0.05f;
                }
                else if (lastPlayerMovement.x == -0.0f && lastPlayerMovement.y == 1.0f)
                {
                    endPosition.y = endPosition.y - 0.05f;
                }
                else if (lastPlayerMovement.x == 0.0f && lastPlayerMovement.y == -1.0f)
                {
                    endPosition.y = endPosition.y + 0.05f;
                }

                GameObject.Find("Player").GetComponent<PlayerController>().transform.position = Vector3.Lerp(currentPosition, endPosition, 30.0f);
            }
            else if (flashCounter < flashLength * 0.8f && flashCounter >= flashLength * 0.6f)
            {
                ToggleColor(false);
            }
            else if (flashCounter < flashLength * 0.6f && flashCounter >= flashLength * 0.4f)
            {
                ToggleColor(true);
            }
            else if (flashCounter < flashLength * 0.4f && flashCounter >= flashLength * 0.2f)
            {
                ToggleColor(false);
                GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
                GameObject.Find("Player").GetComponent<PlayerController>().isDamaged = false;
            }
            else if (flashCounter < flashLength * 0.2f && flashCounter > 0.0f)
            {
                ToggleColor(true);
            }
            else
            {
                _characterRenderer.color = new Color(255, 255, 255, 1);
                flashActive = false;
                GetComponent<BoxCollider2D>().enabled = true;
                GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
                GameObject.Find("Player").GetComponent<PlayerController>().isDamaged = false;
                inmune = false;
            }
        } 
    }
}
