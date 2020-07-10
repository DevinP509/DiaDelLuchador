    using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;
//Manages the spawning and Randomization of platforms
public class PlatformSpawnerBehavior : MonoBehaviour
{
    public List<GameObject> PlatformPreFabs;
    public float platformFallRate;
    
    System.Random random = new System.Random();
    public GameObject enemycounter;
    private EnemeyDetector enemeyDetector;

    // the fall rate of all platforms when there are no enemys
    public float FallRateNoEnemys;
    
  

    // Start is called before the first frame update
    void Start()
    {
        enemeyDetector = enemycounter.GetComponent<EnemeyDetector>();
       
    }

    // Update is called once per frame
    void Update()
    {
        //checks if enemys 
        if(enemeyDetector.count == 0)
        {
            //set speed if there are no enemys
            if (enemeyDetector.count <= 0)
            {

                PlatformFallingBehavior[] fallingplatforms = GetComponentsInChildren<PlatformFallingBehavior>();
                for (int i = 0; i < fallingplatforms.Length; i++)
                {
                    fallingplatforms[i].fallSpeed = FallRateNoEnemys;
                }
            }
            //set fall rate with enemys
            else
            {
                PlatformFallingBehavior[] fallingplatforms = GetComponentsInChildren<PlatformFallingBehavior>();
                for (int i = 0; i < fallingplatforms.Length; i++)
                {
                    fallingplatforms[i].fallSpeed = platformFallRate;
                }
            }
        }
     
    }

    public void SpawnOnDestroy()
    {
        int number = random.Next(0, PlatformPreFabs.Count);
        UnityEngine.Debug.Log(number);
        GameObject platform = PlatformPreFabs[number];
        platform.GetComponent<PlatformFallingBehavior>().fallSpeed = platformFallRate;
        Instantiate(platform, transform);
    }
}
  

