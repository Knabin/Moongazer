using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager
{
	AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
	AudioMixer _audioMixer;
	Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();
	// MP3 Player	=> AudioSource
	// MP3 음원		=> AudioClip
	// 관객(귀)		=> AudioListener

	public void Init()
	{
		GameObject root = GameObject.Find("@Sound");
		_audioMixer = Resources.Load("MyMixer") as AudioMixer;
		if (root == null)
		{
			root = new GameObject { name = "@Sound" };
			Object.DontDestroyOnLoad(root);

			string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));

			// -1 처리 -> Max Count 제외
			for (int i = 0; i < soundNames.Length - 1; ++i)
			{
				GameObject go = new GameObject { name = soundNames[i] };
				_audioSources[i] = go.AddComponent<AudioSource>();
				go.transform.parent = root.transform;
			}
			_audioSources[(int)Define.Sound.Bgm].outputAudioMixerGroup = _audioMixer.FindMatchingGroups("BGM")[0];
			_audioSources[(int)Define.Sound.Effect].outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];
			_audioSources[(int)Define.Sound.Enemy].outputAudioMixerGroup = _audioMixer.FindMatchingGroups("SFX")[0];

			_audioSources[(int)Define.Sound.Bgm].loop = true;
		}

	}

	public void Clear()
	{
		foreach (AudioSource audioSource in _audioSources)
		{
			audioSource.clip = null;
			audioSource.Stop();
		}
		_audioClips.Clear();
	}

	public void Play(string path, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
		AudioClip audioClip = GetOrAddAudioClip(path, type);
		Play(audioClip, type, pitch);
	}

	public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect, float pitch = 1.0f)
	{
		if (audioClip == null)
			return;

		if (type == Define.Sound.Bgm)
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];

			if (audioSource.isPlaying)
				audioSource.Stop();

			audioSource.pitch = pitch;
			audioSource.clip = audioClip;
			audioSource.Play();
		}
		else if (type == Define.Sound.Effect)
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
		else 
		{
			AudioSource audioSource = _audioSources[(int)Define.Sound.Enemy];
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(audioClip);
		}
	}

	public AudioClip GetOrAddAudioClip(string path, Define.Sound type = Define.Sound.Effect)
	{
		if (path.Contains("Sounds/") == false)
			path = $"Sounds/{path}";

		AudioClip audioClip = null;

		if (type == Define.Sound.Bgm)
		{
			audioClip = Managers.Resource.Load<AudioClip>(path);
		}
		else
		{
			if (_audioClips.TryGetValue(path, out audioClip) == false)
			{
				audioClip = Managers.Resource.Load<AudioClip>(path);
				_audioClips.Add(path, audioClip);
			}
		}

		if (audioClip == null)
			Debug.Log($"AudioClip Missing ! {path}");

		return audioClip;
	}
}
