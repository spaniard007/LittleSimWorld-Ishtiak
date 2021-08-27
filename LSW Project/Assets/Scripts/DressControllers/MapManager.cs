﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    #region public variable
    public Text instructionText;
    public Button EventAction;
    public Image eventImage;
    public Image decisionPanel;

    public GameObject eventButtonPanel;

    [Space(20)] public List<Sprite> _eventImage;



    [Space(10)] public GameObject goHomeBtn, GoShopBtn;
    #endregion

    #region private variable
    private bool IsButtonClicked;

    #endregion


    #region singletone
    public static MapManager instance = null;
    private void Awake()
    {
        if (MapManager.instance == null)
            MapManager.instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion



    // Start is called before the first frame update
    private void Start()
    {
        ChangeBTNtext();
    }

    private void Update()
    {
        if (EventManager.instance.IsEventRunning == true)
        {
            goHomeBtn.SetActive(true);
            GoShopBtn.SetActive(true);
        }
        else
        {
            goHomeBtn.SetActive(false);
            GoShopBtn.SetActive(false);
        }
        
    }

    public void ChangeBTNtext()
    {
        if (EventManager.instance.IsEventRunning == true)
        {
            EventAction.GetComponentInChildren<Text>().text = "Go for vote";
            instructionText.color = Color.white;
            instructionText.text = "You have to go \"<b> <color=#F76C26>" + EventManager.instance.RunningEvent + "</color></b>\" event. \n <size=18>go HOME to ready or go SHOP to buy a new dress</size>";
            
        }
        else
        {
            EventAction.GetComponentInChildren<Text>().text = "Start Event";
            instructionText.text = "you don't have an event \n<color=red>\"click start\"</color> \n event to get new Event";
        }

        
    }

    public void Clicked()
    {
        if (EventManager.instance.IsEventRunning == true)
        {
            //go for Vot
            SceneChanger.instance.GoTOScene(4);
        }
        ChangeBTNtext();
    }

    public void StartEvent(bool Ispaid)
    {
        EventManager.instance.StartAnEvent(Ispaid);
        
        //eventButtonPanel.SetActive(false);
        Destroy(eventButtonPanel.gameObject);
    }

    // interface based selection if time left
    public void GetDecisionPanel(Image whichPanel)
    {
        decisionPanel = whichPanel;
    }

}
