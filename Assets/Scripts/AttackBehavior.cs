using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AttackBehavior : MonoBehaviour
{
    private Stopwatch PunchCoolDown = new Stopwatch();
    private Stopwatch ChargeTimer = new Stopwatch();
    private Stopwatch PunchMoveOvertime = new Stopwatch();
    public GameObject punchBox;
    private bool IsCharging = false;
    private float CurrentDamage;
    private float speedStorage;
    [SerializeField]
    
    
    public float chargeTime;
    
    public GameObject chargebar;

    private Rigidbody rb;
    private PlayerMovementBehavior MovmentScript;

    private bool goingRight;
    private bool goingLeft;

    //Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        MovmentScript = GetComponent<PlayerMovementBehavior>();
        PunchCoolDown.Start();
        speedStorage = MovmentScript.speed;
    }

    //Update is called once per frame
    void Update()
    {
        punch();
        punchMovmentManger();
    }

    void punch()
    {

        //if a attack is charging lower speed and start filling
        if(IsCharging == true)
        {
            //set the players speed down while charging a punch
            MovmentScript.speed = speedStorage/4;

            //store charge time in seconds
            chargeTime = ChargeTimer.ElapsedMilliseconds / 1000;

            //cap the charge time
            if(chargeTime >= 5)
            {
                chargeTime = 5;
            }

            //expand the bar depending on charge time
            chargebar.transform.localScale = new Vector3(.5f, chargeTime, 1);
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
        IsCharging = true;
        ChargeTimer.Start();
        
        chargeTime = ChargeTimer.ElapsedMilliseconds / 1000;
        chargebar.transform.localScale = new Vector3(.5f,chargeTime,1);
    }

    void ReleaseCharge()
    {
        if(MovmentScript.facing == false)
        {       
            goingRight = true;       
        }
        else
        {
            goingLeft = true;;            
        }

        PunchMoveOvertime.Restart();

        IsCharging = false;
        ChargeTimer.Reset();
        PunchCoolDown.Restart();
        punchBox.SetActive(true);
        chargebar.transform.localScale = new Vector3(.5f, 0, 1);
    }

    void punchMovmentManger()
    {
        //check if you are which way you are going then launch you in the direction based on the time you are charging
        if(goingRight && PunchMoveOvertime.ElapsedMilliseconds < 200 * chargeTime)
        {
            rb.AddForce(new Vector3(2 * chargeTime, 0, 0), ForceMode.Impulse);
            
            return;
        }

        else if(goingLeft && PunchMoveOvertime.ElapsedMilliseconds < 200 * chargeTime)
        {
            rb.AddForce(new Vector3(-2 * chargeTime, 0, 0), ForceMode.Impulse);
           
            return;
        }

        goingLeft = false;
        goingRight = false;
        
        PunchMoveOvertime.Stop();
    }
}
