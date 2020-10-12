using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Root : MonoBehaviour
{
	private void OnEnable()
	{
		int count = transform.childCount;
		for(int i = 0; i < count; ++i)
		{
			Transform child = transform.GetChild(i);
			child.gameObject.SetActive(true);
		}
	}

	private void OnDisable()
	{
		int count = transform.childCount;
		for (int i = 0; i < count; ++i)
		{
			Transform child = transform.GetChild(i);
			child.gameObject.SetActive(false);
		}
	}
}
