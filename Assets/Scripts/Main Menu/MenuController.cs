using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;

    public GameObject optionsMenu;
    public GameObject graphicsMenu;

    // Update is called once per frame
    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)) //The key for bringing up the pause menu
        {
            if(isPaused)// if isPaused is true then resume the game
            {
                ResumeGame(); // Calls to the function, resuming game
            }else// if isPaused is false then pause the game
            {
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;  // Time pauses
            }
        }
    }

    public void ResumeGame()// opposite of the else statement, resuming the game
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        graphicsMenu.SetActive(false);
        Time.timeScale = 1f; // time continues, set to standard speed
    }

    public void RestartGame()// restarts scene
    {
        Time.timeScale = 1f;// sets time back to normal since game would be paused (at 0f) when user clicks restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);//re loads current scene, to be used for multiple scenes rather than a specific name
    }


    public void ReturnToMain()
    {
        Time.timeScale = 1f; //prevents time being stopped when going back into the menu
        SceneManager.LoadScene(mainMenuScene);// loads main menu scene
    }

    // Menu switching to options and graphics screen, sets screens visibility off and on
    public void ActivateGraphicsSettings()
    {
        optionsMenu.SetActive(false);
        graphicsMenu.SetActive(true);
    }

    public void ReturnToOptionsScreen()
    {
        optionsMenu.SetActive(true);
        graphicsMenu.SetActive(false);
    }
}
