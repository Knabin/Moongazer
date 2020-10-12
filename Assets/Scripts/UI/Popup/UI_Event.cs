using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Event : UI_Popup
{
	enum Texts
	{
		DialogText,
	}

	enum Images
	{
		ConfirmButton,
	}

	int _id, _index = 0;

	public override void Init()
	{
		base.Init();

		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		Get<Image>((int)Images.ConfirmButton).gameObject.BindEvent(OnButtonClicked_Confirm);
	}

	public void SetId(int id)
	{
		_id = id;
		SetText();
	}

	public void SetIndex(int index)
	{
		_index = index;
		SetText();
	}

	void SetText()
	{
		string talk = Managers.Talk.GetTalk(_id, _index);
		Get<Text>((int)Texts.DialogText).text = talk;
	}

	public void OnButtonClicked_Confirm(PointerEventData data)
	{
		ClosePopupUI();
	}
}
