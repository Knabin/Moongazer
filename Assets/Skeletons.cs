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
}
