using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Holds the code to quit the game and change scenes
/// </summary>
public class MainMenuBehavior : MonoBehaviour
{
    /// <summary>
    /// Load the desired scene
    /// </summary>
    /// <param name="sceneBuildIndex"></param>
    /// 
    
    public void loadScene(int sceneBuildIndex)
    {
        
        SceneManager.LoadScene(0);
        SceneManager.LoadScene(sceneBuildIndex);
    }

    /// <summary>
    /// Quit the game
    /// </summary>
    public void quitGame()
    {
        Application.Quit();
    }
}