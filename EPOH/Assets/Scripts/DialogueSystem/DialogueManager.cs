using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Dialogue currentDialogue;
    public string currentLine;
    
    //public Image characterPortrait;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<Dialogue> _dialogues = new Queue<Dialogue>();
    private Queue<string> _lines = new Queue<string>();
    
    [SerializeField] bool isDialogueActive = false;
    [SerializeField] bool isTyping = false;
    [SerializeField] bool isLineEnd = false;
    [SerializeField] float typingSpeed = 0.1f;
    [SerializeField] float lineChangeSpeed = 0.5f;

    DialogueUIController dialogueUI;
    private int dialogueCnt;
    
    void Awake()
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
            dialogueArea.text = currentLine;
            Invoke("ChangeLineTime", lineChangeSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isLineEnd)
        {
            isLineEnd = false;
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue[] p_dialogues)
    {
        isDialogueActive = true;
        //대사 UI창 열기
        dialogueUI = GameObject.Find("Canvas").transform.GetChild(0).GetComponent<DialogueUIController>();
        characterName = dialogueUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueArea = dialogueUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        dialogueUI.OpenUI();
        
        //대화 큐에 대화 넣기
        _dialogues.Clear();
        foreach (Dialogue dialogue in p_dialogues)
        {
            _dialogues.Enqueue(dialogue);
        }
        Debug.Log("숫자 : " + _dialogues.Count);
        dialogueCnt = _dialogues.Count;
        
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        //대화 수가 0이면 대화 종료
        if (_dialogues.Count == 0 && _lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        //대사 큐 수가 0이면
        if (_lines.Count == 0)
        {
            //대화 큐에서 대화 하나 뽑아오기
            currentDialogue = _dialogues.Dequeue();
            characterName.text = currentDialogue.name;
        
            //해당 대화 내 대사들을 대사 큐에 저장하기
            _lines.Clear();
            foreach (string line in currentDialogue.contents)
            {
                _lines.Enqueue(line);
            }
        }
        //대사 큐에서 대사 하나 뽑아오기
        currentLine= _lines.Dequeue();
        
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
        
    }
    
    IEnumerator TypeSentence(string dialogueLine)
    {
        dialogueArea.text = "";

        foreach (char letter in dialogueLine.ToCharArray())
        {
            isTyping = true;
            
            //csv 파일 내 @을 , 로 변경하여 출력
            if (letter.Equals('@'))
            {
                dialogueArea.text += ',';
            }
            else
            {
                dialogueArea.text += letter;
            }
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
        dialogueUI.CloseUI();
        GameManager.instance.story_info += dialogueCnt;
        Debug.Log("여기서 추가되는건가?????");
        GameManager.instance.eventFlag = false;
    }
}
