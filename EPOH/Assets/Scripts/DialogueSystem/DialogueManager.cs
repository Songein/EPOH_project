using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    private DialogueLine currentLine;

    public Image characterPortrait;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> _lines = new Queue<DialogueLine>();

    public bool isDialogueActive = false;
    public bool isTyping = false;
    public bool isLineEnd = false;
    public float typingSpeed = 0.2f;
    public float lineChangeSpeed = 1f;

    [SerializeField] GameObject dialoguePanel;
    
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTyping)
        {
            StopAllCoroutines();
            isTyping = false;
            dialogueArea.text = currentLine.line;
            Invoke("ChangeLineTime", lineChangeSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isLineEnd)
        {
            isLineEnd = false;
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialoguePanel.SetActive(true);
        Debug.Log(_lines);
        _lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            _lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (_lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentLine= _lines.Dequeue();
        characterPortrait.sprite = currentLine.character.portrait;
        characterName.text = currentLine.character.name;
        
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";

        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            isTyping = true;
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        Invoke("ChangeLineTime",lineChangeSpeed);
    }

    void ChangeLineTime()
    {
        isLineEnd = true;
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
    }
}
