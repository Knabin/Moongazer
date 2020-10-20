using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop : UI_Popup
{
	List<Data.Item> _itemList = new List<Data.Item>();

	enum GameObjects
	{
		GridPanel,
	}

	enum Buttons
	{
		CloseButton,
	}

	enum Texts
	{
		GoldText,
		NoticeText,
	}

	enum Images
	{
		NoticeImage,
	}

	public bool _isOpenNotice = false;

	public override void Init()
	{
		base.Init();

		Bind<GameObject>(typeof(GameObjects));
		Bind<Button>(typeof(Buttons));
		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
		foreach (Transform child in gridPanel.transform)
			Managers.Resource.Destroy(child.gameObject);

		_itemList.Add(Managers.Data.ItemDict[1]);
		_itemList.Add(Managers.Data.ItemDict[1]);
		_itemList.Add(Managers.Data.ItemDict[1]);
		_itemList.Add(Managers.Data.ItemDict[1]);

		for(int i = 0; i < _itemList.Count; ++i)
		{
			GameObject item = Managers.UI.MakeSubItem<UI_Shop_Item>(parent: gridPanel.transform).gameObject;
			UI_Shop_Item shopItem = item.GetOrAddComponent<UI_Shop_Item>();
			shopItem.SetInfo(_itemList[i], this);
		}

		Get<Button>((int)Buttons.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);

		Get<Text>((int)Texts.GoldText).text = Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold.ToString();
		Get<Image>((int)Images.NoticeImage).gameObject.SetActive(false);
	}

	public void SetNoticeObject(bool isComplete, Data.Item item = null)
	{
		_isOpenNotice = true;
		if (isComplete)
		{
			Get<Text>((int)Texts.NoticeText).text = $"{item.name}을(를) 구매했습니다.";
		}
		else
		{
			Get<Text>((int)Texts.NoticeText).text = "골드가 부족합니다.";
		}
		Get<Text>((int)Texts.GoldText).text = Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold.ToString();
		StartCoroutine("ShowNoticeObject");
	}

	IEnumerator ShowNoticeObject()
	{
		Color origin = Get<Image>((int)Images.NoticeImage).color;
		Color color = origin;
		color.a = 0;

		Get<Image>((int)Images.NoticeImage).color = color;
		Get<Text>((int)Texts.NoticeText).color = color;

		Get<Image>((int)Images.NoticeImage).gameObject.SetActive(true);

		while (color.a < origin.a)
		{
			yield return null;

			color.a += 0.06f;
			if (color.a > 1.0f) color.a = 1.0f;

			Get<Image>((int)Images.NoticeImage).color = color;
			Get<Text>((int)Texts.NoticeText).color = color;
		}

		while (color.a > 0f)
		{
			yield return null;

			color.a -= 0.08f;
			if (color.a < 0.0f) color.a = 0.0f;

			Get<Image>((int)Images.NoticeImage).color = color;
			Get<Text>((int)Texts.NoticeText).color = color;
		}

		color.a = 1.0f;

		Get<Image>((int)Images.NoticeImage).color = color;
		Get<Text>((int)Texts.NoticeText).color = color;

		Get<Image>((int)Images.NoticeImage).gameObject.SetActive(false);
		_isOpenNotice = false;
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
