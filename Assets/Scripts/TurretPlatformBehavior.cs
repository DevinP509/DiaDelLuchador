using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class TurretPlatformBehavior : MonoBehaviour
{
    //Refrence to the fireball prefab
    public GameObject fireballPrefab;

    //Variable to control firerate
    public int firerate;

    //Create a new stopwatch
    Stopwatch stopwatch = new Stopwatch();

    //Start is called before the first frame update
    void Start()
    {
        //Start the stopwatch
        stopwatch.Start();
    }

    //Shoot the fireball
    private void fire()
    {
        //if the stopwatch elapsed time is greater than 10K divied by the firerate
        if (stopwatch.ElapsedMilliseconds > (10000 / firerate))
        {
            //Create the fireball
            Instantiate(fireballPrefab, transform);

            //Stop watch restart
            stopwatch.Restart();
        }
    }

    //Update is called once per frame
    void Update()
    {
        fire();
    }
}