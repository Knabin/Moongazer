using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
	JoystickValue value;
	enum PlayerSkill
	{
		None,
		Skill1,
		Skill2,
		Skill3,
	}

	enum PlayerAttack
	{
		None,
		Attack1,
		Attack2,
		Attack3,
	}

	PlayerSkill _skill;
	PlayerAttack _attack;
	
	PlayerSkill SkillType {
		get { return _skill; }
		set
		{
			_skill = value;
			// TODO: 뭔가 처리
		}
	}

	PlayerAttack AttackType
	{
		get { return _attack; }
		set
		{
			_attack = value;
			// TODO
		}
	}

	float _speed = 8.0f;

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;
		value = GetComponent<JoystickValue>();

		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

	}

	protected override void UpdateIdle()
	{
		
	}

	protected override void UpdateMoving()
	{
		if (value.joyTouch.magnitude < 0.1f)
			State = Define.State.Idle;
		else if (value.joyTouch.magnitude > 0.6f)
			State = Define.State.Run;
		else
		{
			transform.position += new Vector3(value.joyTouch.x, 0, value.joyTouch.y) * Time.deltaTime * _speed;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(value.joyTouch.x, 0, value.joyTouch.y)), 10 * Time.deltaTime);
		}
	}

	protected override void UpdateRun()
	{
		if (value.joyTouch.magnitude < 0.1f)
			State = Define.State.Idle;
		else if (value.joyTouch.magnitude < 0.6f)
			State = Define.State.Moving;
		else
		{
			transform.position += new Vector3(value.joyTouch.x, 0, value.joyTouch.y) * Time.deltaTime * _speed;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(value.joyTouch.x, 0, value.joyTouch.y)), 10 * Time.deltaTime);
		}
	}

	void OnMouseEvent(Define.MouseEvent evt)
	{
		switch (_state)
		{
			case Define.State.Idle:
				OnMouseEvent_IdleRun(evt);
				break;
			case Define.State.Moving:
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

	void OnMouseEvent_IdleRun(Define.MouseEvent evt)
	{
		if (value.joyTouch == Vector2.zero) return;

		switch (evt)
		{
			case Define.MouseEvent.PointerDown:
			case Define.MouseEvent.Press:
				{
					if (value.joyTouch.magnitude > 0.6f)
						State = Define.State.Run;
					else State = Define.State.Moving;

				}
				break;
			case Define.MouseEvent.PointerUp:
				break;
		}
		
	}
}
