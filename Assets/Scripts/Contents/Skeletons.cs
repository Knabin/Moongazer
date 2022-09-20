using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeletons : MonoBehaviour
{
	private void OnEnable()
	{
		foreach (EnemyController go in gameObject.GetComponentsInChildren<EnemyController>())
		{
			Managers.Game._monsters.Add(go.gameObject);
		}
	}

	private void Update()
	{
		if(Managers.Game._monsters.Count == 0)
		{
			Managers.Game.GetPlayer().GetComponent<PlayerInven>().AddItem(13);
			enabled = false;
		}
	}
}
