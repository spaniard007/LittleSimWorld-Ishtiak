using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    private ConversationTrigger trigger;

    private void Start()
    {
        trigger = gameObject.GetComponent<ConversationTrigger>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            trigger.StartDialogue();
        }
            
    }
}
