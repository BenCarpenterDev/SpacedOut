using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour
{
    public Toggle fullscreenToggle; // toggle variable for fullscreen
    public Toggle vSyncToggle;//toggle variable for vSync

    public ResolutionType[] resolutions;// variable for resolution types, able to list options within inspector, the amount of resolution options
    public int selectedRes;// integer variable, stores currently selected number from ResolutionType
    //each resolution option will be numbered, 0 to 4.The left and right functions determine which number the selectedRes is on

    public Text resText;// variable for displaying currently selected resolution type

    // Start is called before the first frame update
    void Start()
    {
        // to check for current resolution and to display current resolution:
        for(int i = 0; i < resolutions.Length; i++)// if resolution length is greater than selected resolution
        {
            if(resolutions[i].width == Screen.width & resolutions[i].height == Screen.height)// if current screen resolution = the resolution variable
            {
                selectedRes = i;
                resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height;// display current resolution
            }
        }

        fullscreenToggle.isOn = Screen.fullScreen; // enables fullscreen mode

        if(QualitySettings.vSyncCount == 0)
        {
            vSyncToggle.isOn = false;
        }else
        {
            vSyncToggle.isOn = true;
        }
    }

// Resolution Controls
    public void ResolutionLeft() // when the user presses left within the resolution settings
    {
        if(selectedRes > 0) // if selectedRes is greater than 0
        {
            selectedRes--;// then minus SelectedRes by 1, goes down the resolution options
        }

        resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height; // display current settings

        //"SetResolution();" this is obselete due to adding apply changes function.
        // Disabled so it doesn't set current settings straight away
    }

    public void ResolutionRight() // when the user presses right within the resolution settings
    {
        if (selectedRes < resolutions.Length - 1) // if selectedRes is less than (the max amount of resolution options)  minus 1
        {
            selectedRes++;// then add SelectedRes by 1, goes up the resolution options
        }

        resText.text = resolutions[selectedRes].width + " x " + resolutions[selectedRes].height; // display current settings

        //SetResolution();
    }

    public void SetResolution() // Sets the current resolution settings
    {
        Screen.SetResolution(resolutions[selectedRes].width, resolutions[selectedRes].height, fullscreenToggle.isOn);
    }


    // Apply and Save Settings
    public void ApplyChanges() // Applies currently selected display settings
    {
        SetResolution();
        ApplyFullScreen();
        ApplyVsync();
    }// calls to these functions/methods


    // Full Screen and Vsync Toggle controls
    public void ApplyFullScreen() // Fullscreen function
    {
        if(fullscreenToggle.isOn)
        {
            Screen.fullScreen = true;//if toggle is on, then fullscreen is activated

        }else
        {
            Screen.fullScreen = false;//if toggle is off, then fullscreen is disabled
        }
    }

    public void ApplyVsync() // vSync function
    {
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;//if toggle is on, then vSync is activated
        } else
        {
            QualitySettings.vSyncCount = 0;//if toggle is off, then vSync is disabled
        }
    }
}
