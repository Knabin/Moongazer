using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
	[SerializeField]
	public int _itemNumber = 0;
	[SerializeField]
	public bool _left = true;

	PlayerInven _inven;
	public bool _isOpen = false;

	void Start()
	{
		_inven = Managers.Game.GetPlayer().GetComponent<PlayerInven>();
	}

    void Update()
    {
		if (_isOpen || _itemNumber <= 0 || _inven == null || _inven.Inventory.Count == 0) return;
		if (Vector3.Distance(Managers.Game.GetPlayer().transform.position, transform.position) > 10.0f) return;

		Data.Item item = Managers.Data.ItemDict[_itemNumber];
		if(_inven.Inventory.ContainsKey(item))
		{
			_inven.RemoveItem(_itemNumber);
			StartCoroutine("StartOpen");
			_isOpen = true;
		}
		
    }

	IEnumerator StartOpen()
	{
		//-150
		float t = 0.0f;
		float duration = 2.0f;
		Vector3 dir = _left ? Vector3.down : Vector3.up;
		Quaternion startRot = transform.rotation;
		Managers.Sound.Play("Sfx/door", Define.Sound.Effect);

		while (t < duration)
		{
			t += Time.deltaTime;
			transform.rotation = startRot * Quaternion.AngleAxis(t / duration * 180f, dir);
			yield return null;
		}
		enabled = false;
	}
}
