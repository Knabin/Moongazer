using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
	public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }
	public string nextScene;

	public void LoadScene(Define.Scene type)
	{
		// 현재 씬 정리 -> 로딩씬 -> 이동 씬
		// 현재 씬 정리
		if(Managers.Game.GetPlayer() != null) Managers.Game.SaveData();
		Managers.Clear();

		string sceneName = GetSceneName(type);

		// 로딩 씬
		nextScene = sceneName;
		SceneManager.LoadScene("LoadingScene");
	}

	string GetSceneName(Define.Scene type)
	{
		string name = System.Enum.GetName(typeof(Define.Scene), type);
		return name;
	}

	public void Clear()
	{
		CurrentScene.Clear();
	}
}
