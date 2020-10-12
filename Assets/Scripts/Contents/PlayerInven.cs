using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
	public Dictionary<Data.Item, int> Inventory { get; private set; } = new Dictionary<Data.Item, int>();

	private void Start()
	{
	}

	public void AddItem(int itemIndex)
	{
		Dictionary<int, Data.Item> dict = Managers.Data.ItemDict;
		Data.Item item = dict[itemIndex];

		if (Inventory.ContainsKey(item)) ++Inventory[item];
		else Inventory.Add(item, 1);
	}

	public void RemoveItem(int itemIndex)
	{
		Dictionary<int, Data.Item> dict = Managers.Data.ItemDict;
		Data.Item item = dict[itemIndex];

		--Inventory[item];
		if(Inventory[item] <= 0)
		{
			Inventory.Remove(item);
		}
	}
}
