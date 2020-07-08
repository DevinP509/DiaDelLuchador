using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAnimator : MonoBehaviour
{
    public Texture[] textures;
    int currentFrame = 0;
    Renderer renderer;
    float holderTime;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > holderTime + 1)
        {
            if(textures.Length > currentFrame)
            {
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
            
            
             renderer.material.mainTexture = textures[currentFrame];
        }
       
    }
}
