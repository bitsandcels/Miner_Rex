using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Selection : MonoBehaviour
{
    public Text toDisplay;
    public GameObject TogglesScreen;

    public void Play()
    {
        if (GameManager.DidPlayTutorial())
        {
            SceneManager.LoadScene(4);
        }
        else
            SceneManager.LoadScene(GameManager.GetSceneNumber());
    }

    public void Options()
    {
        SceneManager.LoadScene(3);
    }

    public void Quit()
    {
        Application.Quit();
    }
	
    public void Controls()
    {
        CheckIfToggleActive();
        toDisplay.text = "Use the following:\n\n    the arrow keys or WASD to move\n\n    space to jump\n\n    esc to go the menu" 
                           + " \n\n You can change to arrow keys or WASD keys under Settings.";
    }

    public void Objective()
    {
        CheckIfToggleActive();
        toDisplay.text = "-Collect as many cubes as you can.\n\n" +
                        "-When ever you are inside (outside of your cart),\n" +
                        " you can see what types of cubes you've collected. \n\n" +
                        "-Use the rectangle panels to warp between inside \n and outside.\n\n" +
                        "-Warping will restart collect level,\n so quit from the main menu";
    }

    public void Return()
    {
        SceneManager.LoadScene(0);
    }


    public void Toggle_WASD_Keys()
    {
        GameManager.UseWASDinstead();
    }

    public void Toggle_Arrow_Key()
    {
        GameManager.DoUseArrowKeys();
    }

    public void Settings()
    {
        toDisplay.enabled = false;
        TogglesScreen.SetActive(true);
    }


    private void CheckIfToggleActive()
    {
        if (TogglesScreen.activeSelf == true)
        {
            TogglesScreen.SetActive(false);
            toDisplay.enabled = true;
        }
    }
}
