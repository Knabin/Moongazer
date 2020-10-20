using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScene : BaseScene
{
	public override void Clear()
	{
		foreach (GameObject go in Managers.Game._monsters)
		{
			if (go == null || !go.activeSelf)
			{
				Managers.Game.Destroy(go);
				break;
			}
		}
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.FieldScene;

		// 초기화 작업
		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetTarget(player);
		player.transform.position = new Vector3(-19f, 0.12f, -19f);

		Managers.UI.ShowSceneUI<UI_Attack>();
		Managers.UI.ShowSceneUI<UI_Status>();
		Managers.UI.ShowSceneUI<UI_Joystick>();
		Managers.UI.ShowSceneUI<UI_Menu>();

		Managers.Sound.Play("Bgm/Field", Define.Sound.Bgm);

		GameObject go = new GameObject { name = "SpawningPool" };
		go.transform.position = new Vector3(10f, 0.1f, -9f);
		SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
		pool.SetEnemyType("Slime");
		pool.SetKeepMonsterCount(5);

		Managers.Quest.questId = 10;
	}
}
