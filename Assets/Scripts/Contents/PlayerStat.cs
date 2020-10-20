using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerStat : Stat
{
	[SerializeField]
	protected int _exp;
	[SerializeField]
	protected int _gold;

	public int Exp
	{
		get { return _exp; }
		set
		{
			_exp = value;

			int level = Level;
			while (true)
			{
				Data.Stat stat;
				if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
					break;
				if (_exp < stat.totalExp)
					break;
				++level;
			}

			if (level != Level)
			{
				Debug.Log("Level Up!");
				Level = level;
				SetStat(Level);
			}
		}
	}
	public int Gold { get { return _gold; } set { _gold = value; } }

	private void Start()
	{
		LoadStat();

		if (_level >= 1) return;
		_level = 1;

		SetStat(1);
		_exp = 0;
		_defence = 0;
		_moveSpeed = 8.0f;
		_gold = 0;
	}

	public void Cure(int amount)
	{
		Hp += amount;
		if (Hp > MaxHp) Hp = MaxHp;
	}

	public override void OnAttacked(Stat attacker)
	{
		if (GetComponent<BaseController>().State == Define.State.Defence) return;
		int damage = Mathf.Max(0, attacker.Attack - Defence);
		Hp -= damage;

		if (Hp <= 0)
		{
			Hp = 0;
			OnDead(attacker);
		}
	}

	public void SetStat(int level)
	{
		if (level < 1) level = 1;
		Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
		Data.Stat stat = dict[level];

		_hp = stat.maxHp;
		_maxHp = stat.maxHp;
		_attack = stat.attack;
	}

	public void SetStat(int level, int exp, int gold, int hp)
	{
		SetStat(level);
		Exp = exp;
		Gold = gold;
		Hp = hp;
	}

	public bool BuyItem(int price)
	{
		if (Gold < price) return false;

		Gold -= price;
		return true;
	}

	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");

		GetComponent<PlayerController>().State = Define.State.Die;
	}

	public void SaveStat()
	{
		Data.PlayerStat data = new Data.PlayerStat();
		data.level = Level;
		data.hp = (Hp <= 0) ? MaxHp : Hp;
		data.gold = Gold;
		data.exp = Exp;
		
		Debug.Log(JsonUtility.ToJson(data));
		File.WriteAllText(Application.dataPath + "/Resources/Data/PlayerStat.json", JsonUtility.ToJson(data, true));
	}

	public void LoadStat()
	{
		TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/PlayerStat");
		if (textAsset == null) return;
		Data.PlayerStat stat = JsonUtility.FromJson<Data.PlayerStat>(textAsset.text);

		if (stat != null)
		{
			Level = stat.level;
			SetStat(Level);			// 초기값으로 한번 초기화 처리
			Hp = stat.hp;
			Gold = stat.gold;
			Exp = stat.exp;
		}
	}
}
