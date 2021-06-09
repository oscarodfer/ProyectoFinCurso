using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1.5f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool isWalking = false;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    [Tooltip("Dirección en la que se mueve el enemigo, se genera aleatoriamente")]
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int currentDirection;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
    }


    private void FixedUpdate()
    {
        if (isWalking)
        {
            _rigidbody.velocity = walkingDirections[currentDirection] * speed;
            timeBetweenStepsCounter -= Time.fixedDeltaTime;
            if (timeBetweenStepsCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            timeToMakeStepCounter -= Time.fixedDeltaTime;
            if (timeToMakeStepCounter < 0)
            {
                StarWalking();
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
    }

    public void StarWalking()
    {

        currentDirection = Random.Range(0, walkingDirections.Length);
        isWalking = true;
        timeBetweenStepsCounter = timeBetweenSteps;
    }

    public void StopWalking()
    {
        isWalking = false;
        timeToMakeStepCounter = timeToMakeStep;
        _rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Jugador detectado - Voy a por ti!!!");
            
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Debug.Log("Jugador fuera de rango, me vuelvo a patrullar");
        }
    }
}
