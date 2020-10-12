using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
	private void LateUpdate()
	{
		if(Vector3.Distance(Managers.Game.GetPlayer().transform.position, transform.position) < 5.0f)
		{
			Managers.Scene.LoadScene(Define.Scene.DungeonScene);
		}
	}
}
