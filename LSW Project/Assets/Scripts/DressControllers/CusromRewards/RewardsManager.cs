using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardsManager : MonoBehaviour
{
    private string _currentDate;

    public GameObject rewardsPanel;
    public Text instructionText;


    // Start is called before the first frame update
    void Start()
    {
        rewardsPanel.SetActive(false);
        GetCurrentDate();
    }

    private void Update()
    {
		//for testing
        if (Input.GetKeyDown(KeyCode.E))
            StartCoroutine(_GiveRewards());
    }

    private void GetCurrentDate()
    {
        _currentDate = System.DateTime.Now.ToShortDateString();

//        Debug.Log("Date" + _currentDate);
        IsReadyForRewards();
    }

    private void IsReadyForRewards()
    {
        if (PlayerPrefs.HasKey("GivingRewardsDate"))
        {
            if (PlayerPrefs.GetString("GivingRewardsDate") != _currentDate)
            {
                StartCoroutine(_GiveRewards());
            }
        }
        else if (PlayerPrefs.GetString("GivingRewardsDate") == _currentDate)
        {
            return;
        }
        else
        {
            StartCoroutine(_GiveRewards());
        }
    }

    IEnumerator _GiveRewards()
    {
        yield return new WaitForSeconds(1);
        
        PlayerPrefs.SetString("GivingRewardsDate", _currentDate);
        int _coin = UnityEngine.Random.Range(10, 100);
        DressList.Instance.Add_Coin(_coin);

        rewardsPanel.SetActive(true);
        instructionText.text = "Congratulations \n \n you won <color=#ff0000>" + _coin + " </color>XELDA";

        Debug.Log("Giving rewards date " + PlayerPrefs.GetString("GivingRewardsDate") + " " + _coin);
    }
}
