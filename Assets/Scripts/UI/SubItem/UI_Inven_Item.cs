using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
	enum Images
	{
		ItemImage,
		ItemCountImage,
	}

	enum Texts
	{
		ItemCountText,
	}

	string _name;
	UI_Inventory _parent;
	Data.Item _item;

	public override void Init()
	{
		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));

		Get<Image>((int)Images.ItemCountImage).gameObject.SetActive(false);
		Get<Image>((int)Images.ItemImage).gameObject.BindEvent(OnButtonClicked_Info);
	}

	public void SetInfo(Data.Inven inven, UI_Inventory parent)
	{
		_parent = parent;

		_item = Managers.Data.ItemDict[inven.index];
		Sprite sp = Managers.Resource.Load<Sprite>("Textures/Icons/Inventory/" + _item.image);
		if (sp != null)
		{
			Color color = Get<Image>((int)Images.ItemImage).color;
			color.a = 1;

			Get<Image>((int)Images.ItemImage).sprite = sp;
			Get<Image>((int)Images.ItemImage).color = color;
		}

		if (inven.amount > 1)
		{
			Get<Image>((int)Images.ItemCountImage).gameObject.SetActive(true);
			Get<Text>((int)Texts.ItemCountText).text = inven.amount.ToString();
		}
	}

	public void OnButtonClicked_Info(PointerEventData data)
	{
		if (_item == null) return;
		_parent.SetItemInfo(_item.number);
	}
}
