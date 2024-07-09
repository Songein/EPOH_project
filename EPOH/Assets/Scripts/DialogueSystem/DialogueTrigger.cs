using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public Sprite portrait;
    public string name;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)] public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

[System.Serializable]
public class DialogueList
{
    public List<Dialogue> dialogues = new List<Dialogue>();
}
public class DialogueTrigger : MonoBehaviour
{
    public DialogueList dialogueList;

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueList);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
