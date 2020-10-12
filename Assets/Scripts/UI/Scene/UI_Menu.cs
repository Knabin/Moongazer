using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Menu : UI_Scene
{
	enum Images
	{
		Character,
		Inventory,
		Setting,
	}

	public override void Init()
	{
		base.Init();

		Bind<Image>(typeof(Images));
		Get<Image>((int)Images.Character).gameObject.BindEvent(OnButtonClicked_Character);
		Get<Image>((int)Images.Inventory).gameObject.BindEvent(OnButtonClicked_Inventory);
		Get<Image>((int)Images.Setting).gameObject.BindEvent(OnButtonClicked_Setting);
	}

	public void OnButtonClicked_Character(PointerEventData data)
	{
		Managers.UI.ShowPopupUI<UI_Character>();
	}

	public void OnButtonClicked_Inventory(PointerEventData data)
	{
		Managers.UI.ShowPopupUI<UI_Inventory>();
		//Managers.UI.ShowPopupUI<UI_Dialog>();
	}

	public void OnButtonClicked_Setting(PointerEventData data)
	{
		Managers.UI.ShowPopupUI<UI_Setting>();
	}
}
