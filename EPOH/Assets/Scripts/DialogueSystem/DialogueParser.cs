using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogueParser : MonoBehaviour
{
    public Dialogue[] Parse(string _CSVFileName)
    {
        //대사 리스트 생성
        List<Dialogue> dialogueList = new List<Dialogue>();
        //csv 파일 가져오기
        TextAsset csvData = Resources.Load<TextAsset>(_CSVFileName);
        //엔터 기준으로 줄마다 쪼개서 저장
        String[] data = csvData.text.Split(new char[] { '\n' });
        
        for (int i = 1; i < data.Length;)
        {
            //헤더(StoryId, Name, Content, GameState, EndingState) 별로 쪼개기
            string[] header = data[i].Split(new char[] { ',' });
            Dialogue dialogue = new Dialogue();

            if (header[0] != "")
            {
                Debug.Log(header[0]);
                if (header[1] == "")
                {
                    Debug.Log("효과 : " + header[2]);
                    ++i;
                    continue;
                }
            }
            //이름 값 저장
            Debug.Log(header[1]);
            dialogue.name = header[1];
            
            //대사내용 저정하기 위한 List 생성(대사 내용 수가 유동적이기에 List로 관리)
            List<string> contentList = new List<string>();

            do
            {
                contentList.Add(header[2]);
                Debug.Log(header[2]);
                if (++i < data.Length)
                {
                    //저장하고 data 내에 남은 줄이 있다면 다음 줄 읽어들이기
                    header = data[i].Split(new char[] { ',' });
                }
                else
                {
                    break;
                }

            } while ((header[0] == "") && (header[1] == ""));

            Debug.Log("대화리스트에 대화 하나 추가");
            dialogue.contents = contentList.ToArray();
            dialogueList.Add(dialogue);
        }

        return dialogueList.ToArray();
    }
}
