
using UnityEngine;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

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

    //right is false
    //left is truth
    private bool facing= false;


    //Get refrence to the valueKeepingBehavior
    [SerializeField]
    private ValueKeepingBehavior liveValue;

    public Stopwatch invinsiblityTimer = new Stopwatch();

    public GameObject punchBox;

    Stopwatch stopwatch = new Stopwatch();

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


    }

    void FixedUpdate()
    {
        movmentManager();
        punchManager();
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // UnityEngine.Debug.Log("this far");
        if (other.gameObject.CompareTag("Enemy") && invinsiblityTimer.ElapsedMilliseconds > 100)
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
            punchBox.transform.localPosition = new Vector3(1, 0, 0);
        }
        else if (moveInput < 0)
        {
            facing = true;
            punchBox.transform.localPosition = new Vector3(-1, 0, 0);
        }
        //If the player is on ground and space key is pressed
    
        if (IsGrounded() == true && Input.GetKey(KeyCode.Space))
        {
            rigi.AddForce(0,jumpForce,0,ForceMode.Impulse);
           
           
        }

        
        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y - fallSpeed , 0);
    }
    void punchManager()
    {
        //check if player wants to punch
        if (Input.GetKey(KeyCode.R) && stopwatch.ElapsedMilliseconds > 200)
        {
            punchBox.SetActive(true);
            stopwatch.Restart();
        }
        //get rid of punch box
        if (punchBox.activeSelf == true && stopwatch.ElapsedMilliseconds > 100)
        {
            punchBox.SetActive(false);
        }

    }
    //checks if the player is currently grounded
    private bool IsGrounded()
    {
       
        //checks if the player is on the ground by calculating if the ground is colliding with the bottom of the capsule
        return Physics.CheckCapsule(coli.bounds.center, new Vector3(coli.bounds.center.x, coli.bounds.min.y,coli.bounds.center.z),coli.radius * .8f,GroundLayers);        
    }
}