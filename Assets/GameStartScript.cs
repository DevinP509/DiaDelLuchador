using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartScript : MonoBehaviour
{
    float holderTime;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //holderTime = Time.time;
        Time.timeScale = 0;
        holderTime = Time.realtimeSinceStartup;
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeElapsed = Time.realtimeSinceStartup - holderTime;   
        if(timeElapsed >5)
        {
            text.text = null;
        }
        else if(timeElapsed > 3)
        {
            text.color = Color.red;
            text.text = "Vamos";
            Time.timeScale = 1;
        }
        else if (timeElapsed > 2)
        {
            text.color = Color.Lerp(Color.red,Color.yellow,.5f);
            text.text = "Tres";
        }
        else if (timeElapsed > 1)
        {
            text.color = Color.yellow;
            text.text = "Dos";
        }
        else
        {
            text.text = "Uno";
        }
        
    }
  

}
