using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// Makes The FireBalls Go up and delete after a period of time
/// </summary>
public class FireballBehavior : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start();
        rb = GetComponent<Rigidbody>();
        rb.velocity= (new Vector3(0, 10, 0));
    }
    // Update is called once per frame
    void Update()
    {
        if (stopwatch.ElapsedMilliseconds > 10000)
        {
            Destroy(gameObject);
        }
    }
}
