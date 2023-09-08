/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Android;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds
{
	public class GoogleMobileAdsClientFactory
	{
		public static IBannerClient BuildBannerClient()
		{
			return new BannerClient();
		}

		public static IInterstitialClient BuildInterstitialClient()
		{
			return new InterstitialClient();
		}

		public static IRewardBasedVideoAdClient BuildRewardBasedVideoAdClient()
		{
			return new RewardBasedVideoAdClient();
		}

		public static IAdLoaderClient BuildAdLoaderClient(AdLoader adLoader)
		{
			return new AdLoaderClient(adLoader);
		}

		public static IMobileAdsClient MobileAdsInstance()
		{
			return MobileAdsClient.Instance;
		}
	}
}
