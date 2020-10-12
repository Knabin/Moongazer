using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager
{
	public int questId;
	public int questActionIndex;
	public int questCount;

	Dictionary<int, QuestData> questList;

	public void Init()
	{
		questList = new Dictionary<int, QuestData>();
		GenerateData();
	}

	void GenerateData()
	{
		questList.Add(10, new QuestData("슬라임 퇴치", new int[] { 1000, 500, 500, 500, 500, 500 }));
		questList.Add(20, new QuestData("스켈레톤 퇴치", new int[] { 0 }));
		questCount = 0;
	}

	public int GetQuestTalkIndex(int id)
	{
		return questId + questActionIndex;
	}

	public void CheckQuest(int id)
	{
		if (questList.Count == 0) return;
		if (id == questList[questId].npcId[questActionIndex])
			questActionIndex++;

		if (questActionIndex >= questList[questId].npcId.Length)
			NextQuest();
	}

	public bool IsNotEndQuest(int id)
	{
		for(int i = 0; i < questActionIndex; ++i)
		{
			if (id == questList[questId].npcId[i] && questActionIndex < questList[questId].npcId.Length)
				return true;
		}
		return false;
	}

	void NextQuest()
	{
		Debug.Log("NextQuest");
		Managers.Game.GetPlayer().GetComponent<PlayerStat>().Exp += 20;
		Managers.Sound.Play("Sfx/QuestEnd");
		questId += 10;
		questActionIndex = 0;
		questCount = 0;
	}
}
