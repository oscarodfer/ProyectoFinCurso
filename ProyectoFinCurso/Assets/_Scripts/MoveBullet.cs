using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int currentDirection;
    public float speed = 1.5f;


    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = walkingDirections[currentDirection] * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
