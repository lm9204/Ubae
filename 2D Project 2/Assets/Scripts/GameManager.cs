using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject talkPanel;
    public Text talkText;
    public GameObject scanObject;
    public bool isAction;
    
    void Awake()
    {
        
    }

    public void Action(GameObject obj)
    {
        if (isAction)
        { //exit
            isAction = false;
        }
        else
        { //enter
            isAction = true;
            scanObject = obj;
            talkText.text = scanObject.name;
        }
        talkPanel.SetActive(isAction);
    }
}
