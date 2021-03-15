using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    // the int that indicates how many goals are touched synchronously
    private int activatedNum = 0;
    
    public int ActivatedNum
    {
        get { return activatedNum; }
        set
        {
            activatedNum = value; 
            Debug.Log("Activated Number: " + activatedNum);
        }
    }

    // set the goalNum to -1, so that goalNum != activatedNum in the beginning
    private int goalNum = -1;
    
    public int GoalNum
    {
        get { return goalNum; }
        set
        {
            goalNum = value;
            Debug.Log("Goal Number: " + goalNum);
        }
    }

    private void Awake()
    {
        // singleton
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        //when all the goals are activated
        if (ActivatedNum == GoalNum)
        {
            GameManager.instance.GetComponent<ASCIILevelLoader>().CurrentLevel++;
            
        }
    }
    
}
