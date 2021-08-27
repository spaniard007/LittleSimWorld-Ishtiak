using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    private ConversationTrigger trigger;
    public Image decisionPanel;

    private void Start()
    {
        trigger = gameObject.GetComponent<ConversationTrigger>();
        decisionPanel.gameObject.SetActive(false);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            MapManager.instance.GetDecisionPanel(decisionPanel); //Assign respective decision panel for the after dialogue Decision
            if(trigger!=null)
            trigger.StartDialogue();
            else
            {
                ConversationManager.conversationInstance.ShowDecisionPanel();
            }
        }
            
    }
}
