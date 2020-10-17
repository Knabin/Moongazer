using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
	enum GameObjects
	{
		HPBar,
	}

	private Stat _stat;

	public override void Init()
	{
		Bind<GameObject>(typeof(GameObjects));
		_stat = transform.parent.GetComponent<Stat>();
	}

	private void Update()
	{
		Transform parent = transform.parent;
		transform.position = parent.position + Vector3.up * (parent.GetComponent<NavMeshAgent>().height);
		if(Camera.main != null) transform.rotation = Camera.main.transform.rotation;

		float ratio = (float)_stat.Hp / _stat.MaxHp;
		SetHpRatio(ratio);
	}

	public void SetHpRatio(float ratio)
	{
		GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
	}
}
