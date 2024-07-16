using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueEvent dialogue;

    public void TriggerDialogue()
    {
        dialogue.dialogues = CsvManager.Instance.GetDialogue((int)dialogue.line.x, (int)dialogue.line.y);
        DialogueManager.Instance.StartDialogue(dialogue.dialogues);
    }

    public static void TriggerDialogue(int _start, int _end)
    {
        DialogueManager.Instance.StartDialogue(CsvManager.Instance.GetDialogue(_start,_end));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }
}
