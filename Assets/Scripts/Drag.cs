using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
{
	private Transform _itemTr;
	[SerializeField]
	private Transform _inventoryTr;
	public static GameObject draggingItem = null;

	void Start()
    {
		_itemTr = GetComponent<Transform>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		_itemTr.position = Input.mousePosition;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		transform.SetParent(_inventoryTr);
		draggingItem = gameObject;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		draggingItem = null;
	}

	public void OnDrop(PointerEventData eventData)
	{
		throw new System.NotImplementedException();
	}
}
