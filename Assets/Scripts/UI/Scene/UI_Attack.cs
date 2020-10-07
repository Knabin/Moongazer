using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Attack : UI_Scene
{
	bool isAttacking = false;
	PlayerController playerController;

	enum GameObjects
	{
		Skill1,
		Skill2,
		Skill3,
		Skill4,
		Potion,
		Attack,
		Defence,
	}

	enum Images
	{
		Cooldown1,
		Cooldown2,
		Cooldown3,
		Cooldown4,
		Icon1,
		Icon2,
		Icon3,
		Icon4,
	}

	public override void Init()
	{
		base.Init();

		Bind<GameObject>(typeof(GameObjects));
		Bind<Image>(typeof(Images));

		Get<GameObject>((int)GameObjects.Attack).BindEvent(OnButtonClicked_Attack);
		Get<GameObject>((int)GameObjects.Skill1).BindEvent(OnButtonClicked_Skill1);
		Get<GameObject>((int)GameObjects.Skill2).BindEvent(OnButtonClicked_Skill2);
		Get<GameObject>((int)GameObjects.Skill3).BindEvent(OnButtonClicked_Skill3);
		Get<GameObject>((int)GameObjects.Skill4).BindEvent(OnButtonClicked_Skill4);
		Get<GameObject>((int)GameObjects.Defence).BindEvent(OnButtonClicked_Defence);

		playerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
	}

	public void OnButtonClicked_Attack(PointerEventData data)
	{
		if (playerController.State == Define.State.Skill) return;

		playerController.StartAttack();
	}

	public void OnButtonClicked_Skill1(PointerEventData data)
	{
		if (playerController.State == Define.State.Skill) return;

		playerController.StartSkill(1);
		StartCoroutine(FillAmount(Get<Image>((int)Images.Cooldown1), Get<Image>((int)Images.Icon1), 4.0f));
	}

	public void OnButtonClicked_Skill2(PointerEventData data)
	{
		if (playerController.State == Define.State.Skill) return;

		playerController.StartSkill(2);
		StartCoroutine(FillAmount(Get<Image>((int)Images.Cooldown2), Get<Image>((int)Images.Icon2), 5.0f));
	}

	public void OnButtonClicked_Skill3(PointerEventData data)
	{
		if (playerController.State == Define.State.Skill || Get<Image>((int)Images.Cooldown3).fillAmount > 0.0f) return;

		playerController.StartSkill(3);
		StartCoroutine(FillAmount(Get<Image>((int)Images.Cooldown3), Get<Image>((int)Images.Icon3), 8.0f));
	}

	public void OnButtonClicked_Skill4(PointerEventData data)
	{
		if (playerController.State == Define.State.Skill) return;

		playerController.StartSkill(4);
		StartCoroutine(FillAmount(Get<Image>((int)Images.Cooldown4), Get<Image>((int)Images.Icon4), 15.0f));
	}

	public void OnButtonClicked_Defence(PointerEventData data)
	{
		playerController.StartDefence();
	}

	IEnumerator FillAmount(Image image, Image icon, float time)
	{
		// amount 범위 0 ~ 1
		float cooldown = 0f;
		image.fillAmount = 0f;
		icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0.5f);

		while (cooldown < time)
		{
			cooldown += Time.deltaTime;
			image.fillAmount = cooldown / time;
			yield return null;
		}

		image.fillAmount = 0;
		icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 1.0f);
		yield break;
	}
}
