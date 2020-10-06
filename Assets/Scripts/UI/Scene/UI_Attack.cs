using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Attack : UI_Scene
{
	enum GameObjects
	{
		Skill1,
		Skill2,
		Skill3,
		Change,
		Potion,
		Attack,
		Defence,
	}

	public override void Init()
	{
		base.Init();

		Bind<GameObject>(typeof(GameObjects));

		Get<GameObject>((int)GameObjects.Attack).BindEvent(OnButtonClicked_Attack);
	}

	public void OnButtonClicked_Attack(PointerEventData data)
	{
		Managers.Game.GetPlayer().GetComponent<PlayerController>().StartAttack();
	}
}
