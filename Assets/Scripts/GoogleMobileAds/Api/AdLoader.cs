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
using System.Collections.Generic;
using System.Reflection;

namespace GoogleMobileAds.Api
{
	public class AdLoader
	{
		public class Builder
		{
			internal string AdUnitId
			{
				get;
				private set;
			}

			internal HashSet<NativeAdType> AdTypes
			{
				get;
				private set;
			}

			internal HashSet<string> TemplateIds
			{
				get;
				private set;
			}

			internal Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers
			{
				get;
				private set;
			}

			public Builder(string adUnitId)
			{
				AdUnitId = adUnitId;
				AdTypes = new HashSet<NativeAdType>();
				TemplateIds = new HashSet<string>();
				CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>();
			}

			public Builder ForCustomNativeAd(string templateId)
			{
				TemplateIds.Add(templateId);
				AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			public Builder ForCustomNativeAd(string templateId, Action<CustomNativeTemplateAd, string> callback)
			{
				TemplateIds.Add(templateId);
				CustomNativeTemplateClickHandlers[templateId] = callback;
				AdTypes.Add(NativeAdType.CustomTemplate);
				return this;
			}

			public AdLoader Build()
			{
				return new AdLoader(this);
			}
		}

		private IAdLoaderClient adLoaderClient;

		public Dictionary<string, Action<CustomNativeTemplateAd, string>> CustomNativeTemplateClickHandlers
		{
			get;
			private set;
		}

		public string AdUnitId
		{
			get;
			private set;
		}

		public HashSet<NativeAdType> AdTypes
		{
			get;
			private set;
		}

		public HashSet<string> TemplateIds
		{
			get;
			private set;
		}

		public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad;

		public event EventHandler<CustomNativeEventArgs> OnCustomNativeTemplateAdLoaded;

		private AdLoader(Builder builder)
		{
			AdUnitId = string.Copy(builder.AdUnitId);
			CustomNativeTemplateClickHandlers = new Dictionary<string, Action<CustomNativeTemplateAd, string>>(builder.CustomNativeTemplateClickHandlers);
			TemplateIds = new HashSet<string>(builder.TemplateIds);
			AdTypes = new HashSet<NativeAdType>(builder.AdTypes);
			Type type = Type.GetType("GoogleMobileAds.GoogleMobileAdsClientFactory,Assembly-CSharp");
			MethodInfo method = type.GetMethod("BuildAdLoaderClient", BindingFlags.Static | BindingFlags.Public);
			adLoaderClient = (IAdLoaderClient)method.Invoke(null, new object[1]
			{
				this
			});
			adLoaderClient.OnCustomNativeTemplateAdLoaded += delegate(object sender, CustomNativeEventArgs args)
			{
				this.OnCustomNativeTemplateAdLoaded(this, args);
			};
			adLoaderClient.OnAdFailedToLoad += delegate(object sender, AdFailedToLoadEventArgs args)
			{
				if (this.OnAdFailedToLoad != null)
				{
					this.OnAdFailedToLoad(this, args);
				}
			};
		}

		public void LoadAd(AdRequest request)
		{
			adLoaderClient.LoadAd(request);
		}
	}
}
