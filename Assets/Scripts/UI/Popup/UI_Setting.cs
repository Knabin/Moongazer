using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
	enum Buttons
	{
		Resume,
		Save,
		Options,
		Exit,
	}

	public override void Init()
	{
		base.Init();

		Bind<Button>(typeof(Buttons));

		Get<Button>((int)Buttons.Resume).gameObject.BindEvent(OnButtonClicked_Resume);
		Get<Button>((int)Buttons.Options).gameObject.BindEvent(OnButtonClicked_Options);

		Time.timeScale = 0.0f;
	}

	public void OnButtonClicked_Resume(PointerEventData data)
	{
		Time.timeScale = 1.0f;
		ClosePopupUI();
	}

	public void OnButtonClicked_Options(PointerEventData data)
	{
		Managers.UI.ShowPopupUI<UI_Options>();
	}
}
