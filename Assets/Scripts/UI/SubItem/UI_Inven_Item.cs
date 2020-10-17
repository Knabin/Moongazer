using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

	public override void Init()
	{
		Bind<Image>(typeof(Images));
		Bind<Text>(typeof(Texts));



		Get<Image>((int)Images.ItemCountImage).gameObject.SetActive(false);
	}

	public void SetInfo(Data.Inven inven)
	{
		Data.Item item = Managers.Data.ItemDict[inven.index];
		Sprite sp = Managers.Resource.Load<Sprite>("Textures/Icons/Inventory/" + item.image);
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
}
