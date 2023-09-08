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
using System.Collections.Generic;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class AdLoaderClient : AndroidJavaProxy, IAdLoaderClient
	{
		private AndroidJavaObject adLoader;

		private Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateCallbacks
		{
			get;
			set;
		}

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		public AdLoaderClient(AdLoader unityAdLoader)
			: base("com.google.unity.ads.UnityAdLoaderListener")
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			adLoader = new AndroidJavaObject("com.google.unity.ads.NativeAdLoader", @static, unityAdLoader.AdUnitId, this);
			CustomNativeTemplateCallbacks = unityAdLoader.CustomNativeTemplateClickHandlers;
			if (unityAdLoader.AdTypes.Contains(NativeAdType.CustomTemplate))
			{
				foreach (string templateId in unityAdLoader.TemplateIds)
				{
					adLoader.Call("configureCustomNativeTemplateAd", templateId, CustomNativeTemplateCallbacks.ContainsKey(templateId));
				}
			}
			adLoader.Call("create");
		}

		public void LoadAd(AdRequest request)
		{
			adLoader.Call("loadAd", Utils.GetAdRequestJavaObject(request));
		}

		public void onCustomTemplateAdLoaded(AndroidJavaObject ad)
		{
			if (this.OnCustomNativeTemplateAdLoaded != null)
			{
				CustomNativeEventArgs customNativeEventArgs = new CustomNativeEventArgs();
				customNativeEventArgs.nativeAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad));
				CustomNativeEventArgs e = customNativeEventArgs;
				this.OnCustomNativeTemplateAdLoaded(this, e);
			}
		}

		private void onAdFailedToLoad(string errorReason)
		{
			AdFailedToLoadEventArgs adFailedToLoadEventArgs = new AdFailedToLoadEventArgs();
			adFailedToLoadEventArgs.Message = errorReason;
			AdFailedToLoadEventArgs e = adFailedToLoadEventArgs;
			this.OnAdFailedToLoad(this, e);
		}

		public void onCustomClick(AndroidJavaObject ad, string assetName)
		{
			CustomNativeTemplateAd customNativeTemplateAd = new CustomNativeTemplateAd(new CustomNativeTemplateClient(ad));
			CustomNativeTemplateCallbacks[customNativeTemplateAd.GetCustomTemplateId()](customNativeTemplateAd, assetName);
		}
	}
}
