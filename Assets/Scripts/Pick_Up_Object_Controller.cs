using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pick_Up_Object_Controller : MonoBehaviour {

    private int Behavior; // 0 = idle, 1 = moving up, 2 = moving down, 3 = run
    private Vector3 Jump = new Vector3(0.0f, 10.0f, 0.0f);
    private float goUpTimer, goDownTimer, stayStillTimer;

    void Start()
    {
        Behavior = Random.Range(0,1);
        goUpTimer = 2;
        goDownTimer = 2;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
        SetBehavior();
        DoBehavior();
    }

    void SetBehavior()
    {
        if (Behavior == 3)
        {
            return;
        }
        if (Behavior == 0)
        {
            int randNum = Random.Range(1, 20);
            if (randNum > 8) //10% chance of floating
            {
                Behavior = 1;
                return;
            }
            if (randNum <= 7 && gameObject.CompareTag("Pick Up Destroy"))
            {
                Behavior = 3;
            }
        }
    }

    void DoBehavior()
    {
        if (Behavior == 0)
        {
            transform.Rotate(new Vector3(-15, -30, -45) * Time.deltaTime);
            return;
        }
        if (Behavior == 1)
        {
            if (goUpTimer > 0)
            {
                transform.position += new Vector3(0, Time.deltaTime/2, 0);
                goUpTimer -= Time.deltaTime;
            }
            if (goUpTimer <= 0)
            {
                goDownTimer = 1;
                Behavior = 2;
                return;
            }
        }
        if (Behavior == 2)
        {
            if (goDownTimer > 0)
            {
                transform.position += new Vector3(0, -Time.deltaTime/2, 0);
                goDownTimer -= Time.deltaTime;
            }
            if (goDownTimer <= 0)
            {
                goUpTimer = 1;
                Behavior = 0;
                return;
            }
        }

        if(Behavior == 3)
        {
            DetectAndRun();
        }
    }

    void DetectAndRun()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform thisPos = transform;

        thisPos.transform.LookAt(player.transform);
        float distance = Vector3.Distance(thisPos.transform.position, player.transform.position);
        bool tooClose = distance < 5;
        Vector3 direction = tooClose ? Vector3.back : Vector3.forward;
        transform.Translate(direction * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Behavior == 3)
        {
            if (collision.gameObject.CompareTag("Cave"))
            {
                Destroy(this);
            }
        }
    }
}
