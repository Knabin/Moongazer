using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IDragHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
	//public Action<PointerEventData> OnBeginDragHandler = null;
	public Action<PointerEventData> OnClickHandler = null;
	public Action<PointerEventData> OnDragHandler = null;
	public Action<PointerEventData> OnPointerDownHandler = null;
	public Action<PointerEventData> OnPointerUpHandler = null;

	//public void OnBeginDrag(PointerEventData eventData)
	//{
	//    if (OnBeginDragHandler != null)
	//        OnBeginDragHandler.Invoke(eventData);
	//}

	public void OnDrag(PointerEventData eventData)
	{
		if (OnDragHandler != null)
			OnDragHandler.Invoke(eventData);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if (OnClickHandler != null)
			OnClickHandler.Invoke(eventData);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (OnPointerDownHandler != null)
			OnPointerDownHandler.Invoke(eventData);
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (OnPointerUpHandler != null)
			OnPointerUpHandler.Invoke(eventData);
	}
}
