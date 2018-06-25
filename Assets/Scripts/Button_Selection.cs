using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button_Selection : MonoBehaviour
{
    public Text toDisplay;
    public GameObject TogglesScreen;
    public Button KeysToUse;

    public GameObject main_menu_UI, options_UI;
    public Toggle WASD_Toggle, Arrow_Toggle;

    bool UseWASDKeys;

    void Start()
    {
        UseWASDKeys = true;
        GameManager.UseWASDinstead();
    }

    public void Play()
    {
        if (GameManager.DidPlayTutorial())
        {
            SceneManager.LoadScene(3);
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
        GameManager.UseWASDinstead();
    }

    public void Toggle_Arrow_Key()
    {
        GameManager.DoUseArrowKeys();
    }

    public void SetKeys()
    {
        UseWASDKeys = !UseWASDKeys;
        UpdateKeys();
    }

    void UpdateKeys()
    {
        if(KeysToUse == null)
        {
            KeysToUse = GameObject.Find("Keys_Button").GetComponent<Button>();
        }

        if(UseWASDKeys)
        {
            KeysToUse.GetComponentInChildren<Text>().text = "Using WASD Keys";
            GameManager.UseWASDinstead();
        }
        else
        {
            KeysToUse.GetComponentInChildren<Text>().text = "Using Arrow Keys";
            GameManager.DoUseArrowKeys();
        }

        
    }

    public void Settings()
    {
        toDisplay.text = "Push the button below to select which keys to use.";
        TogglesScreen.SetActive(true);
    }


    private void CheckIfToggleActive()
    {
        if (TogglesScreen.activeSelf == true)
        {
            TogglesScreen.SetActive(false);
        }
    }
}
