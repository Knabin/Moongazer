using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;

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
	
	enum Images
	{
		BgmHandle,
		FbxHandle,
	}

	enum Buttons
	{
		CloseButton,
	}

	public AudioMixer masterMixer;

	public override void Init()
	{
		base.Init();

		Bind<Slider>(typeof(Sliders));
		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));
		Bind<Button>(typeof(Buttons));

		float bgm;
		float sfx;

		masterMixer.GetFloat("BGM", out bgm);
		masterMixer.GetFloat("SFX", out sfx);

		Get<Slider>((int)Sliders.BgmSlider).value = (bgm + 40f) / 40.0f;
		Get<Slider>((int)Sliders.FbxSlider).value = (sfx + 40f) / 40.0f;
		Get<Text>((int)Texts.BgmValueText).text = ((int)(Get<Slider>((int)Sliders.BgmSlider).value * 100)).ToString();
		Get<Text>((int)Texts.FbxValueText).text = ((int)(Get<Slider>((int)Sliders.FbxSlider).value * 100)).ToString();

		Get<Slider>((int)Sliders.BgmSlider).gameObject.BindEvent(OnBgmValueChanged, Define.UIEvent.Drag);
		Get<Slider>((int)Sliders.FbxSlider).gameObject.BindEvent(OnFbxValueChanged, Define.UIEvent.Drag);
		Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnFbxValueChanged(PointerEventData data)
	{
		Get<Text>((int)Texts.FbxValueText).text = ((int)(Get<Slider>((int)Sliders.FbxSlider).value * 100)).ToString();
		float sound = Get<Slider>((int)Sliders.FbxSlider).value;

		if (sound <= 0.01f) masterMixer.SetFloat("SFX", -80.0f);
		else
		{
			masterMixer.SetFloat("SFX", sound * 40.0f - 40f);
		}
	}

	public void OnBgmValueChanged(PointerEventData data)
	{
		Get<Text>((int)Texts.BgmValueText).text = ((int)(Get<Slider>((int)Sliders.BgmSlider).value * 100)).ToString();
		float sound = Get<Slider>((int)Sliders.BgmSlider).value;

		if (sound <= 0.01f) masterMixer.SetFloat("BGM", -80.0f);
		else
		{
			masterMixer.SetFloat("BGM", sound * 40.0f - 40f);
		}
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
