using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public int direction;
    public float speed = 1.5f;
    public int damage = 20;
    public GameObject canvasDamage;


    void Start()
    {
        direction = GameObject.Find("Enemy3").GetComponent<EnemyBehaviour>().currentDirection;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
   
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = walkingDirections[direction] * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Boundary"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            var clone = (GameObject)Instantiate(canvasDamage, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoint = damage;
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            Destroy(this.gameObject);
        }
       
    }
}
