using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField]
    [Range (0, 5)]
    protected int health; //Health Component of Enemy

    [SerializeField]
    [Range (0.1f, 10.0f)]
    protected float speed; //Speed component that Enemy Can Run

    [SerializeField]
    [Range (1, 5)]
    protected int gems; //Gems that Player can collect to save life

    [SerializeField]
    protected Transform pointA, pointB; //Transforming Position to one place to another

    protected Vector3 currentTarget; //Keep track of the Current Target
    protected Animator anim; //Enemy Animation
    protected SpriteRenderer sprite; //Enemy Sprite
    protected bool isHit = false; //Variable for Enemy Hit freeze

    //variable to store the Player position in General
    protected Player player;
 
    //Init Method
    public virtual void Init()
    {
        anim = GetComponentInChildren<Animator>(); //Initialize the Animator Component
        sprite = GetComponentInChildren<SpriteRenderer>(); //Initialize the Sprite of Every Enemy Component and will use below
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>(); //Initialize the Player with it's Tag

    }

    //Update Method
    public virtual void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) //if Animator is in the Idle stage then Do Nothing
        {
            return;
        }
        Movement(); //Calling the Movement Method
    }

    //Start Method
    private void Start()
    {
        Init(); //Calling the Init Method
    }

    //Movement Method
    public virtual void Movement()
    {
        //Giant Flip code when reached from point A to B
        if (currentTarget == pointA.position)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            //Target Position forward
            currentTarget = pointB.position;
            anim.SetTrigger("Idle"); //Trigger when Target reached to Position B
        }

        else if (transform.position == pointB.position)
        {
            //Target position Backward
            currentTarget = pointA.position;
            anim.SetTrigger("Idle"); //Trigger when Target reached to Position A
        }

        if (isHit == false)
        {
            //Update Position with Current Target
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }

        // Create variable to store the Distance and give distance b/w two position in Units
        float distance = Vector3.Distance(transform.position, player.transform.position);

        // Condition check whether Player and Enemy is in the distance of 2 units
        if (distance > 2.0f) 
        {
            isHit = false;
            anim.SetBool("InCombat", false);
            // Set the InCombat value to False, because no longer need to battle
        }
    }
}
