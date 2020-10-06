using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Joystick : UI_Scene
{
	RectTransform rect;
	RectTransform handle;

	Vector2 touch = Vector2.zero;
	JoystickValue value;

	float widthHalf;

	enum GameObjects
	{
		FixedJoystick,
		Handle,
		Line,
	}

	public override void Init()
	{
		base.Init();

		Bind<GameObject>(typeof(GameObjects));

		GameObject fixedJoystick = Get<GameObject>((int)GameObjects.FixedJoystick);
		GameObject handleObj = Get<GameObject>((int)GameObjects.Handle);

		rect = fixedJoystick.GetComponent<RectTransform>();
		handle = handleObj.GetComponent<RectTransform>();
		value = Managers.Game.GetPlayer().GetOrAddComponent<JoystickValue>();

		widthHalf = rect.sizeDelta.x * 0.5f;

		handleObj.BindEvent(OnJoystickDrag, Define.UIEvent.Drag);
		handleObj.BindEvent(OnJoystickPointDown, Define.UIEvent.PointerDown);
		handleObj.BindEvent(OnJoystickPointUp, Define.UIEvent.PointerUp);
	}

	public void OnJoystickPointDown(PointerEventData data)
	{
		OnJoystickDrag(data);
	}

	public void OnJoystickPointUp(PointerEventData data)
	{
		handle.anchoredPosition = Vector2.zero;
		value.joyTouch = Vector2.zero;
	}

	public void OnJoystickDrag(PointerEventData data)
	{
		Vector2 mouse;
		if(RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, 
			data.position, data.pressEventCamera, out Vector2 localVector))
		{
			if (localVector.magnitude < widthHalf)
				handle.transform.localPosition = localVector;
			else
				handle.transform.localPosition = localVector.normalized * widthHalf;
			mouse = Vector2.ClampMagnitude(localVector, widthHalf) / widthHalf;
			value.joyTouch = mouse;
		}
	}

	public void Test(PointerEventData data)
	{
		Debug.Log("클릭");
	}
}
