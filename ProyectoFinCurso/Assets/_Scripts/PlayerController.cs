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
    private const string ATT = "IsAttacking";
    private const string IS_WALKING = "IsWalking";
    private const float STUN_DURATION = 1.0f;

    private Rigidbody2D _rb;
    private Animator _animator;
    private Vector2 lastMovement;
    
    private bool isWalking = false;
    public bool isTalking;
    public bool isAttacking = false;
    public bool isFirstAttack = true;
    public bool canMove = true;
    public bool isDamaged = false;
    public float speed = 5.0f;
    public string nextUuid;
    public float attackTime;
    private float attackTimeCounter;
    private float stunCounter;

    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _rb = this.GetComponent<Rigidbody2D>();
        lastMovement = Vector2.zero;
        playerCreated = true;
        isTalking = false;
        stunCounter = STUN_DURATION;
    }

    void Update()
    {
        if (isTalking) 
        {
            _rb.velocity = Vector2.zero;
            return;
        }
        else
        {
            if (_rb.velocity != Vector2.zero)
            {
                isWalking = true;
            }

            if (stunCounter != STUN_DURATION)
            {
                this._rb.velocity = new Vector2(lastMovement.x, lastMovement.y);

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
                if (!canMove)
                {
                    if (isDamaged)
                    {
                        return;
                    }
                    else
                    {
                        canMove = true;
                    }

                    if (isAttacking)
                    {
                        return;
                    }
                    else
                    {
                        isFirstAttack = true;
                        canMove = true;
                    }
                }

                if (isDamaged)
                {
                    isAttacking = false;
                    isFirstAttack = true;
                }

                if (isAttacking)
                {
                    isWalking = false;

                    attackTimeCounter -= Time.deltaTime;

                    if (attackTimeCounter <= 0.0f)
                    {
                        canMove = true;
                        isWalking = true;
                        isAttacking = false;
                        isFirstAttack = true;

                        _animator.SetBool(ATT, false);
                    }
                }
                else
                {
                    canMove = true;
                    isWalking = false;

                    if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && !isDamaged)
                    {
                        int number = Random.Range(0, 6) + 1;
                        FindObjectOfType<AudioManager>().Play("Sword Slash " + number);
                        canMove = false;
                        isAttacking = true;
                        isWalking = false;
                        attackTimeCounter = attackTime;
                        _animator.SetBool(ATT, true);
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
                }
            }
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
        if(collision.gameObject.tag == "Boundary" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BulletEnemy" || collision.gameObject.tag == "EnemyRanged")
        {
            _rb.velocity = Vector3.zero;
            isWalking = false;

            if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BulletEnemy" || collision.gameObject.tag == "EnemyRanged")
            {
                canMove = false;
                isDamaged = true;
                isAttacking = false;
            }       
        }
    }

    private void LateUpdate()
    {
        if (!isWalking)
        {
            _rb.velocity = Vector3.zero;
        }

        _animator.SetBool(IS_WALKING, this.isWalking);
        _animator.SetBool(ATT, this.isAttacking);
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetFloat(LAST_H, this.lastMovement.x);
        _animator.SetFloat(LAST_V, this.lastMovement.y);   
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
