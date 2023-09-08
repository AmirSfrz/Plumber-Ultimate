/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
	public static ScreenFader instance;

	public const float DURATION = 0.37f;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		GTil.Init(this);
	}

	public void FadeOut(Action onComplete)
	{
		GetComponent<Animator>().SetTrigger("fade_out");
		GetComponent<Image>().enabled = true;
		Timer.Schedule(this, 0.37f, delegate
		{
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}

	public void FadeIn(Action onComplete)
	{
		GetComponent<Animator>().SetTrigger("fade_in");
		Timer.Schedule(this, 0.37f, delegate
		{
			GetComponent<Image>().enabled = false;
			if (onComplete != null)
			{
				onComplete();
			}
		});
	}

	public void GotoScene(int sceneIndex)
	{
		FadeOut(delegate
		{
			CUtils.LoadScene(sceneIndex);
		});
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ScreenFader_Out"))
		{
			FadeIn(null);
		}
	}
}
