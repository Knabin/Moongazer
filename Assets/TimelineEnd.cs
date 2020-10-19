using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineEnd : MonoBehaviour
{
	[SerializeField]
	DragonController _dragon;

	private void OnEnable()
	{
		Managers.UI.ShowSceneUI<UI_HPBarBoss>();
		StartCoroutine("StartDragon");
	}

	IEnumerator StartDragon()
	{
		yield return new WaitForSeconds(3.0f);
		_dragon.State = Define.State.Defence;
		enabled = false;
	}
}
