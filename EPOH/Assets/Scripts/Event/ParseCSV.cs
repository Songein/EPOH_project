using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ParseCSV
{
    public Dictionary<string, T> Parse<T>(string _CSVFileName) where T : new()
    {
        //딕셔너리 생성하기
        Dictionary<string, T> dictionary = new Dictionary<string, T>();
        
        //CSV 데이터 가져오기
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);

        if (csvData == null)
        {
            Debug.Log(_CSVFileName + " 이름의 csv file을 찾을 수 없음.");
            return dictionary;
        }
        
        //엔터를 기준으로 줄 나누기
        string[] lines = csvData.text.Split('\n');
        //헤더 값 저장하기
        string[] headers = lines[5].Split(',');
        //7번째 줄부터 읽어오기(1~5번째 줄은 설명, 6번째 줄은 헤더)
        for (int i = 6; i < lines.Length; i++)
        {
            //빈 줄이라면 다음 줄로 넘어가기
            if(string.IsNullOrWhiteSpace(lines[i])) continue;
            //쉼표를 기준으로 분리하기
            string[] values = lines[i].Split(',');
            //첫번째 값은 키 값으로 사용
            string key = values[0];
            //제너릭 객체 생성
            T entry = new T();
            
            List<string> conditionList = new List<string>(); 
            List<string> resultList = new List<string>();
            
            //나머지 값들을 T class 내의 필드들에 저장하기
            for (int j = 1; j < headers.Length && j < values.Length; j++)
            {
                FieldInfo field = typeof(T).GetField(headers[j], BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                if (field != null)
                {
                    //해당 필드 타입으로 값을 변환해서 저장하기
                    field.SetValue(entry,Convert.ChangeType(values[j],field.FieldType));
                }
                else
                {
                    if (headers[j].StartsWith("Condition")) 
                    {
                        conditionList.Add(values[j]);
                    }
                    else if (headers[j].StartsWith("Result")) 
                    {
                        resultList.Add(values[j]);
                    }
                }
            }
            
            FieldInfo conditionField = typeof(T).GetField("Conditions", BindingFlags.Public | BindingFlags.Instance);
            FieldInfo resultField = typeof(T).GetField("Results", BindingFlags.Public | BindingFlags.Instance);
            
            if (conditionField != null && conditionField.FieldType == typeof(string[]))
            {
                conditionField.SetValue(entry, conditionList.ToArray());
            }
            
            if (resultField != null && resultField.FieldType == typeof(string[]))
            {
                resultField.SetValue(entry, resultList.ToArray());
            }
            
            //딕셔너리에 추가하기
            dictionary[key] = entry;
        }
        Debug.LogWarning($"{_CSVFileName} 데이터 파싱 완료! 대사 개수: " + dictionary.Count);
        return dictionary;
    }
}
