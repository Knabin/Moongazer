using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePuzzle : MonoBehaviour
{
	[SerializeField]
	List<CirclePuzzleSub> _list = new List<CirclePuzzleSub>();

	bool _isClear = false;

	int count = 0;

	public void SetIsDown(int index)
	{
		if (_isClear) return;

		int left = index - 1;
		int right = index + 1;
		int top = index - 3;
		int bottom = index + 3;

		if (left < 0 || left / 3 != index / 3)				left = -1;
		if (right >= _list.Count || right / 3 != index / 3) right = -1;
		if (top < 0)										top = -1;
		if (bottom >= _list.Count)							bottom = -1;

		if (left != -1) _list[left].IsDown = !_list[left].IsDown;
		if (right != -1) _list[right].IsDown = !_list[right].IsDown;
		if (top != -1) _list[top].IsDown = !_list[top].IsDown;
		if (bottom != -1) _list[bottom].IsDown = !_list[bottom].IsDown;

		for(int i = 0; i < _list.Count; ++i)
		{
			if (!_list[i].IsDown) return;
			count++;
		}

		Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(2);

		for (int i = 0; i < _list.Count; ++i)
			_list[i].enabled = false;

		Managers.Sound.Play("Sfx/QuestOk", Define.Sound.Effect);
		_isClear = true;

		this.enabled = false;
	}
}
