using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1.5f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private bool isWalking = false;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps = 1.5f;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep = 3.0f;
    private float timeToMakeStepCounter;

    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int currentDirection;

    public BoxCollider2D mapZone;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timeToMakeStepCounter = timeToMakeStep;
        timeBetweenStepsCounter = timeBetweenSteps;
    }

    private void FixedUpdate()
    {
        if (isWalking)
        {
            if (this.transform.position.x < mapZone.bounds.min.x || this.transform.position.x > mapZone.bounds.max.x || this.transform.position.y < mapZone.bounds.min.y || this.transform.position.y > mapZone.bounds.max.y) 
            {
                StopWalking();
            }
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

    public void StarWalking() {

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
}
