using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager
{
	public Dictionary<int, string[]> talkData;

	public void Init()
    {
		talkData = new Dictionary<int, string[]>();
		GeneratedData();
    }

    void GeneratedData()
    {
		talkData.Add(1000, new string[] { "슬라임 다섯 마리를 잡아 주세요. 바로 옆에 있는 다리를 이용하시면 돼요.", "정말 강하시군요! 동굴은 이대로 쭉 내려가서 강을 건너면 됩니다.\n용감한 모험가님께 달의 가호가 함께하기를." });
		talkData.Add(10 + 1000, new string[] { "아니, 이런 위험한 곳에는 무슨 일이신가요? \n아... 역시나 달의 소문을 듣고 오신 모험가님이시군요.",
			"여기까지 발걸음해 주셨지만, 저희 달의 마을은 사라졌습니다.\n아니, 몬스터들 때문에 사라지고 있다고 하는 게 맞겠죠.",
			"현재 달의 신을 모시던 동굴을 마물들이 차지했어요. 저희의 힘으로는 지켜낼 수가 없었습니다.",
			"네? 저희 마을에 도움을 주고 싶으시다구요? \n외부인께 이런 일을 맡겨 드려도 되는 건지 모르겠네요...",
			"이 앞은 위험합니다. 동굴로 향하기 전에 모험가님을 시험해 봐도 괜찮을까요?\n 우측의 다리를 건너면 슬라임들이 우글우글해요. 다섯 마리만 처리해 주시겠어요?" });

		talkData.Add(10 + 1001, new string[] { "슬라임 다섯 마리를 잡아 주세요. 바로 옆에 있는 다리를 이용하시면 돼요." });
		talkData.Add(10 + 1007, new string[] { "정말 강하시군요! 동굴은 이대로 쭉 내려가서 강을 건너면 됩니다.\n용감한 모험가님께 달의 가호가 함께하기를." });

		talkData.Add(2000, new string[] { "달의 일족은 항상 오른쪽을 기준으로 삼는다.\n가장 우측의 것을 첫 번째로 삼는다는 뜻이다.\n정확한 이유는 밝혀지지 않았지만, 전문가들이 추측하기로는..." });
		talkData.Add(2001, new string[] { "'감옥 통행 스크롤'을 획득했다.", "먼지 쌓인 너저분한 책상이다." });

	}

	public string GetTalk(int id, int talkIndex)
	{
		return talkData[id][talkIndex];
	}

	public int GetTalkLength(int id)
	{
		return talkData[id].Length;
	}
}
