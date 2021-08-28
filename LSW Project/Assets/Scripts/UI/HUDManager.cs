using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public Image resumePanel;
    public Image gameOverPanel;
    public Image votingPanel;
    public TextMeshProUGUI xeldaTxt;
    public int gameTimer = 0;
    public TextMeshProUGUI dateInfoTMP;
    public Sprite[] soundIcon;
    public Image soundBTN;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayerCharacter",0);
        PlayerPrefs.GetInt("PlayerHair",0);
        resumePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        CountDown();
        if (Audiomanager.instance.bgSound.enabled)
        {
            soundBTN.sprite = soundIcon[1];
        }
        else
        {
            soundBTN.sprite = soundIcon[0];
        }
        //GoForVote();
    }

    public void GoForVote()
    {
        if(EventManager.instance.IsEventRunning && HomeUI.isReady)
            votingPanel.gameObject.SetActive(true);
        else
        {
            votingPanel.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        xeldaTxt.text = DressList.Instance.Get_Coin().ToString();
        if (EventManager.instance.IsEventRunning)
        {
            dateInfoTMP.text = "You an upcoming \"<b> <color=#F76C26>" + EventManager.instance.RunningEvent +
                               "</color></b>\" event.";
        }
        else
        {
            dateInfoTMP.text = "You don't have any upcoming event!";
        }
    }

    public void PauseGame()
    {
        Audiomanager.instance.PlayBtnSound();
        ShowResumePanel();
    }

    public void ShowResumePanel()
    {
        resumePanel.gameObject.SetActive(true);
        resumePanel.GetComponent<TweenTransforms>().Begin();
    }

    public void ResumeGame(){
        Audiomanager.instance.PlayBtnSound();
        resumePanel.GetComponent<TweenTransforms>().SwitchTargets();
        resumePanel.GetComponent<TweenTransforms>().Begin();
        Invoke("HideResumePanel",0.5f);
    }
    public void HideResumePanel()
    {
        resumePanel.GetComponent<TweenTransforms>().SwitchTargets();
        resumePanel.gameObject.SetActive(false);
    }

    public void SoundToggle()
    {
        if (Audiomanager.instance.bgSound.enabled)
        {
            soundBTN.sprite = soundIcon[0];
            Audiomanager.instance.SoundOff();
        }
        else
        {
            soundBTN.sprite = soundIcon[1];
            Audiomanager.instance.SoundOn();
        }
    }
    public void GoToMenu()
    {
        Audiomanager.instance.PlayBtnSound();
        SceneChanger.instance.GoTOScene(0);
    }
    
    public void CountDown()
    {
        if (DressList.Instance.Get_Coin() > 0)
        {
            DressList.Instance.Minus_Coin(1f);
            Invoke("CountDown",2f);
        }
        else
        {
            //GameOver
            ShowGameOverPanel();
        }
    }
    
    public void ShowGameOverPanel()
    {
        Audiomanager.instance.PlayBtnSound();
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.GetComponent<TweenTransforms>().Begin();
    }

    public void RestartGame()
    {
        Audiomanager.instance.PlayBtnSound();
        DressList.Instance.Add_Coin(100f);
    }
    
    
}
