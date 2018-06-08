using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float Speed;
    public float RotationSpeed;
    public bool playMetalSound;

    public bool TutorialActive;
    public int tutorialCount;

    private bool isGrounded;
    private bool decreaseTimer;
    private Vector3 Jump = new Vector3(0.0f, 20.0f + GameManager.GetJumpBonus(), 0.0f);
    private bool canMove;

    public AudioClip[] impact = new AudioClip[2];
    AudioSource audioSource;

    public GameObject[] Spawners;

    public Text countText, timerText, conditionText;
    public Text rockCountText, metalCountText, goldCountText;
    public Text PressSpace; //Use this to display "Press space to continue"

    void Start()
    {
        canMove = true;

        Speed += GameManager.GetSpeedBonus();

        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlayClipAndChange", 0.01f, 15.0f);

        conditionText.enabled = false;
        PressSpace.enabled = false;

        decreaseTimer = false;


        if (GameManager.GetSceneNumber() == 2) // if player is inside
        {
            PressSpace.enabled = false;
            Jump.y = 5.0f;
            SetMaterialsText();
        }

        if (GameManager.GetSceneNumber() == 1)
        {
            if (GameManager.RemindThePlayer())
            {
                RemindToCheckText();
            }
            SetCountText();
            SetTimerText(decreaseTimer);
        }

        if (GameManager.isFromMainMenu())
        {
            decreaseTimer = true;
            transform.position = GameManager.GetPreviousePos();           
            GameManager.SetMainMenuBool(false);
        }

        if (GameManager.isDoorActive())
        {
            decreaseTimer = true;
            canMove = true;
            transform.position = GameManager.GetOutsidePos();
            transform.Rotate(GameManager.GetOutsideAngle());
            GameManager.SetDoorActive(false);
        }
    }

    void Update()
    {
        if (GameManager.GetSceneNumber() == 2)
        {
            SetMaterialsText();
        }
        if (GameManager.GetSceneNumber() == 1)
        {
            SetTimerText(decreaseTimer);

            if (GameManager.GetTimer(false) == 0)
            {
                decreaseTimer = false;
                if (GameManager.GetCount() >= GameManager.GetMinCount())
                {                    
                    GameManager.IncreaseMinCount();                                       
                }
                GameManager.ResetCount();
                GameManager.ResetTimer(120.0f);
                conditionText.enabled = true;
                PressSpace.enabled = true;
                canMove = false;
            }
        }
    }

    void FixedUpdate()
    {

        if ((GameManager.GetUseArrowKeys() ? Input.GetKey(KeyCode.DownArrow) : Input.GetKey(KeyCode.S)) && canMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self); //move back
        }

        if ((GameManager.GetUseArrowKeys() ? Input.GetKey(KeyCode.UpArrow) : Input.GetKey(KeyCode.W)) && canMove)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * -Speed, Space.Self); //move forward
        }

        if ((GameManager.GetUseArrowKeys() ? Input.GetKey(KeyCode.LeftArrow) : Input.GetKey(KeyCode.A)) && canMove)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * -RotationSpeed, Space.Self); // turn left
        }

        if ((GameManager.GetUseArrowKeys() ? Input.GetKey(KeyCode.RightArrow) : Input.GetKey(KeyCode.D)) && canMove)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * RotationSpeed, Space.Self); // turn right
        }

        if (Input.GetKey(KeyCode.Escape) && canMove) // go to main menu
        {
            GameManager.SetMainMenuBool(true);
            GameManager.SetPreviousPos(transform.position);
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // jump
        {
            if (canMove)
            {
                transform.Translate(Jump * Time.deltaTime * Speed, Space.Self);
                isGrounded = false;
            }
            else
            {
                GameManager.SetSceneNumber(2);
                SceneManager.LoadScene(GameManager.GetSceneNumber());
            }
        }
    }

    void OnCollisionEnter()
    {
        if (TutorialActive)
        {
            return;
        }
            if (GameManager.RemindThePlayer())
            {
                decreaseTimer = true; //place here for now
                GameManager.DidRemindPlayer();
                PressSpace.enabled = false;
                PressSpace.text = "Press Space to Continue";
            }

        audioSource.PlayOneShot(impact[0], 0.7F);
    }

    void OnCollisionStay(Collision other)
    {
        isGrounded = true;
    }

    void PlayClipAndChange()
    {
        if (playMetalSound)
        {
            audioSource.PlayOneShot(impact[1], 0.7f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Warp")) //Go to outdoor level
        {
            GameManager.SetSceneNumber(1);
            GameManager.SetDoorActive(true);
            SceneManager.LoadScene(GameManager.GetSceneNumber());
        }
        if (other.gameObject.CompareTag("Warp_In")) //Go to indoor level
        {
            GameManager.SetSceneNumber(2);
            GameManager.SetDoorActive(false);
            SceneManager.LoadScene(GameManager.GetSceneNumber());
        }
        if (other.gameObject.CompareTag("Pick Up")) //Check if it's a pick up object
        {
            other.gameObject.GetComponent<Renderer>().enabled = false;
            Physics.IgnoreCollision(other.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            GameManager.IncreaseCount();
            SetCountText();
        }
        if (other.gameObject.CompareTag("Pick Up Destroy")) //Check if it's a pick up object
        {
            DestroyObject(other.gameObject);
            if(TutorialActive)
            {
                GameManager.IncreaseTutCount();
            }
            else
            {
                GameManager.IncreaseCount();
                SetCountText();
            } 
        }
        if (other.gameObject.CompareTag("Warp_Outdoor2")) //Check if going to area 2
        {
            Spawners[0].SetActive(false);
            Spawners[1].SetActive(false);
            Spawners[2].SetActive(false);

            transform.position = GameManager.GetArea2Pos();
        }

        if (other.gameObject.CompareTag("Warp_Outdoor1")) //Check if going to area 1
        {
            Spawners[0].SetActive(true);
            Spawners[1].SetActive(true);
            Spawners[2].SetActive(true);

            transform.position = GameManager.GetArea1Pos();
            transform.Rotate(GameManager.GetOutsideAngle());
        }

        if (other.gameObject.CompareTag("Rock"))
        {
            audioSource.PlayOneShot(impact[0], 0.7F);
            transform.Rotate(GameManager.GetOutsideAngle());
        }

        if (other.gameObject.CompareTag("Cart"))
        {
            if(GameManager.IncreaseSpeedandJump())
            {
                PressSpace.enabled = true;
                PressSpace.text = "Increased Speed and Jump on cart.";
            }
            else
            {
                PressSpace.enabled = true;
                PressSpace.text = "You need at least " + GameManager.GetMinNumMatieral("Rock").ToString() + " Rocks, " +
                    GameManager.GetMinNumMatieral("Metal").ToString() + " Metal and " + GameManager.GetMinNumMatieral("Gold").ToString() + " Gold.";
            }
        }

        if(other.gameObject.CompareTag("To Main Level"))
        {
            GameManager.SetSceneNumber(1);
            SceneManager.LoadScene(GameManager.GetSceneNumber());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Cart"))
        {
            PressSpace.enabled = false;
        }
    }

    void SetCountText()
    {
        countText.text = "Pick-Up Count: " + GameManager.GetCount().ToString() + " / " + GameManager.GetMinCount().ToString();
        if (GameManager.GetCount() >= GameManager.GetMinCount())
        {
            conditionText.text = "You Win!";
            countText.color = Color.yellow;
        }
    }

    void SetTimerText(bool doDecrease)
    {
        timerText.text = "Time Left: " + GameManager.GetTimer(doDecrease).ToString() + " Sec";
    }

    void SetMaterialsText()
    {
        GameManager.SetMaterials();
        rockCountText.text = "Rocks: " + GameManager.GetNumMatieral("Rock").ToString();
        metalCountText.text = "Metals: " + GameManager.GetNumMatieral("Metal").ToString();
        goldCountText.text = "Gold: " + GameManager.GetNumMatieral("Gold").ToString();
    }

    void RemindToCheckText()
    {
        PressSpace.text = "Remember to check the Main Menu and Options for Game Info!";
        PressSpace.enabled = true;
    }
}



