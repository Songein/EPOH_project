﻿using System.Collections;
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
        talk_data.Add(0, new string[] {"......",
            "의식 접속 확인.",
            "반가워, 새로운 해커. 오늘이 근무 첫날이라고 했던가?",
            "그래, 당신 말이야.",
            "EPO에 온 걸 환영해.",
            "나는 당신의 담당 오퍼레이터야.",
            "이미 알고 있겠지만,",
            "우리 회사에서는 사람들이 지우고 싶은 기억을 지워주는 일을 해.",
            "사람에게서 기억을 '검색'하고, 그걸 시스템에 '전사'한 다음,",
            "해커가 그 기억을 '삭제'하지.",
            "그 사이에서 중계역을 하며 당신에게 임무를 할당하는 게 나야.",
            "그러면 바로 임무를 시작해 볼까?",
            "오른쪽에 있는 기계에 당신을 위한 임무를 할당해 뒀어.",
            "훌륭히 임무를 수행하길 바랄게."  });
        talk_data.Add(1, new string[] {"수고했어, 해커.",
            "시스템을 통해 네가 기억을 '추출'하는 모습을 보고 있었는데, 역시 잘 하더라.",
            "... 응?",
            "아, 물론 기억을 '삭제'하는 거지.",
            "그보다, 어땠어? 기억을 지운다는 것 말이야.",
            "아마 그 사람은 지금쯤 어릴적 개에 물렸던 사실은 전부 까먹고 강아지와 행복한 시간을 보내고 있겠지.",
            "자세한 후일담이 궁금하면 코어를 살펴보도록 해.",
            "기억이 정말 제대로 지워졌는지, 당분간 회사에서 모니터링을 하거든.",
            "직원의 동기부여를 위해서라나?",
            "하지만...",
            "......",
            "당신은 어떻게 생각할까?",
            "기억을 지우면 반드시 행복해지는지...",
            "당신의 답을 찾길 바라.",
            "자, 그럼 다음 임무를 할당할게.",
            "위젯에서 마음에 드는 임무를 고르도록 해."  });
        talk_data.Add(2, new string[] {"...아, 아. ",
            "들리시나요~?",
            "아! 안녕하세요. 저는 당신의 담당 프로젝터예요!",
            "본래 해커와의 통신은 오퍼레이터의 담당이지만...",
            "오늘은 제가 특별히! 졸라서! 통신을 연결했답니다!",
            "해커님이 기억의 '키'를 2개나 찾으셨다고 들었거든요.",
            "기억의 '키'가 뭐냐고요?",
            "음~ 그건...",
            "제가 설명해 드릴 수도 있지만, 그러면 이그제미너가 할 말이 없어져 버리니까 다음에 직접 들으세요!",
            "대신 저는 제 역할에 대해 말씀드릴게요.",
            "저는 기억을 시스템에 '전사'하는 일을 해요.",
            "기억과 기억을 연결하는 수많은 길의 끝을 시스템과 연결하는 일이죠.",
            "그래서 당신이 시스템을 통해 기억에 드나들 수 있는 거예요.",
            "그리고 그 안의 기억을 비워내면, 그걸 다시 뇌의 다른 기억들과 연결하죠.",
            "이것만 두고 보면 굳이 중간에 해킹해야 하나 싶지만, 여기서 중요한 포인트!",
            "단순히 기억을 지우기만 하면, '공백'이 생겨버리는 문제가 발생해요.",
            "가령 트라우마처럼 특정 상황에서 반복적으로 해당 기억이 호출되었다면...",
            "같은 상황이 발생했을 때, 텅 빈 기억을 호출하게 되죠.",
            "그럼 의뢰인들은 자신이 '잊었다'는 사실을 인식하고 말아요.",
            "하지만 우리 회사의 기술을 사용한다면 대부분은 잊었다는 사실조차 인지하지 못한답니다!",
            "기억을 계속 자극해서, 어떤 순간에 기억이 호출되는지 찾고, 그 연결고리까지 끊어내니까요.",
            "...맞아요! 해커님의 역할이 바로 그거죠.",
            "그러면 만약에, 그 연결고리만 끊어낸다면 어떻게 될 것 같으세요?",
            "아, 정답은 다음에 기회가 되면 말씀드릴게요.",
            "오퍼레이터가 얼른 끝내라고 성화네요.",
            "그럼 임무 힘내세요, 해커님!"   });
        talk_data.Add(3, new string[] {"......",
            "......안녕.",
            "맞아. 나는 이그제미너.",
            "기억을 '검색'하는 일을 하고 있어.",
            "기억을 '검색'한다는 건, 뇌의 어느 부위에 해당 기억이 저장되어 있는지 찾는 건데,",
            "뇌는 기억을 단기 기억과 장기 기억으로 나누어서 저장하거든. 우리는 그중에...",
            "......",
            "......그래서 시냅스가...",
            "......",
            "... 미안. 중요한 건 이게 아니었지.",
            "프로젝터가 말하는데, 기억의 '키'에 대해 궁금해했다며?",
            "기억의 '키'란 말 그대로 열쇠야.",
            "기억은 감정에 따라 특정한 패턴을 가지고 저장되어 있거든.",
            "그래서 어떤 기억을 찾을 때, 그 기억과 밀접하게 연결된 감정을 토대로 검색해.",
            "처음부터 끝까지 찾아보기엔 너무 방대한 양이니까.",
            "그리고, 키를 알아야 네가 기억을 자극할 때 다른 영역으로 연결되는 길을 제대로 찾을 수 있어.",
            "...프로젝터가 연결고리만 끊어낸다면 어떻게 될 것 같냐고 물었다고?",
            "...아마 어디에도 연결되지 못하고 빈 공간을 떠돌지 않을까?",
            "하지만 우리 회사의 기술이 있다면 떠돌고 있는 기억도 '검색'해서 다시 이어 붙일 수 있겠지.",
            "떠돌던 기억이 우연히 다시 이어져 버릴 수 있으니까, 회사에선 절대 안 된다고 하지만.",
            "그래도... 되돌릴 가능성이 있는 쪽이 더 좋다고 생각하지 않아?",
            "......",
            "이만 끊을게. 임무 힘내."   });
        talk_data.Add(4, new string[] {"오랜만이네.",
            "프로젝터와 이그제미너랑은 잘 만나봤어?",
            "둘 다, 당신을 많이 만나고 싶어 했어.",
            "왜냐고?",
            "그야, 당신이 우리 팀의 해커니까...",
            "......",
            "...라고, 하고 싶지만.",
            "그 전에 묻고 싶은 게 있는데.",
            "당신... 기억은 얼마나 남아있어?",
            "이 회사에 대한 기억,",
            "우리에 대한 기억...",
            "......",
            "반응을 보니, 전혀 남아있지 않은 모양이네.",
            "있잖아, 당신은...",
            "...미안. 지금은 말해줄 수 없을 것 같아.",
            "우리도 신중해야 하거든.",
            "하지만... 곧 알게 될 거야."  });
        talk_data.Add(5, new string[] {"...이제 다섯 가지 패턴을 모두 수집했구나.",
            "해커... 당신도 '공감'에 대해서는 알고 있겠지?",
            "다른 사람들을 볼 때, 그 사람의 감정과 비슷한 감정을 느끼는 것 말이야.",
            "맞아. 우리가 수집하고 있었던 건 당신의 감정 패턴이었어.",
            "타인의 기억을 자극하며, 당신의 기억도 자극된 거지.",
            "그것 뿐만은 아니지만...",
            "아무튼 큰 맥락에서는 비슷해.",
            "...무슨 이야기냐고?",
            "지금부터, 우리는 당신이 잃어버린 기억을 돌려줄까 해.",
            "당신은 모종의 이유로 회사의 눈 밖에 났고,",
            "회사는 당신의 기억을 지웠거든.",
            "하지만 우리들은 마지막에 당신의 기억을 완전히 삭제하는 게 아니라, 연결고리만 끊어놓았어.",
            "그러니 잘만 하면 다시 기억을 되찾을 수 있을 거야.",
            "왜 당신에게 기억을 돌려주려 하냐고?",
            "......",
            "그야, 우리는 당신의 팀이니까.",
            "그리고, 당신은 우리를 지키기 위해 모든 것을 떠안았으니까.",
            "...자, 미안하지만 시간이 별로 없어.",
            "이번에 싸우게 될 것은 우리들의 기억으로 구현된 당신이야.",
            "그 기억을 상대하다보면 당신의 기억도 자극되겠지.",
            "행운을 빌게." });
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