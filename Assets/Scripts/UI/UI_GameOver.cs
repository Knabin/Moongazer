using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_GameOver : UI_Popup
{
	enum Buttons
	{
		Title,
		Exit,
	}

	public override void Init()
	{
		base.Init();

		Bind<Button>(typeof(Buttons));

		Get<Button>((int)Buttons.Title).gameObject.BindEvent(OnButtonClicked_Resume);
		Get<Button>((int)Buttons.Exit).gameObject.BindEvent(OnButtonClicked_Exit);

		Time.timeScale = 0.0f;
	}

	public void OnButtonClicked_Resume(PointerEventData data)
	{
		Time.timeScale = 1.0f;
		ClosePopupUI();
		Managers.Scene.LoadScene(Define.Scene.TitleScene);
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
