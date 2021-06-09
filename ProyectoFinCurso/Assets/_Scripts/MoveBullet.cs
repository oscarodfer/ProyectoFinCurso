using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    private int currentDirection;
    public float speed = 1.5f;
    public int damage = 20;

    private CharacterStats playerStats;
    private CharacterStats enemyStats;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = walkingDirections[currentDirection] * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            Destroy(this.gameObject);
        }
    }
}
