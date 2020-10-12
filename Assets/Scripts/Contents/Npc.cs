using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
	UI_Dialog _dialog;

	void Start()
	{
		Managers.Input.MouseAction -= OnClickNpc;
		Managers.Input.MouseAction += OnClickNpc;
	}

	void OnClickNpc(Define.MouseEvent evt)
	{
		if(evt == Define.MouseEvent.PointerDown)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 100.0f);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100.0f, 1 << (int)Define.Layer.Npc))
			{
				if (_dialog == null)
				{
					_dialog = Managers.UI.ShowPopupUI<UI_Dialog>();
					_dialog.SetId(GetComponent<ObjData>().id);
				}
				else
				{
					
					_dialog.gameObject.SetActive(true);
				}

			}
		}
	}
}
