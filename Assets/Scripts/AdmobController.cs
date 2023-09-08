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

public class AdmobController : MonoBehaviour
{
	private BannerView bannerView;

	public InterstitialAd interstitial;

	public RewardBasedVideoAd rewardBasedVideo;

	public static AdmobController instance;

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		if (!CUtils.IsBuyItem() && !CUtils.IsAdsRemoved())
		{
			RequestBanner();
			Timer.Schedule(this, 5f, RequestBanner);
			RequestInterstitial();
		}
		InitRewardedVideo();
		RequestRewardBasedVideo();
	}

	private void InitRewardedVideo()
	{
		rewardBasedVideo = RewardBasedVideoAd.Instance;
		rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
		rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
		rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
		rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
		rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
		rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
		rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
	}

	public void RequestBanner()
	{
		string adUnitId = GameConfig.instance.admob.androidBanner.Trim();
		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
		bannerView.OnAdLoaded += HandleAdLoaded;
		bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;
		bannerView.OnAdOpening += HandleAdOpened;
		bannerView.OnAdClosed += HandleAdClosed;
		bannerView.OnAdLeavingApplication += HandleAdLeftApplication;
		bannerView.LoadAd(CreateAdRequest());
	}

	public void RequestInterstitial()
	{
		string adUnitId = GameConfig.instance.admob.androidInterstitial.Trim();
		interstitial = new InterstitialAd(adUnitId);
		interstitial.OnAdLoaded += HandleInterstitialLoaded;
		interstitial.OnAdFailedToLoad += HandleInterstitialFailedToLoad;
		interstitial.OnAdOpening += HandleInterstitialOpened;
		interstitial.OnAdClosed += HandleInterstitialClosed;
		interstitial.OnAdLeavingApplication += HandleInterstitialLeftApplication;
		interstitial.LoadAd(CreateAdRequest());
	}

	public void RequestRewardBasedVideo()
	{
		string adUnitId = GameConfig.instance.admob.androidRewarded.Trim();
		rewardBasedVideo.LoadAd(CreateAdRequest(), adUnitId);
	}

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder().AddTestDevice("SIMULATOR").AddTestDevice("0123456789ABCDEF0123456789ABCDEF").AddKeyword("game")
			.TagForChildDirectedTreatment(tagForChildDirectedTreatment: false)
			.AddExtra("color_bg", "9B30FF")
			.Build();
	}

	public void ShowInterstitial(InterstitialAd ad)
	{
		if (ad != null && ad.IsLoaded())
		{
			ad.Show();
		}
	}

	public void ShowBanner()
	{
		if (!CUtils.IsBuyItem() && bannerView != null)
		{
			bannerView.Show();
		}
	}

	public void HideBanner()
	{
		if (bannerView != null)
		{
			bannerView.Hide();
		}
	}

	public bool ShowInterstitial()
	{
		if (interstitial != null && interstitial.IsLoaded())
		{
			interstitial.Show();
			return true;
		}
		return false;
	}

	public void ShowRewardBasedVideo()
	{
		if (rewardBasedVideo.IsLoaded())
		{
			rewardBasedVideo.Show();
		}
		else
		{
			MonoBehaviour.print("Reward based video ad is not ready yet");
		}
	}

	public void HandleAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received.");
	}

	public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	public void HandleInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLoaded event received.");
	}

	public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialFailedToLoad event received with message: " + args.Message);
	}

	public void HandleInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialOpened event received");
	}

	public void HandleInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialClosed event received");
		RequestInterstitial();
	}

	public void HandleInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleInterstitialLeftApplication event received");
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		RequestRewardBasedVideo();
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}
}
