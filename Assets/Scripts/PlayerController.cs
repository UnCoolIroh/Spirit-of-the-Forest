using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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
    public bool isAlive = true;
    public int coins;
    public int gems;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI gemText;
    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
        {
            if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(0))
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        hitbox = transform.GetChild(0).gameObject;
        coinText.text = coins.ToString();
        gemText.text = gems.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Swap();
            if (countDown)
            {
                startTimer();
            }
            if (UserInput.instance.controls.Gameplay.Attack.WasPerformedThisFrame())
            {
                Attack();
            }

            AttackTime();
        }
    }

    public void FixedUpdate()
    {
        if (isAlive)
        {
            Move();
        }
    }

    void Swap()
    {
        if (timer == 5 && UserInput.instance.controls.Gameplay.Transform.WasPerformedThisFrame() && !animator.GetBool("bearSwap"))
        {
            isBear = true;
            animator.SetBool("bearSwap", true);
            speed *= .6f;
            timetoAttack *= 15;
            startTimer();

        }
        else if (timer == 5 && UserInput.instance.controls.Gameplay.Transform.WasPerformedThisFrame() && animator.GetBool("bearSwap"))
        {
            isBear = false;
            animator.SetBool("bearSwap", false);
            speed /= .6f;
            timetoAttack /= 15;
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

    public void PickupCoin(int quantity)
    {
        coins += quantity;
        coinText.text = coins.ToString();
    }

    public void PickupGem(int quantity)
    {
        gems += quantity;
        gemText.text = gems.ToString();
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
        if (UserInput.instance.moveInput.x != 0 || UserInput.instance.moveInput.y != 0)
        {
            change = new Vector2(UserInput.instance.moveInput.x, UserInput.instance.moveInput.y);
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