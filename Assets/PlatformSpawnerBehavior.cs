using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class PlatformSpawnerBehavior : MonoBehaviour
{
    public List<GameObject> PlatformPreFabs;
    public float SpawnTime;
    public float platformFallRate;
    Stopwatch stopwatch = new Stopwatch();
     UnityEngine.Random random = new Random();
    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(stopwatch.ElapsedMilliseconds /1000 >= SpawnTime)
        {
            GameObject platform = PlatformPreFabs[UnityEngine.Random.Range(0, PlatformPreFabs.Count - 1)];
            platform.GetComponent<PlatformFallingBehavior>().fallSpeed = platformFallRate;
            Instantiate(platform, transform);
            
            stopwatch.Restart();
        }
    }
}
