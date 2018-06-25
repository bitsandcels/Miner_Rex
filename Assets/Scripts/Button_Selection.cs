using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Selection : MonoBehaviour
{
    public Text toDisplay;
    public GameObject TogglesScreen;

    public GameObject main_menu_UI, options_UI;
    public Toggle WASD_Toggle, Arrow_Toggle;

    void Start()
    {
        main_menu_UI = GameObject.Find("UI_Menu_Overlay");
        options_UI = GameObject.Find("UI_Options_Holder");

        options_UI.SetActive(false);
        Arrow_Toggle.isOn = true;
        Arrow_Toggle.interactable = false;
        WASD_Toggle.isOn = false;
        WASD_Toggle.interactable = true;
    }

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
        main_menu_UI.SetActive(false);
        options_UI.SetActive(true);
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
        options_UI.SetActive(false);
        main_menu_UI.SetActive(true);      
    }


    public void Toggle_WASD_Keys()
    {
        Arrow_Toggle.interactable = true;
        Arrow_Toggle.isOn = false;
        WASD_Toggle.isOn = true;
        WASD_Toggle.interactable = false;

        GameManager.UseWASDinstead();
    }

    public void Toggle_Arrow_Key()
    {
        WASD_Toggle.interactable = true;
        WASD_Toggle.isOn = false;
        Arrow_Toggle.isOn = true;
        Arrow_Toggle.interactable = false;
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
