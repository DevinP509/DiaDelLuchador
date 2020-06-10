using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBehavior : MonoBehaviour
{
    private bool gameStart;
    public GameObject Obj;
    private Rigidbody rigi;

    //Gravity
    public float vSpeed = 1.0f;
    void Start()
    {
        //Get the Rigibody of the player
        rigi = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        rigi.velocity = Vector3.up * vSpeed;
        //Vector3 movement = new Vector3(0, 0, 0);

        //vSpeed -= gravity * Time.deltaTime;

        //movement.y = vSpeed;



    }
}
