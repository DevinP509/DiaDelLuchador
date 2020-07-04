using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningBehavior : MonoBehaviour
{
     public List<GameObject> EnemySelection;
     private int WillThisSpawn;
     private int enemySelected;
    public int SpawnChance;
    // Start is called before the first frame update
    void Start()
    {
        Random.Range(0, 10);
        WillThisSpawn = Random.Range(0, 100);
        enemySelected = Random.Range(0, EnemySelection.Count);
       
        if(WillThisSpawn > 70)
        {
            Instantiate(EnemySelection[enemySelected],gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
