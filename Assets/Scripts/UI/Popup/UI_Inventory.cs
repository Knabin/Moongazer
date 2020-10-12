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
	}

	enum Texts
	{
		GoldText,
	}

	enum Images
	{
		Close,
		CloseButton,
	}

    public override void Init()
    {
		base.Init();

		Bind<GameObject>(typeof(GameObjects));
		Bind<Text>(typeof(Texts));
		Bind<Image>(typeof(Images));

		GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
		foreach (Transform child in gridPanel.transform)
			Managers.Resource.Destroy(child.gameObject);

		Get<Image>((int)Images.Close).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Image>((int)Images.CloseButton).gameObject.BindEvent(OnButtonClicked_Close);
		Get<Text>((int)Texts.GoldText).text = Managers.Game.GetPlayer().GetComponent<PlayerStat>().Gold.ToString();

		int num = Managers.Game.GetPlayer().GetComponent<PlayerInven>().Inventory.Count;

		for (int i = 0; i < 25; ++i)
		{
			GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
			UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
			if(i < num)
			{
				invenItem.SetInfo(i+1);
			}
			invenItem.transform.localScale = new Vector3(1, 1);
		}
	}

	public void OnButtonClicked_Close(PointerEventData data)
	{
		ClosePopupUI();
	}
}
