using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
	[SerializeField]
	ParticleSystem slash;

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
		Attack4,
		Attack5,
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
			switch (_attack)
			{
				case PlayerAttack.None:
				break;
				case PlayerAttack.Attack1:
					anim.CrossFade("attackA2", 0.1f);
				break;
				case PlayerAttack.Attack2:
					anim.CrossFade("attackA3", 0.1f);
					break;
				case PlayerAttack.Attack3:
					anim.CrossFade("attackA4", 0.1f);
					break;
				case PlayerAttack.Attack4:
					anim.CrossFade("attackA5", 0.1f);
					break;
				case PlayerAttack.Attack5:
					anim.CrossFade("attackA5ToStand", 0.1f);
					break;

			}
		}
	}

	float _speed = 8.0f;
	Animator anim;
	bool isAttacking = false;
	float _time = 0.0f;

	Dictionary<PlayerAttack, string> animDict = new Dictionary<PlayerAttack, string>();

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;
		value = GetComponent<JoystickValue>();
		anim = GetComponent<Animator>();

		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

		animDict.Add(PlayerAttack.None, "attackA1");
		animDict.Add(PlayerAttack.Attack1, "attackA2");
		animDict.Add(PlayerAttack.Attack2, "attackA3");
		animDict.Add(PlayerAttack.Attack3, "attackA4");
		animDict.Add(PlayerAttack.Attack4, "attackA5");
		animDict.Add(PlayerAttack.Attack5, "attackA5ToStand");

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

	protected override void UpdateAttack()
	{
		if (isAttacking) return;

		if (Util.IsAnimationDone(anim, animDict[AttackType])) State = Define.State.Idle;
	}

	public void StartAttack()
	{
		isAttacking = true;

		if (State != Define.State.Attack)
		{
			State = Define.State.Attack;
			AttackType = PlayerAttack.None;
			_time = Time.fixedTime;
		}
		else if (AttackType <= PlayerAttack.Attack3 && Time.fixedTime - _time > 0.35f ||
			AttackType == PlayerAttack.Attack4 && Time.fixedTime - _time > 0.9f)
		{
			StopCoroutine("CheckCombo");
			AttackType = AttackType + 1;
			_time = Time.fixedTime;
		}
		StartCoroutine("CheckCombo");
	}

	IEnumerator CheckCombo()
	{
		yield return new WaitForSeconds(1.4f);

		isAttacking = false;
	}

	public void HitEvent()
	{
		Debug.Log("아얏");
	}

	public void SlashEvent()
	{
		slash.Play();
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
				OnMouseEvent_IdleRun(evt);
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
