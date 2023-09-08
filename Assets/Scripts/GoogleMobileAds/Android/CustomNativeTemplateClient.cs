/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Common;
using System.Collections.Generic;
using UnityEngine;

namespace GoogleMobileAds.Android
{
	internal class CustomNativeTemplateClient : ICustomNativeTemplateClient
	{
		private AndroidJavaObject customNativeAd;

		public CustomNativeTemplateClient(AndroidJavaObject customNativeAd)
		{
			this.customNativeAd = customNativeAd;
		}

		public List<string> GetAvailableAssetNames()
		{
			return new List<string>(customNativeAd.Call<string[]>("getAvailableAssetNames", new object[0]));
		}

		public string GetTemplateId()
		{
			return customNativeAd.Call<string>("getTemplateId", new object[0]);
		}

		public byte[] GetImageByteArray(string key)
		{
			byte[] array = customNativeAd.Call<byte[]>("getImage", new object[1]
			{
				key
			});
			if (array.Length == 0)
			{
				return null;
			}
			return array;
		}

		public string GetText(string key)
		{
			string text = customNativeAd.Call<string>("getText", new object[1]
			{
				key
			});
			if (text.Equals(string.Empty))
			{
				return null;
			}
			return text;
		}

		public void PerformClick(string assetName)
		{
			customNativeAd.Call("performClick", assetName);
		}

		public void RecordImpression()
		{
			customNativeAd.Call("recordImpression");
		}
	}
}
