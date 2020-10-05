using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
	enum Images
	{
		Close,
		CloseButton,
	}

    public override void Init()
    {
		base.Init();

		Bind<Image>(typeof(Images));

		Get<Image>((int)Images.Close).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Image>((int)Images.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
