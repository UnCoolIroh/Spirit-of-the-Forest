using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D myRigidBody;
    public int speed = 20;
    private Animator animator;
    public Vector2 change = new Vector2(0, 0);
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FixedUpdate()
    {
        Move();
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