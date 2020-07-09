using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    public GameObject player;

    public GameObject fireballPrefab;

    public int speed;

    public float firerate;

    private Rigidbody rb;

    Stopwatch stopwatch = new Stopwatch();
    public GameObject FrameOne;
    public GameObject FrameTwo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        stopwatch.Start();
    }
    void followplayer()
    {

        //Get this agent's position
        Vector3 pos = transform.position;
        //Get the position of the target agent
        Vector3 targetPos = player.transform.position;

        //Calculate the vector describing the direction to the target and normalize it
        Vector3 direction = targetPos - pos;
        direction = direction.normalized;
        //Multiply the direction by the speed we want the agent to move
        Vector3 force = direction * speed;
        //Subtract the agent's current velocity from the result to get the force we need to apply
        force = force - rb.velocity;

        //Return the force
        rb.AddForce(force);
    }
    private void fire()
    {
        if (stopwatch.ElapsedMilliseconds > (10000 / firerate))
        {
            Instantiate(fireballPrefab,transform);

            FrameOne.SetActive(false);
            FrameTwo.SetActive(true);
            stopwatch.Restart();
        }
    }
    // Update is called once per frame
    void Update()
    {
        firerate = FindObjectOfType<ValueKeepingBehavior>().Difficulty;
        followplayer();
        fire();
        if(stopwatch.ElapsedMilliseconds > (10000 / (firerate *4)))
        {
            FrameOne.SetActive(true);
            FrameTwo.SetActive(false);
        }
    }
 
}
