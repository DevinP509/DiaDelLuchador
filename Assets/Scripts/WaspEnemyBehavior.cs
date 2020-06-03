using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaspEnemyBehavior : MonoBehaviour
{
    public GameObject player;
    public int speed;
    public int Damage;
    public int Health;
    public Camera GameCamera;
    public Rigidbody rb;
    private Renderer rendererer;
    private BoxCollider collider;

    [SerializeField]
    //Refrence to the ValueKeepingBehavior
    private ValueKeepingBehavior scoreKeep;

    // Start is called before the first frame update
    void Start()
    {
        GameCamera = UnityEngine.Camera.main;
        rendererer = GetComponent<Renderer>();
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        ChasePlayer();


        if (Health <= 0)
        {
            die();
        }
    }
    private void ChasePlayer()
    {
        if (CheckIfOnScreen() == true)
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

    }
    public void TakeDamage(int damgage)
    {

    }
    private bool CheckIfOnScreen()
    {

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(GameCamera);
        if (GeometryUtility.TestPlanesAABB(planes, collider.bounds))
            return true;
        else
            return false;
    }



    private void die()
    {
        gameObject.SetActive(false);

        //increase the players score
        scoreKeep.score++;
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PunchBox"))
        {
            Health--;
            rb.AddForce(-rb.velocity * 10, ForceMode.Impulse);
        }
    }

}
