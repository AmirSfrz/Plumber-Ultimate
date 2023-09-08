/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using UnityEngine;

public class Music : MonoBehaviour
{
	public enum Type
	{
		None,
		MainMusic
	}

	public AudioSource audioSource;

	public static Music instance;

	[HideInInspector]
	public AudioClip[] musicClips;

	private Type currentType;

	private void Awake()
	{
		instance = this;
	}

	public bool IsMuted()
	{
		return !IsEnabled();
	}

	public bool IsEnabled()
	{
		return CUtils.GetBool("music_enabled", defaultValue: true);
	}

	public void SetEnabled(bool enabled, bool updateMusic = false)
	{
		CUtils.SetBool("music_enabled", enabled);
		if (updateMusic)
		{
			UpdateSetting();
		}
	}

	public void Play(Type type)
	{
		if (type != 0 && (currentType != type || !audioSource.isPlaying))
		{
			StartCoroutine(PlayNewMusic(type));
		}
	}

	public void Play()
	{
		Play(currentType);
	}

	public void Stop()
	{
		audioSource.Stop();
	}

	private IEnumerator PlayNewMusic(Type type)
	{
		while (audioSource.volume >= 0.1f)
		{
			audioSource.volume -= 0.2f;
			yield return new WaitForSeconds(0.1f);
		}
		audioSource.Stop();
		currentType = type;
		audioSource.clip = musicClips[(int)type];
		if (IsEnabled())
		{
			audioSource.Play();
		}
		audioSource.volume = 1f;
	}

	private void UpdateSetting()
	{
		if (!(audioSource == null))
		{
			if (IsEnabled())
			{
				Play();
			}
			else
			{
				audioSource.Stop();
			}
		}
	}
}
