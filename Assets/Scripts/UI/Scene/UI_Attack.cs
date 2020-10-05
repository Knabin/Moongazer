using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Attack : UI_Scene
{
	enum Buttons
	{
		Skill1,
		Skill2,
		Skill3,
		Skill4,

	}

	public override void Init()
	{
		base.Init();

		Bind<Button>(typeof(Buttons));
	}
}
