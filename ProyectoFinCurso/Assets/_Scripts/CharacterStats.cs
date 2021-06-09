using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

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
    [Tooltip("Defensa que divide al da�o del enemigo")]
    public int[] defenseLevels;
    [Tooltip("Velocidad de ataque del jugador")]
    public int[] speedLevels; //Primero hay que implementar la velocidad de ataque en el player controller.
    [Tooltip("Probabilidad de que el enemigo falle el ataque")]
    public int[] luckLevels;
    [Tooltip("Probabilidad de que el personaje falle el ataque")]
    public int[] accuracyLevels;

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
    }

    public void AddExperience(int exp) 
    {
        this.exp += exp;

        if (level >= expToLevelUp.Length)
        {
            return;
        }
        if (this.exp - expToLevelUp[level] >= expToLevelUp[level])
        {
            level++;
            healthManager.UpdateMaxHealth(hpLevels[level]);
            /* Hay que implementar el attackTime en el playerController.
            playerController.attackTime -= speedLevels[level]/MAX_STAT_VALUE;
            */
        }
    }
}
