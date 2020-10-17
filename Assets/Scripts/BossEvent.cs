using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEvent : MonoBehaviour
{
	[SerializeField]
	GameObject timeline;
	[SerializeField]
	GameObject door;

	private void OnTriggerExit(Collider other)
	{
		timeline.SetActive(true);
		door.SetActive(true);
		Managers.Game.GetPlayer().GetComponent<PlayerController>().transform.position += new Vector3(0, 0, 2);
		Managers.Game.GetPlayer().GetComponent<PlayerController>().value.joyTouch = Vector2.zero;
		Managers.Game.GetPlayer().GetComponent<PlayerController>().State = Define.State.Idle;
		Managers.Game.GetPlayer().GetComponent<PlayerController>().isInBoss = true;
		this.enabled = false;
	}
}
