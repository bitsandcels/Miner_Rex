using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    private static bool PlayCaveInSound = true;

    private static bool RemindPlayer = true;

    //Tell if the player is coming from the main menu scene
    private static bool FromMainMenu = false; 

    //Tell if a player use the door to go from out to in
    private static bool DoorActive = false; 

    //A fixed position to set the player at when leaving the indoor level
    private static Vector3 OutsidePos = new Vector3(8.230419f, -3.455008f, 36.97776f);

    //A fixed angle to set the player at when leaving the indoor level
    private static Vector3 OutsideAngle = new Vector3(0.0f, 90.0f, 0.0f);

    //Use for Main Menu, holds the position the player was just at
    private static Vector3 PreviousPos = new Vector3(0.0f, 0.0f, 0.0f);

    //A fixed position to set the player at when leaving the second area
    private static Vector3 Area1Pos = new Vector3(-70.07563f, -10.29237f, -7.980728f);

    //A fixed position to set the player at when leaving the first area
    private static Vector3 Area2Pos = new Vector3(-29.29995f, -13.87317f, 73.20026f);

    //Holds the current scene the player is at
    private static int CurrentScene = 1;

    private static float timer = 120.0f;

    //Holds the number of objects picked up
    private static int count = 0;

    //Holds the minimum numbers to be collected
    private static int minCount = 5;

    //Count for tutorial only
    private static int tutCount = 0;

    //Possible materials the player can get
    private static int rock = 0;
    private static int metal = 0;
    private static int gold = 0;

    private static int minRock = 5;
    private static int minMetal = 2;
    private static int minGold = 0;

    //Save the last number of objects collected to reset count
    private static int LastPlaceObjects = 0;

    //Save total number of objects collected
    private static int numObjectsCollected = 0;

    private static bool PlayTutorial = true;

    private static float PlayerSpeed = 0;

    private static float Jump = 0;

    private static bool UseArrowKeys = true;

    public static void SetPreviousPos(Vector3 PrePos)
    {
        PreviousPos = PrePos; //Set Previous Posiiton
    }
    public static Vector3 GetPreviousePos()
    {
        return PreviousPos; //Return Previous Position
    }

    public static Vector3 GetOutsideAngle()
    {
        return OutsideAngle; // Return outside angle
    }
    public static Vector3 GetOutsidePos()
    {
        return OutsidePos; //Return outside position
    }

    public static Vector3 GetArea1Pos()
    {
        return Area1Pos; //Return outside position
    }

    public static Vector3 GetArea2Pos()
    {
        return Area2Pos; //Return outside position
    }

    public static void SetSceneNumber(int scene)
    {
        CurrentScene = scene; //set current scene
    }
    public static int GetSceneNumber()
    {
        return CurrentScene; //return current scene
    }

    public static void SetMainMenuBool(bool setTo)
    {
        FromMainMenu = setTo; //flip between true and false
    }
    public static bool isFromMainMenu()
    {
        return FromMainMenu; //return if the player came from the main menu
    }

    public static void SetDoorActive(bool setTo)
    {
        DoorActive = setTo; //flip between true and false
    }
    public static bool isDoorActive()
    {
        return DoorActive; //return if the door is active to set a player at a position
    }

    public static void ResetTimer(float amount)
    {
        timer = amount;
    }

    public static int GetTimer(bool doDecrease)
    {
        if (doDecrease) //if the timer should be decreased
        {
            timer -= Time.deltaTime; //decrease timer
        }
        return Mathf.RoundToInt(timer);
    }

    public static int GetCount()
    {
            return count;
    }
    public static void IncreaseCount()
    {
        count += 1;
    }

    public static void IncreaseTutCount()
    {
        tutCount += 1;
    }
    public static int GetTutCount()
    {
        return tutCount;
    }

    public static int GetMinCount()
    {
        return minCount;
    }

    public static void IncreaseMinCount()
    {
        minCount += 5;
    }

    public static bool IncreaseSpeedandJump()
    {
        if (rock >= minRock && metal >= minMetal && gold >= minGold)
        {
            PlayerSpeed += 0.5f;
            Jump += 0.5f;
            rock -= minRock;
            metal -= minMetal;
            gold -= minGold;

            minRock += 2;
            minMetal += 1;
            minGold += 1;

            return true;
        }

        return false;
    }
    public static float GetSpeedBonus()
    {
        return PlayerSpeed;
    }
    public static float GetJumpBonus()
    {
        return Jump;
    }

    public static void SetMaterials()
    {
        int RandNum = 0;

        for (int i = LastPlaceObjects; i < numObjectsCollected; ++i)
        {
            RandNum = Random.Range(1, 10);
            if (RandNum >=1 && RandNum <= 5)
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

            LastPlaceObjects++;
        }
    }
    public static int GetNumMatieral(string material)
    {
      if(material == "Rock")
      {
        return rock;
      }

      else if(material == "Metal")
      {
        return metal;
      }

      else if (material == "Gold")
      {
         return gold;
      }

      else return 0;
    }

    public static int GetMinNumMatieral(string material)
    {
        if (material == "Rock")
        {
            return minRock;
        }

        else if (material == "Metal")
        {
            return minMetal;
        }

        else if (material == "Gold")
        {
            return minGold;
        }

        else return 0;
    }

    public static bool RemindThePlayer()
    {
        return RemindPlayer;
    }
    public static void DidRemindPlayer()
    {
        RemindPlayer = false;
    }

    public static bool DidPlayTutorial()
    {
        return PlayTutorial;
    }
    public static void LaunchedTutorial()
    {
        PlayTutorial = false;
    }

    public static void ResetCount()
    {
        numObjectsCollected = count;
        count = 0;
    }

    public static void DoUseArrowKeys()
    {
        UseArrowKeys = true;
    }
    public static void UseWASDinstead()
    {
        UseArrowKeys = false;
    }
    public static bool GetUseArrowKeys()
    {
        return UseArrowKeys;
    }

    public static bool DidPlayCaveInSound()
    {
        if (PlayCaveInSound)
        {
            PlayCaveInSound = false;
            return true;
        }
        else
        {
            return PlayCaveInSound;
        }
    }

}
