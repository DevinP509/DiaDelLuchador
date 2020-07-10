
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class PlayerMovementBehavior : MonoBehaviour
{
    //Refrences to the rigidbody
    private Rigidbody rigi;

    //Used to dictate the players movements
    private float moveInput;

    //Base player movement
    public float speed;

    public float DecelPerSec;
    //Gravity
    public float fallSpeed;
    //public float airControl =1;
   
    
    private Stopwatch JumpCoolDown = new Stopwatch();
    private bool jumped;
    public ParticleSystem bloodSpray;
    public float SpeedCap;
    //right is false
    //left is truth
    [HideInInspector]
    public bool facing= false;
    public Stopwatch invinsiblityTimer = new Stopwatch();
    private float speedhold;
    private float jumpforceHold;
    private float fallSpeedHold;
    public Animator animator;
    private bool hasLanded;
    //Get refrence to the valueKeepingBehavior
    [SerializeField]
    public ValueKeepingBehavior liveValue;

    

    public GameObject punchBox;



    public LayerMask GroundLayers;

    //Determines how high the player can jump
    public float jumpForce;
    public bool DisabledForPunch = false;
    //Determines what ground is
    //private bool isGrounded;
    CapsuleCollider coli;
    AttackBehavior attackBehavior;

    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();
        coli = GetComponent<CapsuleCollider>();
        attackBehavior = GetComponent<AttackBehavior>();
        invinsiblityTimer.Start();
        JumpCoolDown.Start();
        speedhold = speed;
        jumpforceHold = jumpForce;
        fallSpeedHold = fallSpeed;
    }

    void Update()
    {
       
       animator.SetFloat("VerticalVelocity", rigi.velocity.y);
 

        movmentManager();
        // movmentManager();
        //disables 
        if (DisabledForPunch)
        {
            movmentManager();
            speed = 0;
            jumpForce = 0;
        }

        else
        {   
            VelocityCorrection();

            speed = speedhold;
            jumpForce = jumpforceHold;
        }

        //checks if player has died
        if (liveValue.lives <= 0)
        {
            die();
            animator.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Kills the player if the fall off the screen
        if (other.gameObject.CompareTag("DeathWall"))
        {
            die();
        }
        //Checks if the player has been hit
        if ((other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("FireBall")) && invinsiblityTimer.ElapsedMilliseconds > 3000)
        {
            animator.SetTrigger("Hit");
            //UnityEngine.Debug.Log("this far");
            liveValue.lives--;
            liveValue.hearts[(int)(liveValue.lives)].SetActive(false);

            invinsiblityTimer.Restart();
        }
    }
    void movmentManager()
    {
      
        //moveInput is equel to the Horizontal control
        moveInput = Input.GetAxisRaw("Horizontal");

        //switches the players facing value
        if (moveInput > 0 && !DisabledForPunch)
        {
            animator.SetBool("IsWalking", true);
            facing = false;
            //punchBox.transform.localPosition = new Vector3(1, 0, 0);
            transform.localRotation= new Quaternion(0,0,0,0) ;
        }
        else if (moveInput < 0 && ! DisabledForPunch )
        {
            animator.SetBool("IsWalking", true);
            facing = true;
            //punchBox.transform.localPosition = new Vector3(-1, 0, 0);
            transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            //slow the player down 10% if no key is held down
            rigi.AddForce(-rigi.velocity.x * DecelPerSec * Time.deltaTime, 0, 0,ForceMode.Impulse) ;
        }
        //If the player is on ground and space key is pressed Jump
        
        if (IsGrounded() == true && Input.GetAxis("Jump") !=0 && JumpCoolDown.ElapsedMilliseconds > 500)
        {
            //set up animator
            animator.SetBool("IsWalking", false);
            animator.SetTrigger("Jump");
            //set players y velocity to zero
            rigi.velocity = new Vector3(rigi.velocity.x,0,0);
            //add jumpforce
            rigi.AddForce(0,jumpForce,0,ForceMode.Impulse);
            //set jump cool down to prevent multi jumping
            JumpCoolDown.Restart();                  
        }
        //If the Jump button is relased Increase the fall speed
        if(Input.GetAxis("Jump") == 0 && fallSpeed != fallSpeedHold * 5)
        {
            fallSpeed = fallSpeed * 5;
        }
        else
        {
            fallSpeed = fallSpeedHold;
        }



       
        //How fast the player moves
        if(IsGrounded())
        {
           
            //rigi.velocity = new Vector3(moveInput * speed * airControlHold, 0, 0);
            rigi.AddForce(moveInput * speed * Time.deltaTime,-fallSpeed * Time.deltaTime,0);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            rigi.AddForce(((moveInput * speed)*.75f )* Time.deltaTime,-fallSpeed* Time.deltaTime , 0);
        }
       
        
        
    }
    //checks if the player is currently grounded
    private bool IsGrounded()
    {
       
        //checks if the player is on the ground by calculating if the ground is colliding with the bottom of the capsule
        return Physics.CheckCapsule(coli.bounds.center, new Vector3(coli.bounds.center.x, coli.bounds.min.y,coli.bounds.center.z),coli.radius * .5f,GroundLayers);        
    }
    //prevents the player from accelerating upward faster than they should
    private void VelocityCorrection()
    {
        if(rigi.velocity.x > SpeedCap)
        {
            rigi.velocity = new Vector3(SpeedCap,rigi.velocity.y,rigi.velocity.z);
        }
        else if(rigi.velocity.x < -SpeedCap)
        {
            rigi.velocity = new Vector3(-SpeedCap, rigi.velocity.y, rigi.velocity.z);
        }
        
    }
    //kills player and goes to next scene
    public void die()
    {
        bloodSpray.gameObject.SetActive(true);
        bloodSpray.Play();
        SceneManager.LoadScene(3);
    }
}