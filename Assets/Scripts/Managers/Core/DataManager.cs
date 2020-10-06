using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoader<Key, Value>
{
	Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
	public Dictionary<int, Data.Stat> StatDict { get; private set; } = new Dictionary<int, Data.Stat>();
	public Dictionary<int, Data.Item> ItemDict { get; private set; } = new Dictionary<int, Data.Item>();

	public void Init()
	{
		StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
		//StatDict = LoadJson<Data.StatData, int, Data.Stat>("StatData").MakeDict();
	}

	Loader LoadJson<Loader, Key, Value>(string path) where Loader : ILoader<Key, Value>
	{
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
		return JsonUtility.FromJson<Loader>(textAsset.text);
	}
}
