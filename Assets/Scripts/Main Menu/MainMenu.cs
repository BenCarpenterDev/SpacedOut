using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{


    public void PlayGame1 ()
    {
        SceneManager.LoadScene("TestMap");
        // A public variable which allows to load a specific level, named "PlayGame1", for the first level
    }

    public void PlayGame2()
    {
        SceneManager.LoadScene("TerrainTest");
    }

    public void QuitGame ()
    {
        Application.Quit();
        // a public variable for exiting the game
    }

}
