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

    #region Singletone
    
    public static ConversationManager instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        }else{
            instance = this;
        }
    }

    #endregion

    void Start()
    {
        dialoguePanel = GetComponent<CanvasGroup>();
        HideDialoguePanel();
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
            isDialogueActive = false;
        }
    }
}
