using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GeneralAIBehavior : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private float distance;
    private bool alert_state;
    public float alert_range;
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    public GameObject hitbox;
    private bool Attacking = false;
    private float timetoAttack = 6f;
    private float attackTimer = 0;
    void Start()
    {
        currentPoint = pointB.transform;
        hitbox = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < alert_range)
        {
            alert_state = true;
        }
        Move();
        Attack();
        AttackTime();
       
    }

    void Attack()
    {
        if (attackTimer == 0)
        {
            Attacking = true;
            hitbox.SetActive(Attacking);
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

    void Move()
    {
        if (alert_state)
        {
            Vector2 direction = player.transform.position - transform.position;
            transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
            transform.position = Vector2.MoveTowards(this.transform.position, currentPoint.position, speed * Time.deltaTime);
        }
    }
}
