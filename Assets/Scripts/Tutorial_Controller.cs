using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial_Controller : MonoBehaviour
{

    public Text dialogue;
    public Text CountText;
    public Text MaterialsText;
    public Text timeText;
    public GameObject Spawner;

    public GameObject Floor;

    private int index;
    private int rock, metal, gold;
    private bool canPressEnter = true;
    private bool MoveScene = false;
    private bool requinmentMade = false;
    private int currentCount = 0;

    void Start()
    {
        index = 1;
        CountText.enabled = false;
        CountText.text = "Pick-Up Count: " + GameManager.GetTutCount().ToString() + " / 5";
        MaterialsText.enabled = false;
        MaterialsText.text = "Rock:      " + rock.ToString() + "      Metal:      " + metal.ToString() + "      Gold:      " + gold.ToString();
        timeText.enabled = false;
    }

    void Update()
    {
        GetSaying(index);
        if (requinmentMade)
        {
            index++;
            requinmentMade = false;
        }
        if (Input.GetKey(KeyCode.Return) && canPressEnter)
        {
            index++;
            canPressEnter = false;
        }
    }
   
    void GetSaying(int index)
    {
        switch(index)
        {
            case 0:
                if (Input.GetKey(KeyCode.Return))
                {
                    index++;
                }
                canPressEnter = true;
                break;
            case 1:
                dialogue.text = "Welcome! I see that you're new here. Let's get down to business shall we? Just ENTER";
                canPressEnter = true;
                break;
            case 2:
                canPressEnter = false;
                dialogue.text = "Let's start off with the basics. Use the arrow keys to move around and space to jump. " +
                    "Our carts are specially designed to follow your every movement! " + "Go ahead, give it a try!" +
                    " Go to the settings under options in the main menu if you want to use WASD instead.";
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)
                    || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    requinmentMade = true;
                }
                break;
            case 3:
                requinmentMade = false;
                dialogue.text = "Excellent work! Now, your duty is to collect these mysterious objects known as Pick-Ups. "
                    + "Pick-Ups are white rotating objects that like to float around, you got to act fast though. " +
                    "Some of them like to run away. For now, collect some Pick-Ups.";
                Spawner.SetActive(true);
                if (GameManager.GetTutCount() >= 2)
                {
                    requinmentMade = true;
                }

                break;
            case 4:
                CountText.text = "Objects Count: " + GameManager.GetTutCount().ToString() + " / 5";
                CountText.enabled = true;
                dialogue.text = "See that above? The number on top of the dash is the number of objects you've collected."
                    + " The number after the dash is the number of objects you have to collect to win!";
                if(GameManager.GetTutCount() >= 5)
                {
                    Spawner.SetActive(false);
                    CountText.color = Color.yellow;
                    timeText.enabled = true;
                    dialogue.text = "When the counter turns yellow, it means already you've won. " 
                        + "However, you also have a timer (see top right corner). If this gets to zero and you didn't "
                        + "collect enough objects, you lose. Press ENTER to continue.";
                    canPressEnter = true;
                }
                
                break;
            case 5:
            case 6:
                CountText.enabled = false;
                timeText.enabled = false;

                SetMaterials();
                MaterialsText.text = "Rock:      " + rock.ToString() + "      Metal:      " + metal.ToString() + "      Gold:      " + gold.ToString();
                MaterialsText.enabled = true;

                dialogue.text = "Above, you can see what types of objects you've collected. You can only see this in the indoors. "
                    + "You know you are inside when you are out of the minecart. Inside, you can collide with the cart and upgrade its speed."
                    + " Press ENTER to continue.";
                canPressEnter = true;
                break;
            case 7:
                GameManager.LaunchedTutorial();
                dialogue.text = "OH NO! WHAT'S GOING ON!?";
                Floor.SetActive(false);
                canPressEnter = true;
                break;
            case 8:
                SceneManager.LoadScene(GameManager.GetSceneNumber());
                break;
            default:
                break;
        }
    }

    void SetMaterials()
    {
        int RandNum = 0;

        for (int i = currentCount; i < GameManager.GetTutCount(); ++i)
        {
            RandNum = Random.Range(1, 10);
            if (RandNum >= 1 && RandNum <= 5)
            {
                rock++;
            }
            if (RandNum >= 6 && RandNum <= 8)
            {
                metal++;
            }
            if (RandNum == 9 || RandNum == 10)
            {
                gold++;
            }
            currentCount++;
        }
    }
}
