using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Holds the code for keeping track of the players score and lives
/// </summary>
public class ValueKeepingBehavior : MonoBehaviour
{
    /// <summary>
    /// Set the base value for the score and lives
    /// </summary>
    public float score = 0;
    public float lives = 3;

    /// <summary>
    /// Create a refrence to the text values
    /// </summary>
    public Text scoreValue;
    public Text livesValue;
    public float Difficulty = 0;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        scoreValue.text = "Score: " + score.ToString();
        livesValue.text = "Lives: " + lives.ToString();
        Difficulty = score / 2;
    }
}