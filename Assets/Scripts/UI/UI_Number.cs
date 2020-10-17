using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Number : UI_Popup
{
	enum Images
	{
		Back,
	}

	enum Texts
	{
		Answer1,
		Answer2,
		Answer3,
		Answer4,
	}

	List<int> answer = new List<int> { 8, 1, 7, 3 };
	List<int> input = new List<int>();

	public override void Init()
	{
		base.Init();

		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));

		Get<Image>((int)Images.Back).gameObject.BindEvent(OnButtonClicked_Close);
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}

	public void ClickButton(int num)
	{
		Debug.Log($"{num} 클릭!");
		Get<Text>(input.Count).text = num.ToString();
		input.Add(num);

		if(input.Count == 4)
		{
			// 답 체크
			for(int i = 0; i < 4; ++i)
			{
				// 답 아니면 input 날리고 UI 0으로 초기화
				if (answer[i] != input[i])
				{
					input.Clear();
					Get<Text>((int)Texts.Answer1).text = "0";
					Get<Text>((int)Texts.Answer2).text = "0";
					Get<Text>((int)Texts.Answer3).text = "0";
					Get<Text>((int)Texts.Answer4).text = "0";
					return;
				}
			}

			// 정답이라면
			Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(12);
			// TODO: 소리 재생?
			ClosePopupUI();
		}
	}
}
