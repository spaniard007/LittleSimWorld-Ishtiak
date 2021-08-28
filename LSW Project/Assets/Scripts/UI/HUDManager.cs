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
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("PlayerCharacter",0);
        PlayerPrefs.GetInt("PlayerHair",0);
        resumePanel.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        CountDown();
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
    }

    public void PauseGame()
    {
        ShowResumePanel();
    }

    public void ShowResumePanel()
    {
        resumePanel.gameObject.SetActive(true);
        resumePanel.GetComponent<TweenTransforms>().Begin();
    }

    public void ResumeGame(){
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
        //control music
    }
    public void GoToMenu()
    {
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
        gameOverPanel.gameObject.SetActive(true);
        gameOverPanel.GetComponent<TweenTransforms>().Begin();
    }

    public void RestartGame()
    {
        DressList.Instance.Add_Coin(100f);
    }
}
