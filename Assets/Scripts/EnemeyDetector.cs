using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Detect How Many Enemys are currently on screen
/// </summary>
public class EnemeyDetector : MonoBehaviour
{ 
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            count++;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            count--;
        }
    }
}
