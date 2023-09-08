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

namespace GoogleMobileAds.Api
{
	public class CustomNativeTemplateAd
	{
		private ICustomNativeTemplateClient client;

		internal CustomNativeTemplateAd(ICustomNativeTemplateClient client)
		{
			this.client = client;
		}

		public List<string> GetAvailableAssetNames()
		{
			return client.GetAvailableAssetNames();
		}

		public string GetCustomTemplateId()
		{
			return client.GetTemplateId();
		}

		public Texture2D GetTexture2D(string key)
		{
			byte[] imageByteArray = client.GetImageByteArray(key);
			if (imageByteArray == null)
			{
				return null;
			}
			return Utils.GetTexture2DFromByteArray(imageByteArray);
		}

		public string GetText(string key)
		{
			return client.GetText(key);
		}

		public void PerformClick(string assetName)
		{
			client.PerformClick(assetName);
		}

		public void RecordImpression()
		{
			client.RecordImpression();
		}
	}
}
