using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Options : UI_Popup
{
	enum Sliders
	{
		BgmSlider,
		FbxSlider,
	}

	enum Texts
	{
		BgmValueText,
		FbxValueText,
	}

	enum Buttons
	{
		CloseButton,
	}

	public override void Init()
	{
		base.Init();

		Bind<Slider>(typeof(Sliders));
		Bind<Text>(typeof(Texts));
		Bind<Button>(typeof(Buttons));

		Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnFbxValueChanged()
	{
		Get<Text>((int)Texts.FbxValueText).text = ((int)(Get<Slider>((int)Sliders.FbxSlider).value * 100)).ToString();
		// TODO: 음량 변경 작업
	}

	public void OnBgmValueChanged()
	{
		Get<Text>((int)Texts.BgmValueText).text = ((int)(Get<Slider>((int)Sliders.BgmSlider).value * 100)).ToString();
		// TODO: 음량 변경 작업
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
