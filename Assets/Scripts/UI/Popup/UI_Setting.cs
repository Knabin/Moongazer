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
		Get<Button>((int)Buttons.Save).gameObject.BindEvent(OnButtonClicked_Save);
		Get<Button>((int)Buttons.Options).gameObject.BindEvent(OnButtonClicked_Options);
		Get<Button>((int)Buttons.Exit).gameObject.BindEvent(OnButtonClicked_Exit);

		Time.timeScale = 0.0f;
	}

	public void OnButtonClicked_Resume(PointerEventData data)
	{
		Time.timeScale = 1.0f;
		ClosePopupUI();
	}

	public void OnButtonClicked_Save(PointerEventData data)
	{
		Managers.Game.SaveData();
		Time.timeScale = 1.0f;
		ClosePopupUI();
	}

	public void OnButtonClicked_Options(PointerEventData data)
	{
		Managers.UI.ShowPopupUI<UI_Options>();
	}

	public void OnButtonClicked_Exit(PointerEventData data)
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
	}
}
