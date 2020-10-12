using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DungeonScene : BaseScene
{
	public override void Clear()
	{
		Managers.Game.SaveStat();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.DungeonScene;

		// 초기화 작업
		GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "Player");
		Camera.main.gameObject.GetOrAddComponent<CameraController>().SetTarget(player);
		player.transform.position = new Vector3(-30, -3, 0);
		player.transform.Rotate(Quaternion.Euler(0f, 90f, 0f).eulerAngles);

		Managers.Game.LoadStat();

		Managers.Sound.Play("Bgm/Dungeon", Define.Sound.Bgm);

		Managers.UI.ShowSceneUI<UI_Attack>();
		Managers.UI.ShowSceneUI<UI_Status>();
		Managers.UI.ShowSceneUI<UI_Joystick>();
		Managers.UI.ShowSceneUI<UI_Menu>();
	}
}
