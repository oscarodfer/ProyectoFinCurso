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
    public float speed = 1.4f;
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

    private GameObject player;
    private Vector2 lastMovement;
    private bool isChasing = false;
    private bool isWalking = false;
    private float stunCounter;

    void Start()
    {
        StartWalking();
        _rb = this.GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        lastMovement = Vector2.zero;
        isWalking = false;
        stunCounter = STUN_DURATION;
        timeBetweenStepsCounter = timeBetweenSteps;
        timeToMakeStepCounter = timeToMakeStep;
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
            
            this._rb.velocity *= -1.0f;

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
                _rb.velocity = player.GetComponent<Transform>().position - this.transform.position * speed;
                ChasePlayer(_rb.velocity);
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

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f && Mathf.Abs(Input.GetAxisRaw(AXIS_V)) <= 0.2f)
        {
            _rb.velocity = new Vector2(Input.GetAxisRaw(AXIS_H) * speed, 0);
            isWalking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f && Mathf.Abs(Input.GetAxisRaw(AXIS_H)) <= 0.2f)
        {
            _rb.velocity = new Vector2(0, Input.GetAxisRaw(AXIS_V) * speed);
            isWalking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f && Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            _rb.velocity = new Vector2(Input.GetAxisRaw(AXIS_H) * speed * 0.6f, Input.GetAxisRaw(AXIS_V) * speed * 0.6f);
            isWalking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }

        Vector2 movement = _rb.velocity;
        movement.Normalize();

        _animator.SetBool(IS_WALKING, isWalking);
        _animator.SetFloat(AXIS_H, movement.x);
        _animator.SetFloat(AXIS_V, movement.y);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
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
        _rb.MovePosition((Vector2)transform.position + (speed * Time.deltaTime * direction));
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
