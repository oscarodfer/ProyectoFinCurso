using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1;
    private Rigidbody2D _rigidbody;

    private bool isMoving;

    [Tooltip("Tiempo que tarda el enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda el enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;
    [Tooltip("Dirección en la que se mueve el enemigo, se genera aleatoriamente")]
    public Vector2 directionToMove;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        //Multiplicamos por RandomRange para que sea aleatorio el arranque.
        timeBetweenStepsCounter = timeBetweenSteps * Random.Range(0.5f, 1.5f);
        timeToMakeStepCounter = timeToMakeStep * Random.Range(0.5f, 1.5f);
    }


    void Update()
    {
        if (isMoving){

            timeToMakeStepCounter -= Time.deltaTime;
            _rigidbody.velocity = directionToMove * speed;
            //Cuando me quedo sin tiempo de movimiento, paramos al enemigo.
            if (timeToMakeStepCounter < 0) {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                _rigidbody.velocity = Vector2.zero;
            }
        }
        else {
            timeBetweenStepsCounter -= Time.deltaTime;
            //Cuando me quedo sin tiempo de estar parado, arrancamos al enemigo para que de un paso.
            if (timeBetweenStepsCounter < 0) {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                //El movimiento del enemigo lo realizamos aleatoriamente.
                directionToMove = new Vector2(Random.Range(-1, 2), Random.Range(-1, 2));
            }
        }



    }
}
