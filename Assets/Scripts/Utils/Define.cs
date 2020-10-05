using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
	public enum WorldObject
	{
		Unknown,
		Player,
		Monster
	}

	public enum State
	{
		Idle,
		Moving,
		Run,
		Jump,
		Attack,
		Skill,
		Defence,
		Die,
	}

	public enum Layer
	{
		Monster = 8,
		Ground = 9,
		Block = 10,
	}

	public enum Scene
	{
		Unknown,
		Login,
		Lobby,
		Game,
		Test,
	}

	public enum Sound
	{
		Bgm,
		Effect,
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
}
