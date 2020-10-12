using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : BaseController
{
	Stat _stat;

	float _scanRange = 8f;
	float _attackRange = 1.2f;
	float _moveSpeed = 3.0f;

	float _moveDistance = 20.0f;

	Vector3 _originPos;
	Quaternion _originRot;

	NavMeshAgent _nav;
	Animator _anim;

	public override Define.State State
	{
		get { return _state; }
		set
		{
			_state = value;

			_anim.SetInteger("state", (int)_state);
			switch (_state)
			{
				case Define.State.Idle:
					break;
				case Define.State.Moving:
					break;
				case Define.State.Run:
					_moveSpeed = 3.0f;
					break;
				case Define.State.Attack:
					break;
				case Define.State.Skill:
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

		_originPos = transform.position;
		_originRot = transform.rotation;

		_nav = gameObject.GetOrAddComponent<NavMeshAgent>();
		_anim = gameObject.GetOrAddComponent<Animator>();

		//if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
		//Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
	}

	protected override void UpdateIdle()
	{
		GameObject player = Managers.Game.GetPlayer();

		if (player == null || player.GetComponent<Stat>().Hp <= 0)
			return;

		float distance = (player.transform.position - transform.position).magnitude;
		//float distanceSqr = (player.transform.position - transform.position).sqrMagnitude;
		if (distance <= _scanRange)
		{
			// 스캔 사정거리에 들어왔다면
			Animator anim = GetComponent<Animator>();
			anim.SetTrigger("IdleToTarget");
			_lockTarget = player;
			State = Define.State.Run;
			return;
		}
	}

	protected override void UpdateRun()
	{
		if (Vector3.Distance(transform.position, _originPos) > _moveDistance)
		{
			State = Define.State.Moving;
			return;
		}

		if (_lockTarget != null)
		{
			_destPos = _lockTarget.transform.position;
			float distance = (_destPos - transform.position).magnitude;
			if (distance <= _attackRange)
			{
				_nav.SetDestination(transform.position);
				if (Random.Range(0, 2) == 0) State = Define.State.Attack;
				else State = Define.State.Skill;
				return;
			}
		}

		Vector3 dir = _destPos - transform.position;

		// 도착했다면
		if (dir.magnitude < 0.1f)
		{
			State = Define.State.Idle;
		}
		else
		{
			_nav.SetDestination(_destPos);
			_nav.speed = _moveSpeed;

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
		}
	}

	protected override void UpdateMoving()
	{
		if (Vector3.Distance(transform.position, _originPos) > 0.1f)
		{
			_nav.destination = _originPos;
			_nav.stoppingDistance = 0f;
		}
		else
		{
			_nav.isStopped = true;
			_nav.ResetPath();

			transform.position = _originPos;
			transform.rotation = _originRot;

			//hp 회복 필요
			//hp = maxHP;

			State = Define.State.Idle;

			Animator anim = GetComponent<Animator>();
			anim.SetTrigger("TargetToIdle");
		}
	}

	protected override void UpdateSkill()
	{
		if (_lockTarget != null)
		{
			Vector3 dir = _lockTarget.transform.position - transform.position;
			Quaternion quat = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
		}
	}
	protected override void UpdateDie()
	{
		if (Util.IsAnimationDone(_anim, "Die"))
		{
			Managers.Quest.CheckQuest(GetComponent<ObjData>().id);
			gameObject.SetActive(false);
		}
	}


	public override void OnAttacked(Stat attacker)
	{
		if (State == Define.State.Moving || State == Define.State.Die) return;
		_stat.OnAttacked(attacker);
	}

	void OnHitEvent()
	{
		if (_lockTarget != null)
		{
			Stat targetStat = _lockTarget.GetComponent<Stat>();
			BaseController ctr = _lockTarget.GetComponent<BaseController>();

			if (targetStat.Hp > 0)
			{
				float distance = (_lockTarget.transform.position - transform.position).magnitude;
				if (distance <= _attackRange)
				{
					if (Random.Range(0, 2) == 0) State = Define.State.Attack;
					else State = Define.State.Skill;

					ctr.OnAttacked(_stat);
				}
				else
					State = Define.State.Run;
			}
			else
			{
				_lockTarget = null;
				State = Define.State.Moving;
			}
		}
		else
		{
			State = Define.State.Idle;
		}
	}

}
