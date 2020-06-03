
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
  
    public int lives;

    public Stopwatch invinsiblityTimer = new Stopwatch();

    public GameObject punchBox;

    Stopwatch stopwatch = new Stopwatch();
    


    //Determines how high the player can jump
    public float jumpForce;

    //Determines what ground is
    //private bool isGrounded;



    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();

        stopwatch.Start();
        invinsiblityTimer.Start();


    }

    void FixedUpdate()
    {
        movmentManager();
        punchManager();
    }

    //Old Jumping System if we decide to use Diagonal platforms

    //void OnCollisionEnter(Collision collision)
    //{

    //    switch (collision.gameObject.tag)
    //    {
    //        //If the gameObject is ground set isGrounded to true
    //        case "Ground":
    //            isGrounded = true;
    //             break;
    //        default:
    //            isGrounded = false;
    //            break;
    //    }
    //    if (!collision.gameObject)
    //    {
    //        isGrounded = false;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        // UnityEngine.Debug.Log("this far");
        if (other.gameObject.CompareTag("Enemy") && invinsiblityTimer.ElapsedMilliseconds > 100)
        {
            UnityEngine.Debug.Log("this far");
            lives--;

            invinsiblityTimer.Restart();
        }
    }


    void Update()
    {
        //get rid of punch box
        if(punchBox.activeSelf == true && stopwatch.ElapsedMilliseconds > 100)
        {
            punchBox.SetActive(false);
        }
        //If the player is on ground and space key is pressed
        if (rigi.velocity.y == 0 && Input.GetKeyDown(KeyCode.Space))

        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y, 0);
    }

    //void OnCollisionEnter(Collision collision)
    //{
    //    switch (collision.gameObject.tag)
    //    {
    //        //If the gameObject is ground set isGrounded to true
    //        case "Ground":
    //            isGrounded = true;
    //            ; break;
    //        default:
    //            break;
    //    }
    //}

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
        if (rigi.velocity.y == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            rigi.velocity = new Vector3(rigi.velocity.x, 1 * jumpForce, 0);
        }

        
        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y- fallSpeed , 0);
    }
    void punchManager()
    {
        //check if player wants to punch
        if (Input.GetKeyDown(KeyCode.R) && stopwatch.ElapsedMilliseconds > 200)
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
}