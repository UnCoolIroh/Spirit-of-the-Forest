using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public float speed = 20;
    private Animator animator;
    public Vector2 change = new Vector2(0, 0);
    private float timer = 5;
    private bool countDown = false;
    public GameObject hitbox;
    private bool Attacking = false;
    public bool isBear = true;
    private float timetoAttack = 0.30f;
    private float attackTimer = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = transform.GetChild(0).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        Swap();
        if (countDown)
        {
            startTimer();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        AttackTime();
    }

    public void FixedUpdate()
    {
        Move();
    }

    void Swap()
    {
        if (timer == 5 && Input.GetKeyDown(KeyCode.Space) && !animator.GetBool("bearSwap"))
        {
            isBear = true;
            animator.SetBool("bearSwap", true);
            speed *= .6f;
            timetoAttack *= 15;
            startTimer();

        }
        else if (timer == 5 && Input.GetKeyDown(KeyCode.Space) && animator.GetBool("bearSwap"))
        {
            isBear = false;
            animator.SetBool("bearSwap", false);
            speed /= .6f;
            timetoAttack /= 4;
            startTimer();
        }


    }

    void AttackTime()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= timetoAttack)
        {
            attackTimer = 0;
            Attacking = false;
            hitbox.SetActive(Attacking);
        }
    }

    void Attack()
    {
        if (!Attacking)
        {
            Attacking = true;
            hitbox.SetActive(Attacking);
            print("PLAYER ATTACK");
            animator.SetTrigger("attack");
        }

    }

    void startTimer()
    {
        countDown = true;
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 5;
            countDown = false;
        }
    }

    public void Move()
    {
        Vector2 velocity = new Vector2(speed, speed);
        if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            change = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            //animator.SetBool("isWalking", true);
        }
        else
        {
            //animator.SetBool("isWalking", false);
            //animator.SetFloat("lastInputX", change.x);
            //animator.SetFloat("lastInputY", change.y);
            return;
        }
        //animator.SetFloat("InputX", change.x);
        //animator.SetFloat("InputY", change.y);
        Vector2 delta = change * velocity * Time.fixedDeltaTime;
        Vector2 newPos = myRigidBody.position + delta;
        myRigidBody.MovePosition(newPos);
    }

}