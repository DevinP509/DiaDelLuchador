using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    //used to prevent punch spamming
    private Stopwatch PunchCoolDown = new Stopwatch();

    private Stopwatch ChargeTimer = new Stopwatch();
    //used to give movment over time on punch release
    private Stopwatch PunchMoveOvertime = new Stopwatch();
    //the hitbox the punch
    public GameObject punchBox;
    //Activated in the player is charging a punch
    private bool IsCharging = false;
    private int PunchPhase = 0;
    public GameObject[] PunchPhases;
    public GameObject CurrentPunch;
    public float timeBetweenChargePhases;
    //stores the current speed of the player
    private float speedStorage;
    //a multiplyer for the distance the punch will send you
    public float PunchPower;
    //the rate the punch will charge
    public float ChargeRate;
    //the minimum charge a punch can have
    public float MinCharge;
    [SerializeField]
    
    
    public float chargeTime;
    
    public GameObject chargebar;

    private Rigidbody rb;
    private PlayerMovementBehavior MovmentScript;

    private bool goingRight;
    private bool goingLeft;
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
    void Update()
    {
        punch();
        //manages the movment of punch relase
        punchMovmentManger();
        //Temporary Position Allows exiting 
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
    void punch()
    {

        //if a attack is charging lower speed and start filling
        if(IsCharging == true)
        {
            ChargePhaseManager();
            ChargeBarAnimator();
        }

        //check if player wants to punch and if the punch box is currently active
        if (Input.GetAxis("Fire1") > 0  && punchBox.activeSelf == false)
        {       
            //start charging if the the player isnt already charging 
            if(IsCharging == false)
            {

                startCharging(); 

            }                 
        }        
        //if the player releases the charge button then release the charge
        else if(IsCharging == true &&  Input.GetAxis("Fire1") == 0)
        {
            ReleaseCharge();
        }

        //get rid of punch box after a attack depending on how long it was charged
        if (punchBox.activeSelf == true && PunchCoolDown.ElapsedMilliseconds > 200*chargeTime)
        {
            
                //set movment back to normal after punch
                MovmentScript.speed = speedStorage;
            
                punchBox.SetActive(false);
            
          
        }
    }
    void startCharging()
    {
        //start the charge timer 
        IsCharging = true;
        ChargeTimer.Start();             
    }
    void ReleaseCharge()
    {
        if(MovmentScript.facing == false)
        {
            
            goingRight = true;
            //rb.AddForce(new Vector3(300*chargeTime,0,0), ForceMode.Impulse);            
        }
        else
        {

            goingLeft = true;
            //rb.AddForce(new Vector3(-300 * chargeTime, 0, 0),ForceMode.Impulse);            
        }


        PunchMoveOvertime.Restart();

        IsCharging = false;
        ChargeTimer.Reset();
        PunchCoolDown.Restart();
        punchBox.SetActive(true);       
    }
    void punchMovmentManger()
    {
        //check if you are which way you are going then launch you in the direction based on the time you are charging
        if(goingRight && PunchMoveOvertime.ElapsedMilliseconds < 200 * PunchPhase)
        {
            //add force going right
            rb.AddForce(new Vector3(PunchPower * PunchPhase, 0, 0), ForceMode.Impulse);
            
            return;
        }
        else if(goingLeft && PunchMoveOvertime.ElapsedMilliseconds < 200 * PunchPhase)
        {
            //add force going left
            rb.AddForce(new Vector3(-PunchPower * PunchPhase, 0, 0), ForceMode.Impulse);
           
            return;
        }
        //the punch movment is complete
        goingLeft = false;
        goingRight = false;
        //stop the movement timer
        PunchMoveOvertime.Stop();
    }
    void ChargePhaseManager()
    {
        //set the players speed down while charging a punch
        MovmentScript.speed = speedStorage/2;
        //store charge time in seconds
        chargeTime = (ChargeTimer.ElapsedMilliseconds / 1000);
        //raises the charge above the minimum charge 
        if (chargeTime >0)
        {
            if(chargeTime >1 * timeBetweenChargePhases && chargeTime<2 * timeBetweenChargePhases)
            {
                UnityEngine.Debug.Log("2");
                
                CurrentPunch = PunchPhases[2];
                PunchPhase = 2;
            }
            else if(chargeTime >2 * timeBetweenChargePhases)
            {

                CurrentPunch = PunchPhases[3];
                PunchPhase = 3;
            }
            else
            {
                CurrentPunch = PunchPhases[1];
                PunchPhase = 1;
            }
           
        }
        else
        {
            CurrentPunch = PunchPhases[0];
            PunchPhase = 0;
        }
         
    }
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
}
