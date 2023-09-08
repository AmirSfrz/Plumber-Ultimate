/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Events;

public class RewardedVideoButton : MonoBehaviour
{
	private const string ACTION_NAME = "rewarded_video";

	public UnityEvent onRewarded;

	private void OnEnable()
	{
		Timer.Schedule(this, 0.1f, AddEvents);
	}

	private void AddEvents()
	{
		if (AdmobController.instance.rewardBasedVideo != null)
		{
			AdmobController.instance.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		}
	}

	public void OnClick()
	{
		if (IsAdAvailable())
		{
			AdmobController.instance.ShowRewardBasedVideo();
		}
		else
		{
			Toast.instance.ShowMessage("Ad is not available now, please wait..");
		}
		Sound.instance.PlayButton();
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		onRewarded.Invoke();
	}

	public bool IsAdAvailable()
	{
		if (AdmobController.instance.rewardBasedVideo == null)
		{
			return false;
		}
		return AdmobController.instance.rewardBasedVideo.IsLoaded();
	}

	private void OnDisable()
	{
		if (AdmobController.instance.rewardBasedVideo != null)
		{
			AdmobController.instance.rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
		}
	}
}
