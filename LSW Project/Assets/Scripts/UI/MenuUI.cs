using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuUI : MonoBehaviour
{
    public Image storyPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        storyPanel.gameObject.SetActive(false);
        if (PlayerPrefs.GetInt("isNew") == 0)
        {
            PlayerPrefs.SetInt("isNew",1);
            ShowStoryBTN();
        }
        //PlayerPrefs.SetInt("isNew",0);   //Reset 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShowStoryBTN()
    {
        Audiomanager.instance.PlayBtnSound();
        ShowStoryPanel();
    }

    public void ShowStoryPanel()
    {
        storyPanel.gameObject.SetActive(true);
        storyPanel.GetComponent<TweenTransforms>().Begin();
    }

    public void HideStoryPanelBTN(){
        Audiomanager.instance.PlayBtnSound();
        storyPanel.GetComponent<TweenTransforms>().SwitchTargets();
        storyPanel.GetComponent<TweenTransforms>().Begin();
        Invoke("HideStoryPanel",0.5f);
    }
    public void HideStoryPanel()
    {
        storyPanel.GetComponent<TweenTransforms>().SwitchTargets();
        storyPanel.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Audiomanager.instance.PlayBtnSound();
        Application.Quit();
    }
}
