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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
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
