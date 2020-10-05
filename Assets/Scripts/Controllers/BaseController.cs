using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
	[SerializeField]
	protected Vector3 _destPos;

	[SerializeField]
	protected Define.State _state = Define.State.Idle;

	[SerializeField]
	protected GameObject _lockTarget;

	public Define.WorldObject WorldObjectType { get; protected set; } = Define.WorldObject.Unknown;

	// 프로퍼티도 virtual / override 가능!
	public virtual Define.State State
	{
		get { return _state; }
		set
		{
			_state = value;

			Animator anim = GetComponent<Animator>();
			switch (_state)
			{
				case Define.State.Idle:
					anim.CrossFade("IDLE", 0.1f);
					break;
				case Define.State.Moving:
					anim.CrossFade("WALK", 0.1f);
					break;
				case Define.State.Run:
					anim.CrossFade("RUN", 0.1f);
					break;
				case Define.State.Jump:
					break;
				case Define.State.Attack:
					break;
				case Define.State.Skill:
					break;
				case Define.State.Defence:
					break;
				case Define.State.Die:
					break;
				default:
					break;
			}
		}
	}

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		switch (State)
		{
			case Define.State.Idle:
				UpdateIdle();
				break;
			case Define.State.Moving:
				UpdateMoving();
				break;
			case Define.State.Run:
				UpdateRun();
				break;
			case Define.State.Jump:
				UpdateJump();
				break;
			case Define.State.Attack:
				UpdateAttack();
				break;
			case Define.State.Skill:
				UpdateSkill();
				break;
			case Define.State.Defence:
				UpdateDefence();
				break;
			case Define.State.Die:
				UpdateDie();
				break;
		}
	}

	public abstract void Init();

	protected virtual void UpdateIdle()
	{

	}
	protected virtual void UpdateMoving()
	{

	}
	protected virtual void UpdateRun()
	{

	}
	protected virtual void UpdateJump()
	{

	}
	protected virtual void UpdateAttack()
	{

	}
	protected virtual void UpdateSkill()
	{

	}
	protected virtual void UpdateDefence()
	{

	}
	protected virtual void UpdateDie()
	{

	}
}
