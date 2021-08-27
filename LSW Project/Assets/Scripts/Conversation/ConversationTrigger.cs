using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        ConversationManager.instance.OpenDialogue(messages,actors);
    }
}
#region Message
[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}
#endregion

#region  Actor
[System.Serializable]
public class Actor {
    public string name;
    public Sprite sprite;
}
#endregion

