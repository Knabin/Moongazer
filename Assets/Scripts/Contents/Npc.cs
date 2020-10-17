using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
	UI_Dialog _dialog;
	ObjData _objData;

	void Start()
	{
		Managers.Input.MouseAction -= OnClickNpc;
		Managers.Input.MouseAction += OnClickNpc;

		_objData = GetComponent<ObjData>();
	}

	void OnClickNpc(Define.MouseEvent evt)
	{
		if (evt == Define.MouseEvent.PointerDown)
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction * 100.0f);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 100.0f, 1 << (int)Define.Layer.Npc))
			{
				if (hit.transform.gameObject != this.gameObject) return;
				switch (_objData.id)
				{
					case 1000:
						if (_dialog == null)
						{
							_dialog = Managers.UI.ShowPopupUI<UI_Dialog>();
							_dialog.SetId(GetComponent<ObjData>().id);
						}
						else
						{
							_dialog.gameObject.SetActive(true);
						}
						break;
					case 1001:
						Managers.UI.ShowPopupUI<UI_Shop>();
						break;
					default:
						break;
				}
			}
		}
	}
}
