using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CirclePuzzleSub : MonoBehaviour
{
	public bool _isDown = false;

	public bool IsDown
	{
		get { return _isDown; }
		set
		{
			_particle.SetActive(value);
			_isDown = value;
		}
	}

	[SerializeField]
	GameObject _particle;
	[SerializeField]
	int _index;

	private void OnTriggerEnter(Collider other)
	{
		Managers.Sound.Play("Sfx/Button");
		IsDown = !IsDown;
		transform.parent.GetComponent<CirclePuzzle>().SetIsDown(_index);
	}
}
