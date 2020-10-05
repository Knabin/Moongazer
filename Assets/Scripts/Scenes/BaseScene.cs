using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
	public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;

	void Awake()
	{
		Init();
	}

	protected virtual void Init()
	{
		Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
		if (obj == null)
			Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";

		GameObject cam = GameObject.Find("@Rig");
		if(cam == null)
		{
			cam = new GameObject { name = "@Rig" };
			cam.AddComponent<CameraController>();
			Camera.main.transform.parent = cam.transform;
		}
	}

	public abstract void Clear();
}
