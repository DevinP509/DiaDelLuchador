using UnityEngine;
using System.Diagnostics;
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
    //Determines how high the player can jump
    public float jumpForce;
    //right is false
    //left is truth
    private bool facing= false;
    //Determines what ground is
    private bool isGrounded;

    public int lives;

    public Stopwatch invinsiblityTimer = new Stopwatch();

    public GameObject punchBox;

    Stopwatch stopwatch = new Stopwatch();
    
    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();
        stopwatch.Start();
        invinsiblityTimer.Start();
    }

    void FixedUpdate()
    {
        //moveInput is equel to the Horizontal control
        moveInput = Input.GetAxisRaw("Horizontal");
        if(moveInput >0)
        {
            facing = false;
            punchBox.transform.localPosition = new Vector3(1,0,0);
        }
        else if (moveInput < 0)
        {
            facing = true;
            punchBox.transform.localPosition = new Vector3(-1, 0, 0);
        }

        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y -fallSpeed, 0);
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
        {
            //Player jumps
            rigi.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        if(Input.GetKeyDown(KeyCode.R) && stopwatch.ElapsedMilliseconds > 200)
        {
            punchBox.SetActive(true);
            stopwatch.Restart();
        }

    }
}