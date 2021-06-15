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
    private const float STUN_DURATION = 1.5f;

    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    private float timeBetweenSteps = 1.0f;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    private float timeToMakeStep = 2.0f;
    private float timeToMakeStepCounter;

    [Tooltip("Dirección en la que se mueve el enemigo, se genera aleatoriamente")]
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public int currentDirection;

    private Transform player;
    private Vector2 lastMovement;
    private bool isChasing = false;
    private bool isWalking = false;
    private float stunCounter;

    //Para el enemigo ranged
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public GameObject shot;


    void Start()
    {
        StartWalking();
        _rb = this.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastMovement = Vector2.zero;
        isWalking = false;
        isChasing = false;
        stunCounter = STUN_DURATION;
        timeBetweenStepsCounter = timeBetweenSteps;
        timeToMakeStepCounter = timeToMakeStep;
        timeBetweenShots = startTimeBetweenShots;
        //timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        //timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        if (_rb.velocity != Vector2.zero)
        {
            isWalking = true;
        }

        if (stunCounter != STUN_DURATION)
        {
            if (tag.Equals("EnemyRanged"))
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * 3 * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            }
              

            if (stunCounter <= 0.0f)
            {
                stunCounter = STUN_DURATION;
            }
            else
            {
                stunCounter -= Time.deltaTime;
                return;
            }
        }
        else
        {
            if (isChasing)
            {
                isWalking = true;

                if(tag.Equals("EnemyRanged"))
                {
                    if (timeBetweenShots <= 0)
                    {
                        Instantiate(shot, transform.position, Quaternion.identity);
                        timeBetweenShots = startTimeBetweenShots;
                    }
                    else
                    {
                        timeBetweenShots -= Time.deltaTime;
                    }

                    if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 2 * Time.deltaTime);
                    }
                    else if (Vector2.Distance(transform.position, player.position) <= stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                    {
                        transform.position = this.transform.position;
                    }
                    else if (Vector2.Distance(transform.position, player.position) <= retreatDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * 1.5f * Time.deltaTime);
                    }
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 3 * Time.deltaTime);
                }
            }
            else
            {
                timeBetweenStepsCounter -= Time.fixedDeltaTime;

                if (timeBetweenStepsCounter <= 0)
                {
                    StopWalking();
                    _rb.velocity = Vector2.zero;
                }
                else
                {
                    //_rigidbody.velocity = Vector2.zero;
                    timeToMakeStepCounter -= Time.fixedDeltaTime;

                    if (timeToMakeStepCounter < 0)
                    {
                        StartWalking();
                        _rb.velocity = walkingDirections[currentDirection] * speed;
                    }
                }
            }
        }

        if (_rb.velocity.x > 0.0f)
        {
            lastMovement.x = 1.0f;
        }
        else if (_rb.velocity.x < 0.0f)
        {
            lastMovement.x = -1.0f;
        }
        else
        {
            lastMovement.x = 0.0f;
        }

        if (_rb.velocity.y > 0.0f)
        {
            lastMovement.y = 1.0f;
        }
        else if (_rb.velocity.y < 0.0f)
        {
            lastMovement.y = -1.0f;
        }
        else
        {
            lastMovement.y = 0.0f;
        }

        _animator.SetBool(IS_WALKING, isWalking);
        _animator.SetFloat(AXIS_H, walkingDirections[currentDirection].x);
        _animator.SetFloat(AXIS_V, walkingDirections[currentDirection].y);
    }

    public void StartWalking()
    {
        isWalking = true;
        currentDirection = Random.Range(0, walkingDirections.Length); 
        
        timeToMakeStepCounter = timeToMakeStep;
    }

    public void StopWalking()
    {
        isWalking = false;
        timeBetweenStepsCounter = timeBetweenSteps;
        //_rigidbody.velocity = Vector2.zero;
    }

    public void ChasePlayer(Vector2 direction)
    {
        
    }
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            isChasing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isWalking = false;
            isChasing = false;

            GetStunned();
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

    public void GetStunned()
    {
        this.stunCounter -= 0.001f;
    }
}
