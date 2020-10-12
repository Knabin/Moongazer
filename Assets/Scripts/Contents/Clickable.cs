using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
	[SerializeField]
	Define.ClickableType type = Define.ClickableType.None;

	public bool isCanTouchAgain = true;
	public bool isTouch = false;

	public void OnClick()
	{
		if (type == Define.ClickableType.PasswordDoor)
		{
			Managers.UI.ShowPopupUI<UI_Number>();
			if (GetComponent<Door>()._isOpen) isTouch = true;
		}
		else
		{
			UI_Event ev = Managers.UI.ShowPopupUI<UI_Event>();
			ev.SetId(GetComponent<ObjData>().id);
			if (!isCanTouchAgain && isTouch) ev.SetIndex(1);
			Managers.Sound.Play("Sfx/QuestOk", Define.Sound.Effect);
			if (!isTouch)
			{
				switch (type)
				{
					case Define.ClickableType.Table:
						Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(1);
						break;
				}
			}
			isTouch = true;
		}
	}
}
