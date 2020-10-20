using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Shop_Item : UI_Base
{
	enum Images
	{
		Icon,
	}

	enum Texts
	{
		Name,
		Price,
	}

	enum GameObjects
	{
		BuyButton,
	}

	UI_Shop _parent;
	Data.Item _item;

	public override void Init()
	{
		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));
		Bind<GameObject>(typeof(GameObjects));

		Get<GameObject>((int)GameObjects.BuyButton).BindEvent(OnButtonClicked_Buy);
	}

	public void SetInfo(Data.Item item, UI_Shop parent)
	{
		_parent = parent;
		_item = item;

		Sprite sp = Managers.Resource.Load<Sprite>("Textures/Icons/Inventory/" + _item.image);

		if (sp != null)
		{
			Get<Image>((int)Images.Icon).sprite = sp;
		}
		Get<Text>((int)Texts.Name).text = item.name;
		Get<Text>((int)Texts.Price).text = item.price.ToString();
	}

	public void OnButtonClicked_Buy(PointerEventData data)
	{
		if (_item == null) return;
		if (_parent._isOpenNotice) return;

		if (Managers.Game.GetPlayer().GetComponent<PlayerStat>().BuyItem(_item.price))
		{
			Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(_item.number);
			_parent.SetNoticeObject(true, _item);
		}
		else
		{
			_parent.SetNoticeObject(false);
		}
	}
}
