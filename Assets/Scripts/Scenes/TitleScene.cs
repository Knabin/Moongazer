using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScene : BaseScene
{
	public override void Clear()
	{
		//throw new System.NotImplementedException();
	}

	protected override void Init()
	{
		base.Init();

		SceneType = Define.Scene.TitleScene;

		Managers.UI.ShowSceneUI<UI_Title>();
		Managers.Sound.Play("Bgm/Title", Define.Sound.Bgm);
	}
}
