using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //Constantes y variables
    public static bool playerCreated;

    private const string AXIS_H = "Horizontal";
    private const string AXIS_V = "Vertical";
    private const string LAST_H = "LastH";
    private const string LAST_V = "LastV";
    private const string IS_WALKING = "IsWalking";

    private Rigidbody2D _rb;
    private Animator _animator;
    public Vector2 lastMovement;

    private bool isWalking = false;
    public bool canMove = true;
    public float speed = 5.0f;
    public string nextUuid;

    //M�todos
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        playerCreated = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            return;
        }

        isWalking = false;

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

        /*
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            this.transform.Translate(translation);
            this.isWalking = true;
            this.lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }

        if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {  
            Vector3 translation = new Vector3(0, Input.GetAxisRaw(AXIS_V) * speed * Time.deltaTime, 0);
            this.transform.Translate(translation);
            this.isWalking = true;
            this.lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }
        */
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "Enemy")
        {
            _rb.velocity = Vector2.zero;
            isWalking = false;

            if (collision.gameObject.tag == "Enemy")
            {
                canMove = false;
            }       
        }
    }

    private void LateUpdate()
    {
        if (!isWalking)
        {
            _rb.velocity = Vector2.zero;
        }

        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetFloat(LAST_H, this.lastMovement.x);
        _animator.SetFloat(LAST_V, this.lastMovement.y);
        _animator.SetBool(IS_WALKING, this.isWalking);
    }
}
