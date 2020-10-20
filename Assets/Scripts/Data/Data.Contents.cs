using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Data
{
	#region Stat

	[Serializable]
	public class Stat
	{
		public int level;
		public int maxHp;
		public int attack;
		public int totalExp;
	}

	[Serializable]
	public class StatData : ILoader<int, Stat>
	{
		public List<Stat> stats = new List<Stat>();

		public Dictionary<int, Stat> MakeDict()
		{
			Dictionary<int, Stat> dict = new Dictionary<int, Stat>();

			foreach (Stat stat in stats)
				dict.Add(stat.level, stat);

			return dict;
		}
	}

	[Serializable]
	public class PlayerStat
	{
		public int level;
		public int hp;
		public int exp;
		public int gold;
	}
	#endregion

	#region Item
	[Serializable]
	public class Item
	{
		public int number;
		public string image;
		public string name;
		public int type;
		public string info;
		public int price;
	}

	[Serializable]
	public class ItemData : ILoader<int, Item>
	{
		public List<Item> items = new List<Item>();

		public Dictionary<int, Item> MakeDict()
		{
			Dictionary<int, Item> dict = new Dictionary<int, Item>();

			foreach (Item item in items)
				dict.Add(item.number, item);

			return dict;
		}
	}

	[Serializable]
	public class Inven
	{
		public int index;
		public int amount;
	}

	[Serializable]
	public class InvenData : ILoader<int, Inven>
	{
		public List<Inven> items = new List<Inven>();

		public Dictionary<int, Inven> MakeDict()
		{
			Dictionary<int, Inven> dict = new Dictionary<int, Inven>();

			foreach (Inven item in items)
				dict.Add(item.index, item);

			return dict;
		}
		
	}
	#endregion
}