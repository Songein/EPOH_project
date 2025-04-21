using System.Collections;
using System.Collections.Generic;
using Event;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("다이얼로그 정보")]
    private DialogueStructure _currentDialogue;
    public string currentLine;
    public string nextDialogueID;
    private Queue<DialogueTextType> _dialogues = new Queue<DialogueTextType>();

    [Header("UI 관련 변수")] [SerializeField] private GameObject _dialogueUI;
    [SerializeField] TextMeshProUGUI _characterName;
    [SerializeField] TextMeshProUGUI _dialogueArea;
    
    [Header("대사 출력 변수")]
    [SerializeField] bool isDialogueActive = false;
    [SerializeField] bool isTyping = false;
    [SerializeField] bool isLineEnd = false;
    [SerializeField] float typingSpeed = 0.1f;
    [SerializeField] float lineChangeSpeed = 0.5f;

    private static DialogueManager _instance;

    public static DialogueManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(DialogueManager).Name);
                    _instance = singletonObject.AddComponent<DialogueManager>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 유지됨
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // 중복 생성 방지
        }
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isTyping)
        {
            StopAllCoroutines();
            isTyping = false;
            _dialogueArea.text = currentLine;
            Invoke("ChangeLineTime", lineChangeSpeed);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isLineEnd)
        {
            isLineEnd = false;
            DisplayNextDialogueLine();
        }
    }

    public IEnumerator StartDialogue(string dialogueID)
    {
        yield return new WaitForSeconds(1f);
        // 다이얼로그 가져오기
        _currentDialogue = DataManager.Instance.Dialogues[dialogueID]; 
        isDialogueActive = true;
        
        //대화 큐에 대화 넣기
        _dialogues.Clear();
        foreach (DialogueTextType dialogue in _currentDialogue.Texts)
        {
            _dialogues.Enqueue(dialogue);
        }
        
        //대사 UI창 열기
        SetUI(_currentDialogue);
        _dialogueUI.SetActive(true);
        
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        //대화 수가 0이면 대화 종료
        if (_dialogues.Count == 0)
        {
            if (nextDialogueID != "")
            {
                StartDialogue(nextDialogueID);
                return;
            }
            EndDialogue();
            return;
        }

        //대사 큐 수가 0이면
        if (_dialogues.Count > 0)
        {
            //대화 큐에서 대화 하나 뽑아오기
            DialogueTextType dialogue = _dialogues.Dequeue();
            currentLine = dialogue.DialogueText;
            nextDialogueID = dialogue.NextDialogueId;
        }
        
        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }
    
    IEnumerator TypeSentence(string dialogueLine)
    {
        _dialogueArea.text = "";

        foreach (char letter in dialogueLine.ToCharArray())
        {
            isTyping = true;
            
            //csv 파일 내 @을 , 로 변경하여 출력
            if (letter.Equals('@'))
            {
                _dialogueArea.text += ',';
            }
            else
            {
                _dialogueArea.text += letter;
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
        _dialogueUI.SetActive(false);
    }

    void SetUI(DialogueStructure dialogueInfo)
    {
        if (!string.IsNullOrEmpty(dialogueInfo.CharacterId) &&
            DataManager.Instance.Characters.ContainsKey(dialogueInfo.CharacterId))
        {
            CharacterStructure characterInfo = DataManager.Instance.Characters[dialogueInfo.CharacterId];
            _characterName.text = characterInfo.CharacterName;
        }
    }
}
