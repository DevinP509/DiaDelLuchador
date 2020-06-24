
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

    //Gravity
    public float fallSpeed;
    public float airControl =1;
    private float airControlHold;
    private Stopwatch stopwatch = new Stopwatch();
    private Stopwatch JumpCoolDown = new Stopwatch();
    private bool jumped;
    public ParticleSystem bloodSpray;
    //right is false
    //left is truth
    [HideInInspector]
    public bool facing= false;
    public Stopwatch invinsiblityTimer = new Stopwatch();


    //Get refrence to the valueKeepingBehavior
    [SerializeField]
    public ValueKeepingBehavior liveValue;

    

    public GameObject punchBox;



    public LayerMask GroundLayers;

    //Determines how high the player can jump
    public float jumpForce;

    //Determines what ground is
    //private bool isGrounded;
    CapsuleCollider coli;


    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();
        coli = GetComponent<CapsuleCollider>();
        stopwatch.Start();
        invinsiblityTimer.Start();
        JumpCoolDown.Start();
        
    }

    void FixedUpdate()
    {
        movmentManager();
        VelocityCorrection();
        
        if(liveValue.lives <= 0)
        {
            die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // UnityEngine.Debug.Log("this far");
        if (other.gameObject.CompareTag("Enemy") && invinsiblityTimer.ElapsedMilliseconds > 200)
        {
            //UnityEngine.Debug.Log("this far");
            liveValue.lives--;

            invinsiblityTimer.Restart();
        }
    }
    void movmentManager()
    {
      
        //moveInput is equel to the Horizontal control
        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0)
        {
            facing = false;
            //punchBox.transform.localPosition = new Vector3(1, 0, 0);
            transform.localRotation= new Quaternion(0,0,0,0) ;
        }
        else if (moveInput < 0)
        {
            facing = true;
            //punchBox.transform.localPosition = new Vector3(-1, 0, 0);
            transform.localRotation = new Quaternion(0, 180, 0, 0);
        }
        //If the player is on ground and space key is pressed
    
        if (IsGrounded() == true && Input.GetKey(KeyCode.Space)&& JumpCoolDown.ElapsedMilliseconds > 100)
        {
            rigi.AddForce(0,jumpForce,0,ForceMode.Impulse);
            JumpCoolDown.Restart();
           
           
        }

        if(IsGrounded() == true)
        {
            airControlHold = 1;
        }
        else
        {
            airControlHold = airControl;
        }

       
        //How fast the player moves
        if(IsGrounded())
        {
            rigi.velocity = new Vector3(moveInput * speed * airControlHold, 0, 0);
        }
        else
        {
            rigi.velocity = new Vector3(moveInput * speed * airControlHold, rigi.velocity.y - fallSpeed , 0);
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
        if(rigi.velocity.y > 25)
        {
            rigi.velocity = new Vector3(rigi.velocity.x,25,rigi.velocity.z);
        }
    }
    public void die()
    {
        bloodSpray.gameObject.SetActive(true);
        bloodSpray.Play();
        SceneManager.LoadScene(2);
    }
}