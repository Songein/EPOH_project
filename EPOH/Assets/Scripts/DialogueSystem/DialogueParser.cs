using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct DialogueTextType
{
    public string DialogueText;
    public string NextDialogueId;
}
public class DialogueStructure
{
    public string DialogueId;
    public string CharacterId;
    public string InteractionType;
    public DialogueTextType[] Texts;
}

public class DialogueParser
{
    public Dictionary<string, DialogueStructure> Parse(string _CSVFileName)
    {
        //딕셔너리 생성하기
        Dictionary<string, DialogueStructure> dictionary = new Dictionary<string, DialogueStructure>();
        
        //CSV 데이터 가져오기
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        if (csvData == null)
        {
            Debug.Log(_CSVFileName + " 이름의 csv file을 찾을 수 없음.");
            return dictionary;
        }
        
        //엔터를 기준으로 줄 나누기
        string[] lines = csvData.text.Split('\n');
        int totalLines = lines.Length - 6;

        string currentKey = "";
        string currentCharacterId = "";
        string currentInteractionType = "";
        List<DialogueTextType> currentDialogues = new List<DialogueTextType>();

        for (int i = 6; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');

            if (values.Length < 2) continue;

            string key = values[0].Trim();
            string characterID = values[1].Trim();
            string interactionType = values.Length > 2 ? values[2].Trim() : "";
            string text = values.Length > 3 ? values[3].Trim() : "";
            string nextID = values.Length > 4 ? values[4].Trim() : "";

            if (!string.IsNullOrEmpty(key)) // 새로운 대사 시작
            {
                if (!string.IsNullOrEmpty(currentKey))
                {
                    dictionary[currentKey] = new DialogueStructure
                    {
                        DialogueId = currentKey,
                        CharacterId = currentCharacterId,
                        InteractionType = currentInteractionType,
                        Texts = currentDialogues.ToArray()
                    };
                }

                currentKey = key;
                currentCharacterId = characterID;
                currentInteractionType = interactionType;
                currentDialogues = new List<DialogueTextType>();
            }

            if (!string.IsNullOrEmpty(text))
            {
                currentDialogues.Add(new DialogueTextType { DialogueText = text, NextDialogueId = nextID });
            }
        }

        if (!string.IsNullOrEmpty(currentKey))
        {
            dictionary[currentKey] = new DialogueStructure
            {
                DialogueId = currentKey,
                CharacterId = currentCharacterId,
                InteractionType = currentInteractionType,
                Texts = currentDialogues.ToArray()
            };
        }

        Debug.LogWarning($"{_CSVFileName} 데이터 파싱 완료! 대사 개수: " + dictionary.Count);
        return dictionary;
    }
}
