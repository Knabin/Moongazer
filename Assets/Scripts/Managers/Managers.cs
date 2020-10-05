using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
	static Managers s_instance;   // 유일성이 보장된다
	static Managers Instance { get { Init(); return s_instance; } }  // 유일한 매니저를 갖고 온다

	#region Contents
	GameManager _game = new GameManager();

	public static GameManager Game
	{
		get { return Instance._game; }
	}


	#endregion

	#region Core
	DataManager _data = new DataManager();
	InputManager _input = new InputManager();
	PoolManager _pool = new PoolManager();
	ResourceManager _resource = new ResourceManager();
	SceneManagerEx _scene = new SceneManagerEx();
	SoundManager _sound = new SoundManager();
	UIManager _ui = new UIManager();

	public static DataManager Data { get { return Instance._data; } }
	public static InputManager Input { get { return Instance._input; } }
	public static PoolManager Pool { get { return Instance._pool; } }
	public static ResourceManager Resource { get { return Instance._resource; } }
	public static SceneManagerEx Scene { get { return Instance._scene; } }
	public static SoundManager Sound { get { return Instance._sound; } }
	public static UIManager UI { get { return Instance._ui; } }
	#endregion

	void Start()
	{
		Init();
	}

	void Update()
	{
		_input.OnUpdate();
	}

	static void Init()
	{
		if (s_instance == null)
		{
			// 각각의 Managers가 선언되었다고 해도 이렇게 찾아서 사용한다면 원본(@Managers)의 Managers가 instance에 저장될 것이다.
			GameObject go = GameObject.Find("@Managers");

			// Scene에 GameObject인 @Managers가 없다면 새로 추가한다.
			if (go == null)
			{
				go = new GameObject { name = "@Managers" };
				go.AddComponent<Managers>();
			}
			// 새로운 Scene이 로드될 때 삭제되지 않게끔 한다.
			DontDestroyOnLoad(go);
			s_instance = go.GetComponent<Managers>();

			s_instance._data.Init();
			s_instance._pool.Init();
			s_instance._sound.Init();
		}
	}

	public static void Clear()
	{
		Input.Clear();
		Scene.Clear();
		Sound.Clear();
		UI.Clear();

		Pool.Clear();
	}
}
