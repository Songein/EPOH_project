using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [Tooltip("캐릭터 이름")]
    public string name;
    [Tooltip("대사 내용")]
    public string[] contents;
}

[System.Serializable]
public class DialogueEvent
{
    //어떤 대화 이벤트인지
    public string name;
    
    //x~y 까지의 대사 출력
    public Vector2 line;
    public Dialogue[] dialogues;
}
