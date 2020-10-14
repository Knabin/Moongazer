using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
	public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
	public Dictionary<int, Data.Stat> EnemyDict { get; private set; } = new Dictionary<int, Data.Stat>();
	public Dictionary<int, Data.Item> ItemDict { get; private set; } = new Dictionary<int, Data.Item>();

	public Dictionary<string, int> EnemyNumDict { get; private set; } = new Dictionary<string, int>();

	public void Init()
	{
		StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
		EnemyDict = LoadJson<Data.StatData, int, Data.Stat>("EnemyStatData").MakeDict();
		ItemDict = LoadJson<Data.ItemData, int, Data.Item>("ItemData").MakeDict();

		EnemyNumDict.Add("Slime", 1);
		EnemyNumDict.Add("Skeleton", 2);
		EnemyNumDict.Add("Dragon", 3);
	}

	public Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}

	// 딕셔너리를 가져와서 읽어야 함

	//public void SaveJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	//{
	//	string filePath = Path.Combine(Application.dataPath, $"Resources/Data/{path}");
	//	File.WriteAllText(filePath, JsonUtility.ToJson(item, true));
	//}
}
