using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningBehavior : MonoBehaviour
{
     public List<GameObject> EnemySelection;
     private System.Random Number= new System.Random();

     private int WillThisSpawn;
     private int enemySelected;
    // Start is called before the first frame update
    void Start()
    {
        WillThisSpawn = Number.Next(0, 10);
        enemySelected = Number.Next(0, EnemySelection.Count);
        if(WillThisSpawn > 4)
        {
            Instantiate(EnemySelection[enemySelected], transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
