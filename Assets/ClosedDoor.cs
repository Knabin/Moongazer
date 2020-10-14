using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosedDoor : MonoBehaviour
{
	[SerializeField]
	GameObject _skeletons;

	[SerializeField]
	List<GameObject> _list;

	bool _isStart = false;
	bool _isEnd = false;

	private void Update()
	{
		if (_isEnd) return;

		if (!_isStart)
		{
			if(Managers.Game.GetPlayer().transform.position.z - transform.position.z > 1.5f)
			{
				_isStart = true;
				_skeletons.SetActive(true);
			}
		}
		else
		{
			if(_list.Count == 0)
			{
				//Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(4);
				_isEnd = true;
				enabled = false;
			}

			for(int i = 0; i < _list.Count; ++i)
			{
				if(_list[i] == null)
				{
					_list.Remove(_list[i]);
				}
			}
		}
	}

	//플레이어 z가 얘 z보다 1 이상 커질 경우 닫히고 스켈레톤 활성화
	// 스켈레톤 리스트가 0이 되면 아이템 획득하고 해당 컴포넌트 비활성화
}
