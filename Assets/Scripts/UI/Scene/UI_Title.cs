using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Title : UI_Scene
{
	enum Buttons
	{
		StartButton,
		OptionButton,
		ExitButton,
	}

	public override void Init()
	{
		base.Init();

		Bind<Button>(typeof(Buttons));
		Get<Button>((int)Buttons.StartButton).gameObject.BindEvent(OnButtonClicked_Start);
		Get<Button>((int)Buttons.OptionButton).gameObject.BindEvent(OnButtonClicked_Option);
		Get<Button>((int)Buttons.ExitButton).gameObject.BindEvent(OnButtonClicked_Exit);
	}

	public void OnButtonClicked_Start(PointerEventData data)
	{
		Managers.Scene.LoadScene(Define.Scene.FieldScene);
	}

	public void OnButtonClicked_Option(PointerEventData data)
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
