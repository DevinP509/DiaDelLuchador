using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderAnimator : MonoBehaviour
{
    public Sprite[] textures;
    int currentFrame = 0;
    public SpriteRenderer SpriteRender;
    float holderTime;
    // Start is called before the first frame update
    void Start()
    {
        holderTime = Time.time;
        //renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > holderTime + .1)
        {
            if(textures.Length -1 == currentFrame)
            {
                currentFrame = 0;
            }
            else
            {
                currentFrame++;
            }
            holderTime = Time.time;

            SpriteRender.sprite = textures[currentFrame];
             //SpriteRender.material.mainTexture = textures[currentFrame];
        }
       
    }
}
