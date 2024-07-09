using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CsvManager : MonoBehaviour
{
    public static CsvManager Instance;
    
    [SerializeField] string csvFileName;
    public static bool isCSVReadFinish = false;
    
    private Dictionary<int, Dialogue> dialogueDic = new Dictionary<int, Dialogue>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DialogueParser parser = GetComponent<DialogueParser>();
            Dialogue[] dialogues = parser.Parse(csvFileName);
            for (int i = 0; i < dialogues.Length; i++)
            {
                dialogueDic.Add(i+1, dialogues[i]);
            }
            isCSVReadFinish = true;
            if (isCSVReadFinish)
            {
                Debug.Log("딕셔너리에 저장된 대화 수 : " + dialogueDic.Count);
            }
        }
    }
    
    public Dialogue[] GetDialogue(int _StartNum, int _EndNum)
    {
        List<Dialogue> dialogueList = new List<Dialogue>();

        for (int i = 0; i <= _EndNum - _StartNum; i++)
        {
            dialogueList.Add(dialogueDic[_StartNum+i]);
        }

        return dialogueList.ToArray();
    }
}
