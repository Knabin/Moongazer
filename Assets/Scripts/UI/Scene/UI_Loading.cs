using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Loading : UI_Scene
{
	AsyncOperation async;
	string _nextSceneName;

	enum Images
	{
		Fill,
	}

	public override void Init()
	{
		base.Init();

		Bind<Image>(typeof(Images));
		StartCoroutine("Loading");
	}

	IEnumerator Loading()
	{
		async = SceneManager.LoadSceneAsync(Managers.Scene.nextScene);
		async.allowSceneActivation = false;

		float time = 0.0f;
		while(!async.isDone)
		{
			yield return null;

			time += Time.deltaTime;

			if(async.progress >= 0.9f)
			{
				Get<Image>((int)Images.Fill).fillAmount = Mathf.Lerp(Get<Image>((int)Images.Fill).fillAmount, 1.0f, time);

				if (Get<Image>((int)Images.Fill).fillAmount == 1.0f)
					async.allowSceneActivation = true;
			}
			else
			{
				Get<Image>((int)Images.Fill).fillAmount = Mathf.Lerp(Get<Image>((int)Images.Fill).fillAmount, async.progress, time);
				if (Get<Image>((int)Images.Fill).fillAmount >= async.progress)
					time = 0f;
			}
		}
	}
}
