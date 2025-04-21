using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

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
    public async UniTask<Dictionary<string, DialogueStructure>> Parse(string sheetName)
    {
        //딕셔너리 생성하기
        Dictionary<string, DialogueStructure> dictionary = new Dictionary<string, DialogueStructure>();
        
        string csvUrl = $"https://docs.google.com/spreadsheets/d/1F8Kp36JIyAbTImLd5hE6USwS9ohh9LfMvEWrDHSHcXc/gviz/tq?tqx=out:csv&sheet={sheetName}";
        
        // CSV 데이터 가져오기
        string csvData = await LoadCSVFromURL(csvUrl);
        if (string.IsNullOrWhiteSpace(csvData))
        {
            Debug.Log("CSV 데이터를 로드할 수 없습니다.");
            return dictionary;
        }
        
        //엔터를 기준으로 줄 나누기
        string[] lines = csvData.Split("\n");
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
            string rawText = values.Length > 3 ? values[3].Trim() : "";
            string text = rawText.Replace("\\", "\n");  // \를 \n로 변경
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

        Debug.LogWarning("대화 데이터 파싱 완료! 대사 개수: " + dictionary.Count);
        return dictionary;
    }
    
    private async UniTask<string> LoadCSVFromURL(string url)
    {
        // 캐시에 없으면 다운로드
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            try
            {
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"CSV 다운로드 실패: {www.error}");
                    return string.Empty;
                }

                string csvText = www.downloadHandler.text;
                return csvText;
            }
            catch (Exception e)
            {
                Debug.LogError($"CSV 다운로드 중 오류: {e.Message}");
                return string.Empty;
            }
        }
    }
}
