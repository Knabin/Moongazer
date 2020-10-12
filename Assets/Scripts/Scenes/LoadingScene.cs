using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : BaseScene
{
	public override void Clear()
	{
		//throw new System.NotImplementedException();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.LoadingScene;
		Managers.UI.ShowSceneUI<UI_Loading>();
	}
}
