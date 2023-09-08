/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class RewardedVideoGroup : MonoBehaviour
{
	public GameObject buttonGroup;

	public GameObject textGroup;

	public TimerText timerText;

	private const string ACTION_NAME = "rewarded_video";

	private void Start()
	{
		if (timerText != null)
		{
			TimerText obj = timerText;
			obj.onCountDownComplete = (Action)Delegate.Combine(obj.onCountDownComplete, new Action(OnCountDownComplete));
		}
		Timer.Schedule(this, 0.1f, AddEvents);
		if (!IsAvailableToShow())
		{
			buttonGroup.SetActive(value: false);
			if (IsAdAvailable() && !IsActionAvailable())
			{
				int time = (int)((double)GameConfig.instance.rewardedVideoPeriod - CUtils.GetActionDeltaTime("rewarded_video"));
				ShowTimerText(time);
			}
		}
		InvokeRepeating("IUpdate", 1f, 1f);
	}

	private void AddEvents()
	{
		if (AdmobController.instance.rewardBasedVideo != null)
		{
			AdmobController.instance.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		}
	}

	private void IUpdate()
	{
		buttonGroup.SetActive(IsAvailableToShow());
	}

	public void OnClick()
	{
		AdmobController.instance.ShowRewardBasedVideo();
		Sound.instance.PlayButton();
	}

	private void ShowTimerText(int time)
	{
		if (textGroup != null)
		{
			textGroup.SetActive(value: true);
			timerText.SetTime(time);
			timerText.Run();
		}
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		buttonGroup.SetActive(value: false);
		ShowTimerText(GameConfig.instance.rewardedVideoPeriod);
	}

	private void OnCountDownComplete()
	{
		textGroup.SetActive(value: false);
		if (IsAdAvailable())
		{
			buttonGroup.SetActive(value: true);
		}
	}

	public bool IsAvailableToShow()
	{
		return IsActionAvailable() && IsAdAvailable();
	}

	private bool IsActionAvailable()
	{
		return CUtils.IsActionAvailable("rewarded_video", GameConfig.instance.rewardedVideoPeriod);
	}

	private bool IsAdAvailable()
	{
		if (AdmobController.instance.rewardBasedVideo == null)
		{
			return false;
		}
		return AdmobController.instance.rewardBasedVideo.IsLoaded();
	}

	private void OnDestroy()
	{
		if (AdmobController.instance.rewardBasedVideo != null)
		{
			AdmobController.instance.rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
		}
	}

	private void OnApplicationPause(bool pause)
	{
		if (!pause && textGroup != null && textGroup.activeSelf)
		{
			int time = (int)((double)GameConfig.instance.rewardedVideoPeriod - CUtils.GetActionDeltaTime("rewarded_video"));
			ShowTimerText(time);
		}
	}
}
