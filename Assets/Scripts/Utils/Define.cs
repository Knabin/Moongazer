using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	public enum WorldObject
	{
		Unknown,
		Player,
		Enemy
	}

	public enum State
	{
		Idle,
		Moving,
		Run,
		Attack,
		Skill,
		Defence,
		Die,
	}

	public enum Layer
	{
		Enemy = 8,
		Block = 9,
		Npc = 10,
		Obj = 11,
	}

	public enum Scene
	{
		Unknown,
		TitleScene,
		LoadingScene,
		FieldScene,
		DungeonScene,
		TestScene,
	}

	public enum Sound
	{
		Bgm,
		Effect,
		Enemy,
		MaxCount,
	}

	public enum UIEvent
	{
		Click,
		Drag,
		PointerDown,
		PointerUp,
	}

	public enum MouseEvent
	{
		Press,
		PointerDown,
		PointerUp,
		Click,
	}

	public enum CameraMode
	{
		QuarterView,
		QuarterViewManual,
		EventView,
	}

	public enum ItemType
	{
		Field,
		Potion,
	}

	public enum ClickableType
	{
		None,
		Table,
		PasswordDoor,
	}
}
