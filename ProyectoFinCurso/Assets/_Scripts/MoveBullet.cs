using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    public int damage;
    public GameObject canvasDamage;

    public float speed;
    private Transform player;
    private Vector2 target;
    public GameObject acidAnimation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        FindObjectOfType<AudioManager>().Play("Acid Bullet");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyShot();
        }
    }
    /*private void FixedUpdate()
    {
        playerPosition.Normalize();
        _rigidbody.velocity = playerPosition * 2 - _rigidbody.velocity * 1.0f;
        //Double shot:
        //_rigidbody.velocity = playerPosition - _rigidbody.velocity * 1.1f;
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("NPC") || collision.gameObject.tag.Equals("Boundary"))
        {  
            DestroyShot();
        }
         
        if (collision.gameObject.tag.Equals("Player") && GameObject.Find("Player").GetComponent<HealthManager>().isInmune == false)
        {
            var clone = (GameObject)Instantiate(canvasDamage, collision.gameObject.transform.position, Quaternion.Euler(Vector3.zero));
            clone.GetComponent<DamageNumber>().damagePoint = damage;
            collision.gameObject.GetComponent<HealthManager>().DamageCharacter(damage);
            DestroyShot();
        }     
    }

    private void DestroyShot ()
    {
        Destroy(Instantiate(acidAnimation, transform.position, Quaternion.identity), 0.5f);
        Destroy(this.gameObject);   
    }
}
