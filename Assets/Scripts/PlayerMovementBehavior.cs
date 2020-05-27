using UnityEngine;

public class PlayerMovementBehavior : MonoBehaviour
{
    //Refrences to the rigidbody
    private Rigidbody rigi;

    //Used to dictate the players movements
    private float moveInput;

    //Base player movement
    public float speed;

    //Determines how high the player can jump
    public float jumpForce;

    //Determines what ground is
    private bool isGrounded;


    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //moveInput is equel to the Horizontal control
        moveInput = Input.GetAxisRaw("Horizontal");

        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y, 0);
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            //If the gameObject is ground set isGrounded to true
            case "Ground":
                isGrounded = true;
                ; break;
            default:
                isGrounded = false;
                break;
        }
        if (!collision.gameObject)
        {
            isGrounded = false;
        }
    }

    void Update()
    {
        //If the player is on ground and space key is pressed
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            //Player jumps
            rigi.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }

    }
}