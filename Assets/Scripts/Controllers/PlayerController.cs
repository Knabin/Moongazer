﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
	[SerializeField]
	ParticleSystem slash;
	[SerializeField]
	ParticleSystem slashVertical;
	[SerializeField]
	ParticleSystem slashHorizontal;
	[SerializeField]
	ParticleSystem sk1;
	[SerializeField]
	ParticleSystem sk2;
	[SerializeField]
	ParticleSystem sk3;
	[SerializeField]
	ParticleSystem buff;

	JoystickValue value;
	enum PlayerSkill
	{
		None,
		Skill1,
		Skill2,
		Skill3,
		Skill4,
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
			isJumping = true;
			_skill = value;
			switch (_skill)
			{
				case PlayerSkill.None:
					break;
				case PlayerSkill.Skill1:
					anim.CrossFade("SKILL1", 0.1f);
					break;
				case PlayerSkill.Skill2:
					anim.CrossFade("SKILL2", 0.1f);
					break;
				case PlayerSkill.Skill3:
					anim.CrossFade("SKILL3", 0.1f);
					break;
				case PlayerSkill.Skill4:
					anim.CrossFade("SKILL4", 0.1f);
					break;
			}
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

	float speed = 8.0f;
	Animator anim;
	bool isAttacking = false;
	bool isJumping = false;
	float _time = 0.0f;

	Dictionary<PlayerAttack, string> attackAnim = new Dictionary<PlayerAttack, string>();
	Dictionary<PlayerSkill, string> skillAnim = new Dictionary<PlayerSkill, string>();

	PlayerStat stat;

	public override void Init()
	{
		WorldObjectType = Define.WorldObject.Player;
		value = GetComponent<JoystickValue>();
		anim = GetComponent<Animator>();
		stat = GetComponent<PlayerStat>();

		Managers.Input.MouseAction -= OnMouseEvent;
		Managers.Input.MouseAction += OnMouseEvent;

		attackAnim.Add(PlayerAttack.None, "attackA1");
		attackAnim.Add(PlayerAttack.Attack1, "attackA2");
		attackAnim.Add(PlayerAttack.Attack2, "attackA3");
		attackAnim.Add(PlayerAttack.Attack3, "attackA4");
		attackAnim.Add(PlayerAttack.Attack4, "attackA5");
		attackAnim.Add(PlayerAttack.Attack5, "attackA5ToStand");

		skillAnim.Add(PlayerSkill.Skill1, "SKILL1");
		skillAnim.Add(PlayerSkill.Skill2, "SKILL2");
		skillAnim.Add(PlayerSkill.Skill3, "SKILL3");
		skillAnim.Add(PlayerSkill.Skill4, "SKILL4");
	}

	private void LateUpdate()
	{
		foreach (GameObject go in Managers.Game._monsters)
		{
			if (!go.activeSelf)
			{
				Managers.Game.Destroy(go);
				break;
			}
		}
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
			transform.position += new Vector3(value.joyTouch.x, 0, value.joyTouch.y) * Time.deltaTime * speed;
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
			transform.position += new Vector3(value.joyTouch.x, 0, value.joyTouch.y) * Time.deltaTime * speed;
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(value.joyTouch.x, 0, value.joyTouch.y)), 10 * Time.deltaTime);
		}
	}

	protected override void UpdateAttack()
	{
		if (isAttacking) return;

		if (Util.IsAnimationDone(anim, attackAnim[AttackType])) State = Define.State.Idle;
	}

	protected override void UpdateSkill()
	{
		if (Util.IsAnimationDone(anim, skillAnim[SkillType])) State = Define.State.Idle;
	}

	protected override void UpdateDefence()
	{
		if (Util.IsAnimationDone(anim, "DEFENCE")) State = Define.State.Idle;
		if(value.joyTouch != Vector2.zero)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(value.joyTouch.x, 0, value.joyTouch.y)), 10 * Time.deltaTime);
		}
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
		yield return new WaitForSeconds(1.1f);

		isAttacking = false;
		yield break;
	}

	public void StartSkill(int skillNum)
	{
		isAttacking = true;

		if(State != Define.State.Skill)
		{
			State = Define.State.Skill;
			SkillType = (PlayerSkill)skillNum;
		}
	}

	public void StartDefence()
	{
		if (State != Define.State.Defence)
		{
			State = Define.State.Defence;
		}
	}

	public void HitEvent()
	{
		float range = 2.0f;
		float angle = 30f;
		int attack = stat.Attack;
		bool isCheckAngle = true;

		// TODO: case마다 공격 범위, 공격 각도 조절 필요
		// 평타, 2스킬 - 각도 + 거리
		// 1스킬 - 거리만 체크(모든 방향에서 맞을 수 있음)
		// 3스킬 - 레이캐스트 응용, 코루틴으로 두 번(상태 시작 시, 파티클 종료 시) 처리

		if (State == Define.State.Skill)
		{
			switch (SkillType)
			{
				case PlayerSkill.Skill1:
					range = 3.0f;
					attack = (int)(attack * 1.3f);
					isCheckAngle = false;
					break;
				case PlayerSkill.Skill2:
					range = 2.0f;
					attack = (int)(attack * 1.8f);
					angle = 20f;
					break;
				case PlayerSkill.Skill3:
					break;
				case PlayerSkill.Skill4:
					break;
			}
		}

		foreach (GameObject go in Managers.Game._monsters)
		{
			// TODO: case마다 공격 범위, 공격 각도 조절 필요
			// 평타, 2스킬 - 각도 + 거리
			// 1스킬 - 거리만 체크(모든 방향에서 맞을 수 있음)
			// 3스킬 - 레이캐스트 응용, 코루틴으로 두 번(상태 시작 시, 파티클 종료 시) 처리

			if (Vector3.Distance(go.transform.position, transform.position) > range) continue;

			Vector3 dirToTarget = (go.transform.position - transform.position).normalized;

			if (!isCheckAngle || Vector3.Dot(transform.forward, dirToTarget) > Mathf.Cos(angle) * Mathf.Deg2Rad)
			{
				Stat st = go.GetComponent<Stat>();
				st.OnAttacked(stat);
			}
		}
	}

	// TODO: OnAttacked(Stat attacker)
	public override void OnAttacked(Stat attacker)
	{
		if (isJumping) return;
		stat.OnAttacked(attacker);
	}

	public void SlashEvent()
	{
		switch (AttackType)
		{
			case PlayerAttack.None:
				slash.Play();
				break;
			case PlayerAttack.Attack1:
				slashHorizontal.Play();
				break;
			case PlayerAttack.Attack2:
				slashVertical.Play();
				break;
			case PlayerAttack.Attack3:
				break;
			case PlayerAttack.Attack4:
				slashHorizontal.Play();
				break;
			case PlayerAttack.Attack5:
				break;
		}
	}

	public void SkillEvent()
	{
		isJumping = false;
		switch (SkillType)
		{
			case PlayerSkill.Skill1:
				sk1.Play();
				break;
			case PlayerSkill.Skill2:
				sk2.Play();
				break;
			case PlayerSkill.Skill3:
				sk3.Play();
				break;
			case PlayerSkill.Skill4:
				buff.Play();
				stat.Hp = Mathf.Max(stat.MaxHp, stat.Hp + stat.Attack * 5);
				break;
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
			case Define.State.Attack:
				if(Util.IsAnimationDone(anim, attackAnim[AttackType]))
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
