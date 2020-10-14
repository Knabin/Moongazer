using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerInven : MonoBehaviour
{
	public Dictionary<int, Data.Inven> Inventory { get; private set; } = new Dictionary<int, Data.Inven>();

	private void Start()
	{
		Inventory = Managers.Data.LoadJson<Data.InvenData, int, Data.Inven>("InventoryData").MakeDict();
		AddItem(1);
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
		//if (Inventory.ContainsKey(item)) ++Inventory[item];
		//else Inventory.Add(item, 1);
	}

	public void RemoveItem(int itemIndex)
	{
		--Inventory[itemIndex].amount;
		if(Inventory[itemIndex].amount <= 0)
		{
			Inventory.Remove(itemIndex);
		}
	}

	public void SaveInven()
	{
		Data.InvenData data = new Data.InvenData();
		data.items = new List<Data.Inven>(Inventory.Values);
		Debug.Log(JsonUtility.ToJson(data));
		File.WriteAllText(Application.dataPath + "/Resources/Data/InventoryData.json", JsonUtility.ToJson(data, true));
	}
}
