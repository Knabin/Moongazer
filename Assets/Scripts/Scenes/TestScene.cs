using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
	public override void Clear()
	{
		throw new System.NotImplementedException();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.Test;
		Managers.UI.ShowSceneUI<UI_Joystick>();
		Managers.UI.ShowSceneUI<UI_Menu>();

		// 초기화 작업
		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetTarget(player);

		GameObject go = new GameObject { name = "SpawningPool" };
		SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
	}
}
