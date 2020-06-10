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
        rb = GetComponent<Rigidbody>();
        MovmentScript = GetComponent<PlayerMovementBehavior>();
        PunchCoolDown.Start();
    }

    // Update is called once per frame
    void Update()
    {
        punch();
        punchMovmentManger();
    }
    void punch()
    {


        if(IsCharging == true)
        {
            chargeTime = ChargeTimer.ElapsedMilliseconds / 1000;
            if(chargeTime >= 5)
            {
                chargeTime = 5;
            }
                chargebar.transform.localScale = new Vector3(.5f, chargeTime, 1);
        }
        //check if player wants to punch
        if (Input.GetAxis("Fire1") > 0 && PunchCoolDown.ElapsedMilliseconds > 1200)
        {       
            if(IsCharging == false)
            {
                startCharging(); 

            }
                 
        }        
        else if(IsCharging == true &&  Input.GetAxis("Fire1") == 0)
        {
            ReleaseCharge();
        }
        //get rid of punch box
        if (punchBox.activeSelf == true && PunchCoolDown.ElapsedMilliseconds > 2000)
        {
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
        chargebar.transform.localScale = new Vector3(.5f, 0, 1);
    }
    void punchMovmentManger()
    {
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
