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
    void Start()
    {
        currentPoint = pointB.transform;
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
