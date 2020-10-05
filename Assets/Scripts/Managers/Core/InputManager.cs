using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
	// 일종의 delegate, listener 패턴
	public Action KeyAction = null;
	public Action<Define.MouseEvent> MouseAction = null;

	bool _pressed = false;
	float _pressedTime = 0;

	public void OnUpdate()
	{
		/*if (EventSystem.current.IsPointerOverGameObject())
			return;*/

		if (Input.anyKey && KeyAction != null)
			KeyAction.Invoke();     // 키 액션 있었다고 알리기

		if (MouseAction != null)
		{
			if (Input.GetMouseButton(0))
			{
				if (!_pressed)
				{
					MouseAction.Invoke(Define.MouseEvent.PointerDown);
					_pressedTime = Time.time;
				}
				MouseAction.Invoke(Define.MouseEvent.Press);
				_pressed = true;
			}
			else
			{
				if (_pressed)
				{
					if (Time.time < _pressedTime * 0.2f)
						MouseAction.Invoke(Define.MouseEvent.Click);
					else
						MouseAction.Invoke(Define.MouseEvent.PointerUp);
				}
				_pressed = false;
				_pressedTime = 0;
			}
		}
	}

	public void Clear()
	{
		KeyAction = null;
		MouseAction = null;
	}
}
