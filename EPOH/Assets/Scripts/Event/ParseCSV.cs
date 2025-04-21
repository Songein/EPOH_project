using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class ParseCSV
{
    public async UniTask<Dictionary<string, T>> Parse<T>(string sheetName) where T : new()
    {
        // 딕셔너리 생성
        Dictionary<string, T> dictionary = new Dictionary<string, T>();
        string csvUrl = $"https://docs.google.com/spreadsheets/d/1F8Kp36JIyAbTImLd5hE6USwS9ohh9LfMvEWrDHSHcXc/gviz/tq?tqx=out:csv&sheet={sheetName}";
        
        // CSV 데이터 가져오기
        string csvData = await LoadCSVFromURL(csvUrl);
        if (string.IsNullOrWhiteSpace(csvData))
        {
            Debug.Log("CSV 데이터를 로드할 수 없습니다.");
            return dictionary;
        }
        
        string[] lines = csvData.Split("\n");
        int headerLineIndex = 5;
        string[] headers = lines[headerLineIndex].Split(',');
    
        // 데이터 줄 순회
        for (int i = headerLineIndex + 1; i < lines.Length; i++)
        {
            //빈 줄이라면 다음 줄로 넘어가기
            if(string.IsNullOrWhiteSpace(lines[i])) continue;
            //쉼표를 기준으로 분리하기
            string[] values = lines[i].Split(',');
            //첫번째 값은 키 값으로 사용
            string key = values[0].Trim().Replace("\"", "");
            //제너릭 객체 생성
            T entry = new T();
            
            List<string> conditionsList = new List<string>(); 
            List<string> resultsList = new List<string>();

            for (int j = 0; j < headers.Length && j < values.Length; j++)
            {
                string header = headers[j].Trim().Replace("\"", "");
                FieldInfo field = typeof(T).GetField(header, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                
                if(string.IsNullOrEmpty(header))
                {
                    // 빈 헤더는 건너뜀
                    continue;
                }
                string value = values[j].Trim().Replace("\"", "");
                Debug.Log($"{header} : {value}");
                if (field != null)
                {
                    //해당 필드 타입으로 값을 변환해서 저장하기
                    field.SetValue(entry,Convert.ChangeType(value,field.FieldType));
                }
                else
                {
                    if (headers[j].StartsWith("Condition")) 
                    {
                        conditionsList.Add(value);
                    }
                    else if (headers[j].StartsWith("Result")) 
                    {
                        resultsList.Add(value);
                    }
                }
            }

            FieldInfo conditionsField = typeof(T).GetField("Conditions", BindingFlags.Public | BindingFlags.Instance);
            FieldInfo resultsField = typeof(T).GetField("Results", BindingFlags.Public | BindingFlags.Instance);
            
            if (conditionsField != null && conditionsField.FieldType == typeof(string[]))
            {
                conditionsField.SetValue(entry, conditionsList.ToArray());
            }
            
            if (resultsField != null && resultsField.FieldType == typeof(string[]))
            {
                resultsField.SetValue(entry, resultsList.ToArray());
            }
            
            dictionary[key] = entry;
            Debug.LogWarning($"{sheetName} 시트에 {key} 값 엔트리 저장");
        }
    
        Debug.LogWarning($"{sheetName} 데이터 파싱 완료! 개수: " + dictionary.Count);
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
    
    private async UniTask<string> LoadCSVFromResources(string fileName)
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"EventCSV/{fileName}");
        
        if (textAsset == null)
        {
            Debug.LogError($"리소스에서 CSV 파일을 찾을 수 없습니다: EventCSV/{fileName}");
            return string.Empty;
        }

        string csvText = textAsset.text;


        await UniTask.Delay(200);
        return csvText;
    }
}
