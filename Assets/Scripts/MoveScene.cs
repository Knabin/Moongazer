using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
	bool isMoved = false;

	private void LateUpdate()
	{
		if (isMoved) return;

		if (Vector3.Distance(Managers.Game.GetPlayer().transform.position, transform.position) < 4.0f)
		{
			isMoved = true;
			Managers.Scene.LoadScene(Define.Scene.DungeonScene);
		}
	}
}
