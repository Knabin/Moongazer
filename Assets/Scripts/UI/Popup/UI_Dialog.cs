using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Dialog : UI_Popup
{
	enum Texts
	{
		Text,
	}

	enum Images
	{
		ConfirmButton,
		CancelButton,
	}

	public override void Init()
	{
		base.Init();

		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		Get<Image>((int)Images.ConfirmButton).gameObject.BindEvent(OnButtonClicked_Confirm);
		Get<Image>((int)Images.CancelButton).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnButtonClicked_Confirm(PointerEventData data)
	{
		// TODO: 퀘스트 수락
		ClosePopupUI();
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
