using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
	[SerializeField]
	protected int _level;
	[SerializeField]
	protected int _hp;
	[SerializeField]
	protected int _maxHp;
	[SerializeField]
	protected int _attack;
	[SerializeField]
	protected int _defence;
	[SerializeField]
	protected float _moveSpeed;

	public int Level { get { return _level; } set { _level = value; } }
	public int Hp { get { return _hp; } set { _hp = value; } }
	public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
	public int Attack { get { return _attack; } set { _attack = value; } }
	public int Defence { get { return _defence; } set { _defence = value; } }
	public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

	private void Start()
	{
		_level = 1;

		SetStat(gameObject.name);
	}

	public virtual void OnAttacked(Stat attacker)
	{
		int damage = Mathf.Max(0, attacker.Attack - Defence);
		Hp -= damage;
		GetComponent<Animator>().SetTrigger("Attacked");
		//GetComponent<BaseController>().State = Define.State.Defence;

		if (Hp <= 0)
		{
			Hp = 0;
			OnDead(attacker);
		}
	}

	public virtual void OnAttacked(int amount)
	{
		int damage = Mathf.Max(0, amount - Defence);
		Hp -= damage;
		GetComponent<Animator>().SetTrigger("Attacked");

		if (Hp <= 0)
		{
			Hp = 0;
			OnDead();
		}
	}

	public void SetStat(string enemyName)
	{
		Dictionary<int, Data.Stat> dict = Managers.Data.EnemyDict;
		int num = Managers.Data.EnemyNumDict[enemyName];
		Data.Stat stat = dict[num];

		_hp = stat.maxHp;
		_maxHp = stat.maxHp;
		_attack = stat.attack;
	}

	protected virtual void OnDead()
	{
		PlayerStat playerStat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

		if (playerStat != null)
		{
			playerStat.Exp += Level * 5;
			playerStat.Gold += Random.Range(Level * 10, (Level + 5) * 10);
		}
		gameObject.GetComponent<BaseController>().State = Define.State.Die;
	}

	protected virtual void OnDead(Stat attacker)
	{
		PlayerStat playerStat = attacker as PlayerStat;

		if (playerStat != null)
		{
			playerStat.Exp += Level * 5;
			playerStat.Gold += Random.Range(Level * 10, (Level + 5) * 10);
		}
		gameObject.GetComponent<BaseController>().State = Define.State.Die;
	}
}
