using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour
{
    //public bool isActivated = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //the number of activated goals plus one when it's triggered
        
        GameManager.instance.ActivatedNum++;
        Debug.Log("activated"+GameManager.instance.ActivatedNum);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        //isActivated = false;
        GameManager.instance.ActivatedNum--;
        Debug.Log("not activated"+GameManager.instance.ActivatedNum);
    }
}
