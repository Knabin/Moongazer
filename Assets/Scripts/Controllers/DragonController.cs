using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragonController : BaseController
{
	Stat _stat;
	Animator _anim;

	int _attackRandomStart = 1;
	int _attackRandomEnd = 8;
	int _attackNum = 0;
	float _attackDelay = 7.0f;

	[SerializeField]
	Transform _fireBallPosition;

	[SerializeField]
	GameObject _fireBall;
	[SerializeField]
	ParticleSystem _fireBreath;
	[SerializeField]
	ParticleSystem _fireSpecial;

	public override Define.State State
	{
		get { return _state; }
		set
		{
			_state = value;

			switch (_state)
			{
				case Define.State.Idle:
					if (_stat.Hp <= 100.0f)
					{
						_attackDelay = 4.0f;
						_anim.SetBool("IsBurst", true);
					}
					StartCoroutine("SetSkillRandom");
					break;
				case Define.State.Attack:
					break;
				case Define.State.Defence:
					break;
				case Define.State.Die:
					_anim.SetTrigger("IsDead");
					break;
				default:
					break;
			}
		}
	}

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Enemy;

		_stat = gameObject.GetComponent<Stat>();
		_anim = gameObject.GetOrAddComponent<Animator>();

		State = Define.State.Defence;   // 자는 상황

		// fireball 풀에 넣기
		Managers.Pool.CreatePool(_fireBall);
		_lockTarget = Managers.Game.GetPlayer();

		Managers.UI.ShowSceneUI<UI_HPBarBoss>();
	}

	public override void OnAttacked(Stat attacker)
	{
		if (State == Define.State.Die || _attackNum == 8) return;
		_stat.OnAttacked(attacker);
	}

	public void SleepToIdleEvent()
	{
		_anim.SetTrigger("SleepToIdle");
	}

	public void AttackToIdle()
	{
		State = Define.State.Idle;
	}

	public void HitEvent()
	{
		float range = 5.0f;
		float angle = 30f;
		float attack = _stat.Attack;
		bool isCheckAngle = true;

		switch (_attackNum)
		{
			case 1:
				attack = _stat.Attack * 0.5f;  // 10
				angle = 30f;
			break;
			case 2:
				attack = _stat.Attack * 0.25f;  // 5
				angle = 60f;
				break;
			case 3:
				attack = _stat.Attack * 0.25f;  // 5
				angle = 60f;
				break;
			case 4:
				range = 4.0f;
				attack = _stat.Attack * 0.5f;
				angle = 20f;
			break;
		}

		if (Vector3.Distance(transform.position, Managers.Game.GetPlayer().transform.position) > range) return;
		Vector3 dirToTarget = (Managers.Game.GetPlayer().transform.position - transform.position).normalized;

		if (!isCheckAngle || Vector3.Dot(transform.forward, dirToTarget) > Mathf.Cos(angle) * Mathf.Deg2Rad)
		{
			BaseController st = Managers.Game.GetPlayer().GetComponent<BaseController>();
			st.OnAttacked(_stat);
		}
	}

	public void SkillFire()
	{
		Managers.Pool.Pop(_fireBall);
	}

	public void SkillBreath()
	{
		if(_fireBreath.isPlaying)
		{
			_fireBreath.Stop();
		}
		else
		{
			_fireBreath.Play();
		}
	}

	public void SkillSpecial()
	{
		if (_fireSpecial.isPlaying)
		{
			_fireSpecial.Stop();
		}
		else
		{
			_fireSpecial.Play();
		}
	}

	public void PlaySound(string fileName)
	{
		Managers.Sound.Play($"Sfx/{fileName}", Define.Sound.Enemy);
	}

	IEnumerator SetSkillRandom()
	{
		yield return new WaitForSeconds(_attackDelay);

		if(_lockTarget != null)
		{
			if(Vector3.Distance(_lockTarget.transform.position, transform.position) < 5.0f)
			{
				_attackRandomStart = 1;
				_attackRandomEnd = 5;
			}
			else
			{
				_attackRandomStart = 5;
				_attackRandomEnd = (_stat.Hp <= 100.0f) ? 8 : 7;
			}
		}

		_attackNum = Random.Range(_attackRandomStart, _attackRandomEnd);
		_anim.SetInteger("Skill", _attackNum);
		_anim.SetTrigger("StartAttack");

	}
}
