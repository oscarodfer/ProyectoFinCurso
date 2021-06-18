using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int scoreOnDeath;

    public int maxHealth;
    [SerializeField]
    private int currentHealth;
    public int Health
    {
        get 
        {
            return currentHealth;
        }
    }

    public bool flashActive;
    private Animator _animator;
    public float flashLength;
    private float flashCounter;
    private SpriteRenderer _characterRenderer;
    private Color characterColor;
    public Animation punkDeathAnimation;

    public int expWhenDefeated;
    private QuestEnemy quest;
    private QuestManager questManager;

    public bool isInmune = false;

    void Start()
    {
        _characterRenderer = GetComponent<SpriteRenderer>();
        UpdateMaxHealth(maxHealth);
        currentHealth = maxHealth;
        quest = GetComponent<QuestEnemy>();
        questManager = FindObjectOfType<QuestManager>();
        _animator = this.GetComponent<Animator>();
        characterColor = _characterRenderer.color;
    }

    public void DamageCharacter(int damage) 
    {
        if(isInmune)
        {
            return;
        }
        else
        {
            if(damage != 1)
            currentHealth -= damage;
        } 

        if (currentHealth <= 0) 
        {
            if (gameObject.GetComponent<EnemyBehaviour>())
            {
                this.gameObject.GetComponent<EnemyBehaviour>().SetDead(true);

                if (gameObject.GetComponent<EnemyBehaviour>().isBoss)
                {
                    FindObjectOfType<AudioManager>().Stop("Level 3");
                    FindObjectOfType<AudioManager>().Play("Mission Complete");
                    GameObject.Find("Status").GetComponent<GameStatus>().GameOver();
                }
            }
            
            if (gameObject.GetComponent<PlayerController>())
            {
                //GameObject.Find("Status").GetComponent<GameStatus>().GameOver();
                gameObject.GetComponent<PlayerController>().isDead = true;
            }

            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;

            if (gameObject.tag.Equals("Enemy") || gameObject.tag.Equals("EnemyRanged")) 
            {
                GameObject.Find("Player").GetComponent<CharacterStats>().AddExperience(expWhenDefeated);
                GameObject.Find("Player").GetComponent<CharacterStats>().AddScore(scoreOnDeath);
                questManager.enemyKilled = quest;           
            }
            //this.gameObject.SetActive(false);    
        }
        if (flashLength > 0) 
        {
            flashActive = true;
            flashCounter = flashLength;
        }
    }

    public void UpdateMaxHealth(int newMaxHealth) 
    {
        int offsetHealth = newMaxHealth - maxHealth;
        maxHealth = newMaxHealth;
        currentHealth += offsetHealth;
    }

    public void Heal() 
    {
        currentHealth = maxHealth;
    }

    void ToggleColor(bool visible) 
    {
        _characterRenderer.color = new Color(255, 0, 0, (visible ? 1 : 0));
    }

    private void FixedUpdate()
    {
        if (this.currentHealth <= 0)
        {
            if (gameObject.GetComponent<EnemyBehaviour>())
            {
                this.gameObject.GetComponent<EnemyBehaviour>().SetDead(true);
            }

            if (gameObject.GetComponent<PlayerController>())
            {
                //GameObject.Find("Status").GetComponent<GameStatus>().GameOver();
                gameObject.GetComponent<PlayerController>().isDead = true;
            }
        }

        if (flashActive)
        {
            isInmune = true;
            ToggleColor(true);
            flashCounter -= Time.fixedDeltaTime;

            if (flashCounter < flashLength && flashCounter >= flashLength * 0.8f)
            {
                ToggleColor(true);
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
                _characterRenderer.color = characterColor;
                flashActive = false;
                GameObject.Find("Player").GetComponent<PlayerController>().canMove = true;
                GameObject.Find("Player").GetComponent<PlayerController>().isDamaged = false;
                isInmune = false;

                if(this.gameObject.GetComponent<EnemyBehaviour>())
                {
                    if(this.gameObject.GetComponent<EnemyBehaviour>().isBoss){
                        this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
                    }
                }
            }
        }
        else
        {
            isInmune = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
        }
    }

}
