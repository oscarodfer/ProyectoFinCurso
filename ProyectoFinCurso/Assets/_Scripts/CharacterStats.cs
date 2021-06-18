using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    public GameObject levelUpText;
    public GameObject sparksAnimation;
    public const int MAX_STAT_VALUE = 100;
    public const int MAX_HEALTH_VALUE = 9999;
    public const int MIN_HEALTH_VALUE = 1;
    [Tooltip("Nivel del jugador")]
    public int level;
    [Tooltip("Experiencia del jugador")]
    public int exp;
    [Tooltip("Niveles del jugador")]
    public int[] expToLevelUp;
    [Tooltip("Niveles de vida del jugador")]
    public int[] hpLevels;
    [Tooltip("Fuerza que se suma a la del arma")]
    public int[] strengthLevels;
    [Tooltip("Defensa que divide al daño del enemigo")]
    public int[] defenseLevels;
    [Tooltip("Velocidad de ataque del jugador")]
    public int[] speedLevels; //Primero hay que implementar la velocidad de ataque en el player controller.
    [Tooltip("Probabilidad de que el enemigo falle el ataque")]
    public int[] luckLevels;
    [Tooltip("Probabilidad de que el personaje falle el ataque")]
    public int[] accuracyLevels;

    public int totalScore;

    private HealthManager healthManager;
    private PlayerController playerController;

    void Start()
    {
        healthManager = GetComponent<HealthManager>();
        playerController = GetComponent<PlayerController>();
        healthManager.UpdateMaxHealth(hpLevels[level]);
        if (gameObject.tag.Equals("Enemy")) 
        {
            EnemyBehaviour controller = GetComponent<EnemyBehaviour>();
            controller.speed += speedLevels[level] / MAX_STAT_VALUE;
        }
        totalScore = 0;
    }

    public void AddExperience(int exp) 
    {
        this.exp += exp;

        if (level >= expToLevelUp.Length)
        {
            return;
        }
        if (this.exp >= expToLevelUp[level])
        {
            level++;

            if(levelUpText)
            {
                LevelUp();
            }
            
            FindObjectOfType<AudioManager>().Play("Level Up");
            healthManager.UpdateMaxHealth(hpLevels[level]);
            /* Hay que implementar el attackTime en el playerController.
            playerController.attackTime -= speedLevels[level]/MAX_STAT_VALUE;
            */
        }
    }

    public void LevelUp ()
    {
        Destroy(Instantiate(sparksAnimation, transform.position, Quaternion.identity), 0.5f);
        Vector2 newPosition = transform.position;
        newPosition.y += 1f;
        Instantiate(levelUpText, newPosition, Quaternion.identity, transform);
    }

    public void AddScore(int score)
    {
        totalScore += score;
    }
}
