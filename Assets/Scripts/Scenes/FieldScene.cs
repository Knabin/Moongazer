using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScene : BaseScene
{
	public override void Clear()
	{
		Managers.Game.SaveStat();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.FieldScene;

		// 초기화 작업
		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetTarget(player);
		player.transform.position = new Vector3(-29f, 0.3f, 17);

		Managers.UI.ShowSceneUI<UI_Attack>();
		Managers.UI.ShowSceneUI<UI_Status>();
		Managers.UI.ShowSceneUI<UI_Joystick>();
		Managers.UI.ShowSceneUI<UI_Menu>();

		Managers.Sound.Play("Bgm/Field", Define.Sound.Bgm);

		GameObject go = new GameObject { name = "SpawningPool" };
		SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
		pool.transform.position = new Vector3(11f, 0f, 9f);
		pool.SetEnemyType("Slime");
		pool.SetKeepMonsterCount(6);

		Managers.Quest.questId = 10;
	}
}
