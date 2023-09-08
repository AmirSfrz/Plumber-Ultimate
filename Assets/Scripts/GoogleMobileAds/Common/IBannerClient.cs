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

namespace GoogleMobileAds.Common
{
	public interface IBannerClient
	{
		event EventHandler<EventArgs> OnAdLoaded;

		event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		event EventHandler<EventArgs> OnAdOpening;

		event EventHandler<EventArgs> OnAdClosed;

		event EventHandler<EventArgs> OnAdLeavingApplication;

		void CreateBannerView(string adUnitId, AdSize adSize, AdPosition position);

		void CreateBannerView(string adUnitId, AdSize adSize, int x, int y);

		void LoadAd(AdRequest request);

		void ShowBannerView();

		void HideBannerView();

		void DestroyBannerView();

		float GetHeightInPixels();

		float GetWidthInPixels();

		void SetPosition(AdPosition adPosition);

		void SetPosition(int x, int y);

		string MediationAdapterClassName();
	}
}
