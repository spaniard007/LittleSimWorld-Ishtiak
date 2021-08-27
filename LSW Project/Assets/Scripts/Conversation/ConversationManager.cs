using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    #region DilogueUIRef

    public Image actorImg;
    public TextMeshProUGUI actorNameTxt;
    public TextMeshProUGUI messageTxt;
    public CanvasGroup dialoguePanel;

    #endregion

    private Message[] currrentMessages;
    private Actor[] currentActors;
    private int activeMessageNo = 0;
    public bool isDialogueActive;
    public Image decisionPanelParent;

    #region Singletone
    public static ConversationManager conversationInstance;

    private void Awake() {
        if (conversationInstance != null) {
            Destroy(gameObject);
        }else{
            conversationInstance = this;
        }
    }

    #endregion

    void Start()
    {
        HideDialoguePanel();
        decisionPanelParent.gameObject.SetActive(false);
    }

    public void OpenDialogue(Message[] messages, Actor[] actors)
    {
        ShowDialoguePanel();
        currrentMessages = messages;
        currentActors = actors;
        activeMessageNo = 0;
        // Conversation started
        isDialogueActive = true;
        DisplayMessage();
    }

    void ShowDialoguePanel()
    {
        dialoguePanel.alpha = 1;
        dialoguePanel.interactable = true;
        dialoguePanel.blocksRaycasts = true;
    }
    
    void HideDialoguePanel()
    {
        dialoguePanel.alpha = 0;
        dialoguePanel.interactable = false;
        dialoguePanel.blocksRaycasts = false;
    }

    void DisplayMessage()
    {
        Message messageToDisplay = currrentMessages[activeMessageNo];
        messageTxt.text = messageToDisplay.message;

        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorNameTxt.text = actorToDisplay.name;
        actorImg.sprite = actorToDisplay.sprite;
    }

    public void NextMessage()
    {
        activeMessageNo++;
        if (activeMessageNo < currrentMessages.Length)
        {
            DisplayMessage();
        }
        else
        {
            //Conversation finished
            HideDialoguePanel();
            ShowDecisionPanel();
            isDialogueActive = false;
        }
    }

    public void ShowDecisionPanel()
    {
        if (MapManager.instance.decisionPanel != null)
        {
            decisionPanelParent.gameObject.SetActive(true);
            MapManager.instance.decisionPanel.gameObject.SetActive(true);
        }
    }
}
