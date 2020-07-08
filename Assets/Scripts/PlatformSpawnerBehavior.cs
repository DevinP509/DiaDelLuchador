    using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using System;

public class PlatformSpawnerBehavior : MonoBehaviour
{
    public List<GameObject> PlatformPreFabs;
    public float SpawnTime;
    public float platformFallRate;
    Stopwatch stopwatch = new Stopwatch();
    System.Random random = new System.Random();
    public GameObject enemycounter;
    private EnemeyDetector enemeyDetector;
    public float FallRateNoEnemys;
    
    private int countHold;

    // Start is called before the first frame update
    void Start()
    {
        enemeyDetector = enemycounter.GetComponent<EnemeyDetector>();
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
      
        if(enemeyDetector.count != countHold || enemeyDetector.count == 0)
        {
            if (enemeyDetector.count <= 0)
            {

                PlatformFallingBehavior[] fallingplatforms = GetComponentsInChildren<PlatformFallingBehavior>();
                for (int i = 0; i < fallingplatforms.Length; i++)
                {
                    fallingplatforms[i].fallSpeed = FallRateNoEnemys;
                }
            }
            else
            {
                PlatformFallingBehavior[] fallingplatforms = GetComponentsInChildren<PlatformFallingBehavior>();
                for (int i = 0; i < fallingplatforms.Length; i++)
                {
                    fallingplatforms[i].fallSpeed = platformFallRate;
                }
            }
        }
     
        countHold = enemeyDetector.count;
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
  

