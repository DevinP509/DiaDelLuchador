﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
public class WaspEnemyBehavior : MonoBehaviour
{
    private GameObject player;
    public int speed;
    float timeholder;
    public float Health;
    private Camera GameCamera;
    public Rigidbody rb;
    private Renderer rendererer;
    private BoxCollider collider;
    public ParticleSystem bloodSpray;
    private Stopwatch DamagePreventer = new Stopwatch();
    private bool started= false;
    private Animator animator;
    [SerializeField]
    //Refrence to the ValueKeepingBehavior
    private ValueKeepingBehavior scoreKeep;
    public EnemeyDetector enemeyDetector;
    public float AttackAnimationPlayRange;
   
    // Start is called before the first frame update
    void Start()
    {
        enemeyDetector = FindObjectOfType<EnemeyDetector>();
        scoreKeep = FindObjectOfType<ValueKeepingBehavior>();
        player = GameObject.FindWithTag("Player");
        GameCamera = UnityEngine.Camera.main;
        rendererer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
        bloodSpray.Stop();
        timeholder = Time.timeSinceLevelLoad;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (started == false)
        {
            if (timeholder < Time.timeSinceLevelLoad)
            {
                started = true;
            }
        }
        else
        {
            ChasePlayer();
        }
        //attack when in range of the player
       if((player.transform.position - transform.position).magnitude > AttackAnimationPlayRange)
        {
            animator.SetTrigger("Attack");
        }

        //kill if health is zero
        if (Health <= 0)
        {
            enemeyDetector.count--;
            die();
        }
        //prevent 
        if(DamagePreventer.ElapsedMilliseconds > 1000)
        {
            gameObject.tag = "Enemy";
        }
    }
    private void ChasePlayer()
    {
        if (CheckIfOnScreen() == true)
        {

            //Get this agent's position
            Vector3 pos = transform.position;
            //Get the position of the target agent
            Vector3 targetPos = player.transform.position;

            //Calculate the vector describing the direction to the target and normalize it
            Vector3 direction = targetPos - pos;
            direction = direction.normalized;
            //Multiply the direction by the speed we want the agent to move
            Vector3 force = direction * speed;
            //Subtract the agent's current velocity from the result to get the force we need to apply
            force = force - rb.velocity;

            //Return the force
            rb.AddForce(force);
            //flip the wasp sprite
            if(rb.velocity.x >=0)
            {
                transform.localRotation = new Quaternion(0, 180, 0, 0);
                
            }
            else
            {
                transform.localRotation = new Quaternion(0,0, 0, 0);
               
            }
        }

    }
    //check if currently on the screen
    private bool CheckIfOnScreen()
    {
        //generate planes to check agaisnt
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(GameCamera);
        //check if this object collides with those planes
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            return true;
        else
            return false;
    }



    private void die()
    {
        //play blood spray 
        bloodSpray.Play();
        //add a point
        scoreKeep.score++;
        //destroy this object
        rb.useGravity = true;
        
        animator.SetBool("Dead", true);
        Destroy(this);

    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PunchBox"))
        {
            animator.SetTrigger("TakeDamage");
            //prevents the player from being damaged while punching through a enemy
            gameObject.tag = "InactiveEnemy";
            DamagePreventer.Restart();
            //get the time the player charged a punch
            float chargeMult = other.gameObject.GetComponentInParent<AttackBehavior>().PunchPhase;
            //subtract health based off charge time
            Health -= chargeMult;
            //play bloodspray partical
            bloodSpray.Play();
            //knock enemy away
            rb.AddForce(-rb.velocity * chargeMult, ForceMode.Impulse);
          
        }

    }

}
