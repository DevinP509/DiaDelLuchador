using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

/// <summary>
/// Manages the Players Punch and all related game objects
/// </summary>

public class AttackBehavior : MonoBehaviour
{   
    //current Punch
    public int PunchPhase = 0;
    //All the phases
    public GameObject[] PunchPhases;
    //the current punch partical
    public GameObject CurrentPunch;
    //time between phasese
    public float timeBetweenChargePhases;
     //the hitbox the punch
    public GameObject punchBox;
    //Animator
    public Animator animator;
  
    //a multiplyer for the distance the punch will send you
    public float PunchPower;
    //partical affect phases
    public GameObject[] ParticalPhases;
    //used to prevent punch spamming
    private Stopwatch PunchCoolDown = new Stopwatch();
       //stores the current speed of the player
    private float speedStorage;
    //holder of the current Time
    private float holderTime;
    
    //Activated in the player is charging a punch
    private bool IsCharging = false;
     //Determine if the player is going 
    private bool goingRight;
    private bool goingLeft;  
    
    private Rigidbody rb;
    //Players Movment Script
    private PlayerMovementBehavior MovmentScript;

    [SerializeField]
    
    
    public float chargeTime;   
    public GameObject chargebar;

 
    // Start is called before the first frame update
    void Start()
    {
        
        //get components
        rb = GetComponent<Rigidbody>();
        MovmentScript = GetComponent<PlayerMovementBehavior>();
        PunchCoolDown.Start();
        //store a refrence to the players origonal movement speed
        speedStorage = MovmentScript.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        punch();
        punchMovmentManger();
        //manages the movment of punch relase
    }
    void punch()
    {

        //Checks Time Since Holder Time
        timeElapsed();
        //Update Charge Phases
        if(IsCharging)
        {
            
            ChargeBarAnimator();
        }
       
        

        //check if player wants to punch and if the punch box is currently active
        if (Input.GetAxis("Fire1") > 0  && punchBox.activeSelf == false)
        {       
            //start charging if the the player isnt already charging 
            if(IsCharging == false)
            {
                MovmentScript.DisabledForPunch = true;
                //set holder Time
                holderTime = Time.time;
                
                //start the charge timer 
                IsCharging = true;
               
                animator.SetBool("IsCharging", true);
            }                 
        }        
        //if the player releases the charge button then release the charge
        else if(IsCharging == true &&  Input.GetAxis("Fire1") == 0)
        {
            ReleaseCharge();
        }

        //get rid of punch box after a attack depending on how long it was charged
        if (punchBox.activeSelf == true && PunchCoolDown.ElapsedMilliseconds >   200  * PunchPhase)
        {
                
                animator.SetBool("IsPunching", false);
                animator.speed = 1;
              
                //Reenable Movment
                MovmentScript.DisabledForPunch = false;
                //Set Back to Normal State
                punchBox.SetActive(false);
                
          
        }
    }

    void ReleaseCharge()
    {
        //manages the movment of punch relase
        //set charging to false
        IsCharging = false;
        //set animator bools
        animator.SetBool("IsCharging", false);
        animator.SetBool("IsPunching", true);
        //calculate animator speed
        animator.speed = animator.speed / PunchPhase;
        //set chargebar back 
        CurrentPunch = PunchPhases[0];
        //play charge Bar Animator 
        ChargeBarAnimator();
        //CoolDownToPreventSpam
        PunchCoolDown.Restart();
        //Play Punch Partical
        ParticalPhases[PunchPhase].GetComponent<ParticleSystem>().Play();
        //Enable Punch Hitbox
        punchBox.SetActive(true);       
    }
    void punchMovmentManger()
    {

        //check if you are which way you are going then launch you in the direction based on the time you are charging
        if(MovmentScript.facing ==false && punchBox.activeInHierarchy )
        {
            //add force going right and slightly up to prevent the player from going down
            rb.AddForce(new Vector3(PunchPower * PunchPhase, .15f, 0), ForceMode.Impulse);
            return;

        }
        else if(MovmentScript.facing ==true && punchBox.activeInHierarchy)
        {
            //add force going left and slightly up to prevent the player from going down
            rb.AddForce(new Vector3(-PunchPower * PunchPhase, .15f, 0), ForceMode.Impulse);
            return;
            
        }
       
        //the punch movment is complete
        goingLeft = false;
        goingRight = false;
     
       
    }
    //Animates The Charge Bar
    void ChargeBarAnimator()
    {
        foreach(GameObject i in PunchPhases)
        {
            if(i != CurrentPunch)
            {
                i.SetActive(false);
            }
        }
        CurrentPunch.SetActive(true);
    }
    //Time Since Holder Time
    void timeElapsed()
    {
        float timeElapsed = Time.time - holderTime;
        if (timeElapsed >= 2 * timeBetweenChargePhases)
        {

            CurrentPunch = PunchPhases[3];
            PunchPhase = 3;
        }
        else if (timeElapsed >= 1 * timeBetweenChargePhases)
        {


            CurrentPunch = PunchPhases[2];
            PunchPhase = 2;

        }
       else if (timeElapsed >= 0 * timeBetweenChargePhases)
       {
            CurrentPunch = PunchPhases[1];
            PunchPhase = 1;
       }
        else
        {
            CurrentPunch = PunchPhases[0];
            PunchPhase = 0;
        }
    }
   
    
}
