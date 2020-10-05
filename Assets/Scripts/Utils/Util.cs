using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
	// 컴포넌트를 가지고 있다면 반환하고, 가지고 있지 않다면 추가해서 반환하는 함수
	public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
	{
		T component = go.GetComponent<T>();

		if (component == null)
			component = go.AddComponent<T>();

		return component;
	}

	public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
	{
		Transform transform = FindChild<Transform>(go, name, recursive);

		if (transform != null)
			return transform.gameObject;

		return null;
	}

	public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
	{
		if (go == null)
			return null;

		// 자신과 자신의 직속 자식만 찾는 경우
		if (!recursive)
		{
			for (int i = 0; i < go.transform.childCount; ++i)
			{
				Transform transform = go.transform.GetChild(i);
				if (string.IsNullOrEmpty(name) || transform.name == name)
				{
					T component = transform.GetComponent<T>();
					if (component != null)
						return component;
				}
			}
		}
		else
		{
			foreach (T component in go.GetComponentsInChildren<T>())
			{
				// 이름이 비어 있거나, 찾았을 경우
				if (string.IsNullOrEmpty(name) || component.name == name)
					return component;
			}
		}

		// 못 찾은 경우
		return null;
	}
}
