using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
	enum GameObjects
	{
		GridPanel,
	}

	enum Buttons
	{
		CloseButton,
	}

	enum Texts
	{
		GoldText,
	}

	public override void Init()
	{
		base.Init();

		Bind<GameObject>(typeof(GameObjects));
		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));

		GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
		foreach (Transform child in gridPanel.transform)
			Managers.Resource.Destroy(child.gameObject);

		Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
