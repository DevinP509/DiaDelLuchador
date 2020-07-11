using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
///  for FireBall Auto Delete 
/// </summary>
public class TurretFireballBehavior : MonoBehaviour
{
    Stopwatch stopwatch = new Stopwatch();
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        stopwatch.Start();
        rb = GetComponent<Rigidbody>();
        rb.velocity = (new Vector3(10, 0, 0));
    }

    //Update is called once per frame
    void Update()
    {
        if (stopwatch.ElapsedMilliseconds > 4000)
        {
            Destroy(gameObject);
        }
    }
}
