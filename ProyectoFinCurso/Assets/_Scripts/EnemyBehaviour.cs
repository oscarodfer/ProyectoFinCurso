using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
public class EnemyBehaviour : MonoBehaviour
{
    private const string AXIS_H = "Horizontal";
    private const string AXIS_V = "Vertical";
    private const string IS_WALKING = "IsWalking";
    private const string IS_DEAD = "IsDead";
    private const float STUN_DURATION = 1.5f;

    public bool isBoss;
    private int bulletCount;

    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed;
    private Rigidbody2D _rb;
    private Animator _animator;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    [Tooltip("Direcci?n en la que se mueve el enemigo, se genera aleatoriamente")]
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private Vector2 direction;
    public int currentDirection;

    private Transform player;
    private bool isChasing = false;
    private bool isWalking = false;
    private bool isDead;
    private float stunCounter;

    public GameObject bloodAnimation;

    //Para el enemigo ranged
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public float startMachinegun;
    private float machinegun;
    public int maxNumberOfShots;
    private int numberOfShots;
    public GameObject shot;


    void Start()
    {
        StartWalking();
        _rb = this.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isWalking = false;
        isChasing = false;
        isDead = false;
        stunCounter = STUN_DURATION;
        timeBetweenStepsCounter = timeBetweenSteps;
        timeToMakeStepCounter = timeToMakeStep;
        timeBetweenShots = 0.5f;
        machinegun = 0.5f;
        bulletCount = 0;
        numberOfShots = Random.Range(0, maxNumberOfShots) + 1;
        //timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
        //timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        if (!isDead)
        {
            if (_rb.velocity != Vector2.zero)
            {
                isWalking = true;
            }

            if (stunCounter != STUN_DURATION)
            {
                Retreat();
              
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
                    direction = player.position - transform.position;

                    if (tag.Equals("EnemyRanged"))
                    {
                        if (timeBetweenShots <= 0)
                        {
                            if(!isBoss)
                            {  
                                Instantiate(shot, transform.position, Quaternion.identity);
                                timeBetweenShots = startTimeBetweenShots;
                            }
                            else
                            {
                                if (machinegun <= 0)
                                {
                                    if (bulletCount < numberOfShots)
                                    {
                                        Instantiate(shot, transform.position, Quaternion.identity);
                                        bulletCount++;
                                        machinegun = startMachinegun;
                                        numberOfShots = Random.Range(0, maxNumberOfShots) + 1;
                                    }
                                    else
                                    {
                                        bulletCount = 0;
                                        timeBetweenShots = startTimeBetweenShots;
                                    }
                                }
                                else
                                {
                                    machinegun -= Time.deltaTime;
                                }
                            }
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
                            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                        }
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 2 * Time.deltaTime);
                    }      
                }
                else
                {
                    direction = _rb.velocity;
                    direction.Normalize();

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
        }

        _animator.SetBool(IS_DEAD, isDead);
        _animator.SetBool(IS_WALKING, isWalking);
        _animator.SetFloat(AXIS_H, direction.x);
        _animator.SetFloat(AXIS_V, direction.y);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("SafeZone"))
        {
            isChasing = false;
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("SafeZone").transform.position, -speed * 3 * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            isChasing = true;
        }

        if (other.tag.Equals("SafeZone"))
        {
            isChasing = false;
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("SafeZone").transform.position, -speed * 3 * Time.deltaTime);
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
            FindObjectOfType<AudioManager>().Play("Player Hurt");
            Destroy(Instantiate(bloodAnimation, player.transform.position, Quaternion.identity), 0.5f);
            isWalking = false;
            isChasing = false;

            GetStunned();
        }
    }

    public void GetStunned()
    {
        this.stunCounter -= 0.001f;
    }

    private void Retreat ()
    {
        if (tag.Equals("EnemyRanged"))
        {
            if(!isBoss)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * 3 * Time.deltaTime);
            }
            else
            {
                Vector2 destination = GameObject.Find("Puntos").transform.position;

                transform.position = Vector2.MoveTowards(transform.position, destination, speed * 10 * Time.deltaTime);
                this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
    }

    public void SetDead (bool dead)
    {
        this.isDead = dead;
    }

    public void Die ()
    {
        this.gameObject.SetActive(false);
    }

    public void PlayDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("Explosion");
    }
}
