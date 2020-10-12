using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	public Item _item;
	public int _itemCount;
	public Image _itemImage;

	[SerializeField]
	private Text _textCount;
	[SerializeField]
	private GameObject _countImage;

	private void SetColor(float alpha)
	{
		Color color = _itemImage.color;
		color.a = alpha;
		_itemImage.color = color;
	}

	public void AddItem(Item item, int count = 1)
	{
		_item = item;
		_itemCount = count;
		_itemImage.sprite = item.itemImage;

		if (_item.itemType != Item.ItemType.Equipment)
		{
			_countImage.SetActive(true);
			_textCount.text = _itemCount.ToString();
		}
		else
		{
			_textCount.text = "0";
			_countImage.SetActive(false);
		}

		SetColor(1f);
	}

	public void SetSlotCount(int count)
	{
		_itemCount += count;
		_textCount.text = _itemCount.ToString();

		if (_itemCount <= 0)
			ClearSlot();
	}

	private void ClearSlot()
	{
		_item = null;
		_itemCount = 0;
		_itemImage.sprite = null;
		SetColor(0f);

		_countImage.SetActive(false);
		_textCount.text = "0";
	}
}
