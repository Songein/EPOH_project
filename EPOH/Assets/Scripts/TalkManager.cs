using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talk_data; // 스크립트를 저장할 딕셔너리

    void Awake()
    {
        talk_data = new Dictionary<int, string[]>();
        GenerateData();
    }

    void GenerateData() // 데이터를 만들어주는 함수
    {
        //신규 스크립트
        talk_data.Add(0, new string[]
        {
            "여긴...?",
            "아무 것도 기억나지 않아..."
        });
        talk_data.Add(1, new string[]
        {
            "의식 접속 확인.",
            "직원 코드 H-00. 의뢰가 할당되었습니다. 업무에 임해주시기 바랍니다."
        });
        talk_data.Add(2, new string[]
        {
            "의뢰?, 업무? 저쪽에 빛나고 있는 것을 조작하면 되는 건가?"
        });
        talk_data.Add(3, new string[]
        {
            "기억을 지워...? 내가 기억을 지우는 일을 했다는 건가?",
            "...그랬던 것 같기도 한데, 자세한 것들이 하나도 기억나지 않아.",
            "이 '의뢰'를 해결하면 무언가 더 기억이 날까..."
        });
        talk_data.Add(4, new string[]
        {
            "의뢰가 수리되었습니다. 의뢰인의 기억과 연결합니다.",
            "......",
            "연결 완료. 전사 장치를 통해 기억에 입장해주시기 바랍니다."
        });
        talk_data.Add(5, new string[]
        {
            "밖으로 보이는 풍경이 변하고 있어.",
            "의뢰인의 '기억'과 연결된다는게 이런 뜻인가?"
        });
        talk_data.Add(6, new string[]
        {
            "저, 저게 뭐야! 저런게 정말 개라고?"
        });
        talk_data.Add(7, new string[]
        {
            "기억 소거 절차를 진행합니다.",
            "먼저 해당 기억이 포함된 모든 기억을 식별하기 위해 기억을 자극해 주십시오.",
            "기억을 자극하는 것은 부착된 컴포넌트 위젯을 이용해주시기 바랍니다."
        });
        talk_data.Add(8, new string[]
        {
            "끄, 끝난 건가?",
            "뭐야, 왜 더 무서워진 것 같지..?"
        });
        talk_data.Add(9, new string[]
        {
            "기억 분석이 완료되었습니다.",
            "기억 제거 프로세스를 시작합니다.",
            "기억의 방어 작용이 강화되니 조심해주십시오.",
            "긴급 전송 프로토콜 사용 시 작업하던 프로세스가 일부 소실될 수 있으므로 위급 상황에만 사용하길 권장합니다.",
            "최대한 빠르게 해킹 포인트를 전부 모아주세요."
        });
        talk_data.Add(10, new string[]
        {
            "기억 제거 프로세스가 완료되었습니다. 수고하셨습니다.",
            "업무 대기실로 복귀해주시기 바랍니다."
        });
        talk_data.Add(11, new string[]
        {
            "개에게 물린 기억을 지우면 개를 더이상 두려워하지 않을 수 있다니, 좋은걸." ,
            "가기 전에도 저런 게 있었던가...?",
            "윽...!"
        });
        talk_data.Add(12, new string[]
        {
            "불쌍해... 우리가 키울 수는 없을까?"
        });
        talk_data.Add(13, new string[]
        {
            "괜찮겠어, 호아? 너 개를 무서워하잖아."
        });
        talk_data.Add(14, new string[]
        {
            "괘, 괜찮... 흐아악!"
        });
        talk_data.Add(15, new string[]
        {
            "하나도 안 괜찮아 보이는데..."
        });
        talk_data.Add(16, new string[]
        {
            "어릴 적에 큰 개한테 물린 기억이 있거든. 작은 강아지면 괜찮을 줄 알았는데..."
        });
        talk_data.Add(17, new string[]
        {
            "이 녀석이 얼마나 더 클지 모르는 걸. 무리하지 않는 편이 좋아."
        });
        talk_data.Add(18, new string[]
        {
            "......"
        });
        talk_data.Add(19, new string[]
        {
            "주인을 찾는 공고를 올리면 어쩌면 사람이 금방 와줄지도 모르고..."
        });
        talk_data.Add(20, new string[]
        {
            "있잖아.",
            "혹시, 우리가 연구하고 있는 기억 절제술로 개에 물렸던 기억을 지우면 더이상 개를 두려워하지 않을 수 있지 않을까?"
        });
        talk_data.Add(21, new string[]
        {
            "뭐?"
        });
        talk_data.Add(22, new string[]
        {
            "...어차피 임상 실험이 필요한 시기잖아."
        });
        talk_data.Add(23, new string[]
        {
            "그래도, 어떻게 될지 모르는데..."
        });
        talk_data.Add(24, new string[]
        {
            "괜찮아, 나는 동료들을 믿으니까.",
            "내 기억을 지워보는게 어때?"
        });
        talk_data.Add(25, new string[]
        {
            "이건...",
            "내... 기억?"
        });
    }

    public string GetTalk(int id, int talk_index) // 대사를 한 줄씩 리턴해주는 함수
    {
        if(talk_index == talk_data[id].Length) // index가 길이와 같을 경우(더이상 출력할 대사가 없을 경우)
        {
            return null; // null 반환
        }
        return talk_data[id][talk_index]; // 이외의 경우 대사 한 줄을 반환
    }
}
