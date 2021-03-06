using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EventManager : MonoBehaviour
{

    #region variable
    public bool IsEventRunning;
    public Event_Catagory RunningEvent;
    public bool IsPaidVoting;
    public GameObject createDatePanel;
    public GameObject paidDatePanel;

    
    #endregion

    #region singletone
    public static EventManager instance = null;
    private void Awake()
    {
        if (EventManager.instance == null)
            EventManager.instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion

    void Start()
    {
        IsEventRunning = false;
        if(createDatePanel!=null)
        createDatePanel.SetActive(true);
        if(paidDatePanel != null)
        paidDatePanel.SetActive(false);

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StartAnEvent(false);
        }
        SetEventBackground();
    }

    public void StartAnEvent(bool _IsPaidVoting)
    {
        IsEventRunning = true;
        RunningEvent = (Event_Catagory)Random.Range(0, System.Enum.GetValues(typeof(Event_Catagory)).Length);
        MapManager.instance.ChangeBTNtext();
        Debug.LogWarning(RunningEvent);
        if (_IsPaidVoting)
            IsPaidVoting = true;
        else
            IsPaidVoting = false;
    }
    

    private void SetEventBackground()
    {
        if (RunningEvent.ToString() == "Weeding")
        {
            MapManager.instance.eventImage.sprite = MapManager.instance._eventImage[0];
        }
        else if (RunningEvent.ToString() == "Party")
        {
            MapManager.instance.eventImage.sprite = MapManager.instance._eventImage[1];
        }

        else if (RunningEvent.ToString() == "Office")
        {
            MapManager.instance.eventImage.sprite = MapManager.instance._eventImage[3];
        }
        else if (RunningEvent.ToString() == "Casual") 
        {
            MapManager.instance.eventImage.sprite = MapManager.instance._eventImage[2];
        }
    }


    public void EndEvent()
    {
        DateTimer.timerForDate = 100f;
        Destroy(this.gameObject);
    }
}
