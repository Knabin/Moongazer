using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
	public Dictionary<int, Data.Inven> Inventory { get; private set; } = new Dictionary<int, Data.Inven>();
	public Action OnInvenChangedHandler = null;

	private void Awake()
	{
		Inventory = Managers.Data.LoadJson<Data.InvenData, int, Data.Inven>("InventoryData").MakeDict();
		SaveInven();
	}

	public void AddItem(int itemIndex)
	{
		if (Inventory.ContainsKey(itemIndex)) ++Inventory[itemIndex].amount;
		else
		{
			Data.Inven item2 = new Data.Inven();
			item2.index = itemIndex;
			item2.amount = 1;
			Inventory.Add(itemIndex, item2);
		}

		if (OnInvenChangedHandler != null)
			OnInvenChangedHandler.Invoke();
	}

	public void RemoveItem(int itemIndex)
	{
		--Inventory[itemIndex].amount;
		if(Inventory[itemIndex].amount <= 0)
		{
			Inventory.Remove(itemIndex);
		}

		if (OnInvenChangedHandler != null)
			OnInvenChangedHandler.Invoke();
	}

	public void SaveInven()
	{
		Data.InvenData data = new Data.InvenData();
		data.items = new List<Data.Inven>(Inventory.Values);
		Debug.Log(JsonUtility.ToJson(data));
		File.WriteAllText(Application.dataPath + "/Resources/Data/InventoryData.json", JsonUtility.ToJson(data, true));
	}
}
