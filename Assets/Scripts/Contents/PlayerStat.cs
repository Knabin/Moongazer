using System.Collections;
using System.Collections.Generic;
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
		_level = 1;

		SetStat(1);
		_exp = 0;
		_defence = 0;
		_moveSpeed = 8.0f;
		_gold = 0;
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

	protected override void OnDead(Stat attacker)
	{
		Debug.Log("Player Dead");

		GetComponent<PlayerController>().State = Define.State.Die;
	}
}
