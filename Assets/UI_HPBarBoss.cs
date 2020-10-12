using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBarBoss : UI_Scene
{
	enum GameObjects
	{
		HPBar,
	}

	private Stat _stat;

	public override void Init()
	{
		Bind<GameObject>(typeof(GameObjects));
		_stat = GameObject.FindWithTag("Boss").GetComponent<Stat>();
	}

	private void Update()
	{
		float ratio = (float)_stat.Hp / _stat.MaxHp;
		SetHpRatio(ratio);
	}

	public void SetHpRatio(float ratio)
	{
		Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
	}
}
