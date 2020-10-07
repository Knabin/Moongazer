using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Status : UI_Scene
{
	enum Images
	{
		HP,
		Exp,
	}

	enum Texts
	{
		LevelText,
		HPText,
		ExpText,
	}

	public override void Init()
	{
		base.Init();

		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));

		SetUI();
	}

	private void LateUpdate()
	{
		SetUI();
	}

	public void SetUI()
	{
		PlayerStat stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();

		Data.Stat st;
		if (Managers.Data.StatDict.TryGetValue(stat.Level + 1, out st) == false)
			st.totalExp = stat.Exp;

		float hpPercent = (float)stat.Hp / stat.MaxHp;
		float expPercent = (float)stat.Exp / st.totalExp;

		Get<Text>((int)Texts.LevelText).text = $"{stat.Level}";
		Get<Text>((int)Texts.HPText).text = $"{stat.Hp}";
		Get<Text>((int)Texts.ExpText).text = $"{expPercent * 100}%";

		Get<Image>((int)Images.HP).fillAmount = hpPercent;
		Get<Image>((int)Images.Exp).fillAmount = expPercent;
	}
}
