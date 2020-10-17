using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	// int <-> GameObject
	/*
    Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> _monsters = new Dictionary<int, GameObject>();
    Dictionary<int, GameObject> _env = new Dictionary<int, GameObject>();*/
	GameObject _player;

	int level = 1;
	int exp;
	int gold;
	int hp;

	public HashSet<GameObject> _monsters = new HashSet<GameObject>();
	Dictionary<int, Data.Item> _items = new Dictionary<int, Data.Item>();

	public Action<int> OnSpawnEvent;

	public GameObject GetPlayer() { return _player; }

	public void LoadData()
	{
		_player.GetComponent<PlayerStat>().LoadStat();
	}

	public void SaveData()
	{
		_player.GetComponent<PlayerStat>().SaveStat();
		_player.GetComponent<PlayerInven>().SaveInven();
	}

	public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
	{
		GameObject go = Managers.Resource.Instantiate(path, parent);

		switch (type)
		{
			case Define.WorldObject.Enemy:
				_monsters.Add(go);
				if (OnSpawnEvent != null)
					OnSpawnEvent.Invoke(1);
				break;
			case Define.WorldObject.Player:
				_player = go;
				break;
		}

		return go;
	}

	public Define.WorldObject GetWorldObjectType(GameObject go)
	{
		if(go == null) return Define.WorldObject.Unknown;
		BaseController bc = go.GetComponent<BaseController>();

		if (bc == null)
			return Define.WorldObject.Unknown;

		//bc is PlayerController    => 느림!
		return bc.WorldObjectType;
	}

	public void Destroy(GameObject go)
	{
		if (go == null && _monsters.Contains(go)) _monsters.Remove(go);
		Define.WorldObject type = GetWorldObjectType(go);

		switch (type)
		{
			case Define.WorldObject.Enemy:
				{
					if (_monsters.Contains(go))
					{
						_monsters.Remove(go);
						if (OnSpawnEvent != null)
							OnSpawnEvent.Invoke(-1);
					}
				}
				break;
			case Define.WorldObject.Player:
				{
					if (_player == go)
						_player = null;
				}
				break;
		}
		Managers.Resource.Destroy(go);
	}
}
