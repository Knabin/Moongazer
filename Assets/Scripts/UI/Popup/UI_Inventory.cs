using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inventory : UI_Popup
{
	enum GameObjects
	{
		GridPanel,
		Info,
	}

	enum Texts
	{
		GoldText,
		ItemName,
		ItemInfo,
	}

	enum Images
	{
		Close,
		CloseButton,
	}

	bool isClicked = false;

	PlayerInven inven;

	public override void Init()
    {
		base.Init();

		Bind<GameObject>(typeof(GameObjects));
		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
		foreach (Transform child in gridPanel.transform)
			Managers.Resource.Destroy(child.gameObject);

		Get<Image>((int)Images.Close).gameObject.BindEvent(OnButtonClicked_Back);
		Get<Image>((int)Images.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Text>((int)Texts.GoldText).text = Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold.ToString();

		inven = Managers.Game.GetPlayer().GetComponent<PlayerInven>();
		List<int> list = new List<int>(inven.Inventory.Keys);

		int num = inven.Inventory.Count;
		int check = 0;

		for (int i = 0; i < 25; ++i)
		{
			GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
			UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
			if (check < num)
			{
				invenItem.SetInfo(inven.Inventory[list[check++]], this);
			}
			invenItem.transform.localScale = new Vector3(1, 1);
		}

		Get<GameObject>((int)GameObjects.Info).SetActive(false);
	}

	public void SetItemInfo(int index)
	{
		Get<Text>((int)Texts.ItemName).text = Managers.Data.ItemDict[index].name;
		Get<Text>((int)Texts.ItemInfo).text = Managers.Data.ItemDict[index].info;
		Get<GameObject>((int)GameObjects.Info).SetActive(true);
		isClicked = true;
	}

	public void OnButtonClicked_Back(PointerEventData data)
	{
		if(isClicked)
		{
			Get<GameObject>((int)GameObjects.Info).SetActive(false);
			isClicked = false;
		}
		else ClosePopupUI();
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
