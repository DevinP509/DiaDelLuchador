using UnityEngine;
using System.Diagnostics;
public class PlayerMovementBehavior : MonoBehaviour
{

    private CharacterController charController;

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
    private bool facing = false;
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

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    void FixedUpdate()
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

        //How fast the player moves
        rigi.velocity = new Vector3(moveInput * speed, rigi.velocity.y - fallSpeed, 0);
    }

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
        if (punchBox.activeSelf == true && stopwatch.ElapsedMilliseconds > 100)
        {
            punchBox.SetActive(false);
        }
        //If the player is on ground and space key is pressed
        if (charController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //Player jumps
            rigi.velocity = Vector3.up * jumpForce;
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.R) && stopwatch.ElapsedMilliseconds > 200)
        {
            punchBox.SetActive(true);
            stopwatch.Restart();
        }

    }
}



//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerMove : MonoBehaviour
//{
//    //used for the player moving horizontally and vertically
//    [SerializeField] private string horizontalInputName;
//    [SerializeField] private float movementSpeed;

//    //used for the player to jump
//    [SerializeField] private AnimationCurve jumpFallOff;
//    [SerializeField] private float jumpMultiplier;
//    [SerializeField] private KeyCode jumpKey;

//    private CharacterController charController;

//    private bool isJumping;

//    private void Awake()
//    {
//        charController = GetComponent<CharacterController>();
//    }

//    private void Update()
//    {
//        PlayerMovement();
//    }

//    //handles the character controler movement
//    private void PlayerMovement()
//    {
//        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;

//        Vector3 rightMovement = transform.right * horizInput;

//        charController.SimpleMove(rightMovement);

//        JumpInput();
//    }

//    //handles the jump input
//    private void JumpInput()
//    {
//        if (Input.GetKeyDown(jumpKey) && !isJumping)
//        {
//            isJumping = true;
//            StartCoroutine(JumpEvent());
//        }
//    }

//    private IEnumerator JumpEvent()
//    {
//        charController.slopeLimit = 90.0f;
//        float timeInAir = 0.0f;

//        do
//        {
//            float jumpForce = jumpFallOff.Evaluate(timeInAir);
//            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
//            timeInAir += Time.deltaTime;

//            yield return null;

//        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

//        charController.slopeLimit = 45.0f;
//        isJumping = false;
//    }
//}