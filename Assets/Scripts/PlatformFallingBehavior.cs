using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlatformFallingBehavior : MonoBehaviour
{
    public float fallSpeed;

    public GameObject platformspawner;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlatformDestroyer"))
        {
           
            Destroy(gameObject);
        }
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y-fallSpeed, transform.position.z);
    }
}
