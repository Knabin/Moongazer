using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
	enum Images
	{
		ItemImage,
	}

	string _name;

	public override void Init()
	{
		Bind<Image>(typeof(Images));
		//Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

		Get<Image>((int)Images.ItemImage).gameObject.BindEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); });
	}

	public void SetInfo(int index)
	{
		Data.Item item = Managers.Data.ItemDict[index];
		Sprite sp = Managers.Resource.Load<Sprite>("Textures/Icons/Inventory/" + item.name);
		if (sp != null)
		{
			Color color = Get<Image>((int)Images.ItemImage).color;
			color.a = 1;

			Get<Image>((int)Images.ItemImage).sprite = sp;
			Get<Image>((int)Images.ItemImage).color = color;
		}
	}
}
