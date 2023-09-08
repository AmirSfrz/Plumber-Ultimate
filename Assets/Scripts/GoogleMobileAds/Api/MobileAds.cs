/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Common;
using System;
using System.Reflection;

namespace GoogleMobileAds.Api
{
	public class MobileAds
	{
		private static readonly IMobileAdsClient client = GetMobileAdsClient();

		public static void Initialize(string appId)
		{
			client.Initialize(appId);
		}

		public static void SetApplicationMuted(bool muted)
		{
			client.SetApplicationMuted(muted);
		}

		public static void SetApplicationVolume(float volume)
		{
			client.SetApplicationVolume(volume);
		}

		public static void SetiOSAppPauseOnBackground(bool pause)
		{
			client.SetiOSAppPauseOnBackground(pause);
		}

		private static IMobileAdsClient GetMobileAdsClient()
		{
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("MobileAdsInstance", BindingFlags.Static | BindingFlags.Public);
			return (IMobileAdsClient)method.Invoke(null, null);
		}
	}
}
