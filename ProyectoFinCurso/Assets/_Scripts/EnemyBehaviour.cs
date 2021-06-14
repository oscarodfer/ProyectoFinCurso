using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
public class EnemyBehaviour : MonoBehaviour
{
    private const string AXIS_H = "Horizontal";
    private const string AXIS_V = "Vertical";
    private const string LAST_H = "LastH";
    private const string LAST_V = "LastV";
    private const string IS_WALKING = "IsWalking";

    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1.5f;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    [Tooltip("Dirección en la que se mueve el enemigo, se genera aleatoriamente")]
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public int currentDirection;

    private GameObject player;
    private Vector2 movement;
    private Vector2 lastMovement;
    private bool chasing = false;
    private bool isWalking = false;

    void Start()
    {
        StarWalking();
        _rb = this.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        lastMovement = Vector2.zero;
        isWalking = false;
        //timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        //timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        if (!isWalking)
        {
            _rb.velocity = Vector2.zero;
        }

        if (chasing)
        {
            isWalking = true;
            _rb.velocity = player.GetComponent<Transform>().position - this.transform.position * speed;
            ChasePlayer(_rb.velocity);
        }
        else
        {
            if (isWalking)
            {
                _rb.velocity = walkingDirections[currentDirection] * speed;
                timeBetweenStepsCounter -= Time.fixedDeltaTime;
                if (timeBetweenStepsCounter < 0)
                {
                    StopWalking();
                }
            }
            else
            {
                //_rigidbody.velocity = Vector2.zero;
                timeToMakeStepCounter -= Time.fixedDeltaTime;

                if (timeToMakeStepCounter < 0)
                {
                    StarWalking();
                }
            }
        }

        _animator.SetBool(IS_WALKING, isWalking);
        _animator.SetFloat(AXIS_H, _rb.velocity.x);
        _animator.SetFloat(AXIS_V, _rb.velocity.y);
        _animator.SetFloat(LAST_H, this.lastMovement.x);
        _animator.SetFloat(LAST_V, this.lastMovement.y);
    }

    public void StarWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, walkingDirections.Length); 
        timeBetweenStepsCounter = timeBetweenSteps;
    }

    public void ChasePlayer(Vector2 direction)
    {
        _rb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
    }

    public void StopWalking()
    {
        isWalking = false;
        timeToMakeStepCounter = timeToMakeStep;
        //_rigidbody.velocity = Vector2.zero;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            chasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            chasing = false;
        }
    }

    public Vector2 GetLastMovement()
    {
        return this.lastMovement;
    }

    public void SetLastMovement(Vector2 v2)
    {
        this.lastMovement = v2;
    }
}
