using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Character : UI_Popup
{
	enum Images
	{
		Close,
		CloseButton,
	}

	enum Texts
	{
		LevelText,
		HpText,
		ExpText,
		AtkText,
	}

	public override void Init()
	{
		base.Init();

		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));

		Get<Image>((int)Images.Close).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Image>((int)Images.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);

		SetStat();
	}


	void SetStat()
	{
		PlayerStat stat = Managers.Game.GetPlayer().GetComponent<PlayerStat>();
		Data.Stat temp;
		int nextExp = -1;
		int nowExp = 0;

		if(Managers.Data.StatDict.TryGetValue(stat.Level, out temp))
			nowExp = temp.totalExp;

		if (Managers.Data.StatDict.TryGetValue(stat.Level + 1, out temp) != false)
			nextExp = temp.totalExp;

		Get<Text>((int)Texts.LevelText).text = stat.Level.ToString();
		Get<Text>((int)Texts.HpText).text = $"{stat.Hp} / {stat.MaxHp}";
		Get<Text>((int)Texts.ExpText).text = $"{stat.Exp - nowExp} / {nextExp - nowExp}";
		Get<Text>((int)Texts.AtkText).text = stat.Attack.ToString();
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
