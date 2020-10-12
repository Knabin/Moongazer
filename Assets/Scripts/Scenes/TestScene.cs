using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScene : BaseScene
{
	public override void Clear()
	{
		//foreach (GameObject go in Managers.Game._monsters)
		//{
		//	go.SetActive(false);
		//}
		////Managers.Game._monsters.Clear();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.TestScene;
		
		// 초기화 작업
		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetTarget(player);

		Managers.UI.ShowSceneUI<UI_Attack>();
		Managers.UI.ShowSceneUI<UI_Status>();
		Managers.UI.ShowSceneUI<UI_Joystick>();
		Managers.UI.ShowSceneUI<UI_Menu>();


		//GameObject go = new GameObject { name = "SpawningPool" };
		//SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
		//pool.SetEnemyType("Slime");
		//pool.SetKeepMonsterCount(2);

		//GameObject go2 = new GameObject { name = "SpawningPool" };
		//SpawningPool pool2 = go2.GetOrAddComponent<SpawningPool>();
		//pool2.SetEnemyType("Skeleton");
		//pool2.SetKeepMonsterCount(2);

		Managers.Game.Spawn(Define.WorldObject.Enemy, "Dragon");
	}
}
