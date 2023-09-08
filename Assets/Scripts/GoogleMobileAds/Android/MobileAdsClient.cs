/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Common;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	public class MobileAdsClient : IMobileAdsClient
	{
		private static MobileAdsClient instance = new MobileAdsClient();

		public static MobileAdsClient Instance => instance;

		private MobileAdsClient()
		{
		}

		public void Initialize(string appId)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
			AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass2.CallStatic("initialize", @static, appId);
		}

		public void SetApplicationVolume(float volume)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppVolume", volume);
		}

		public void SetApplicationMuted(bool muted)
		{
			AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.google.android.gms.ads.MobileAds");
			androidJavaClass.CallStatic("setAppMuted", muted);
		}

		public void SetiOSAppPauseOnBackground(bool pause)
		{
		}
	}
}
