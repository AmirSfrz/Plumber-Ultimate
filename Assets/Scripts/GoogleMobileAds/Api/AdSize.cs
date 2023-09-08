/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

namespace GoogleMobileAds.Api
{
	public class AdSize
	{
		private bool isSmartBanner;

		private int width;

		private int height;

		public static readonly AdSize Banner = new AdSize(320, 50);

		public static readonly AdSize MediumRectangle = new AdSize(300, 250);

		public static readonly AdSize IABBanner = new AdSize(468, 60);

		public static readonly AdSize Leaderboard = new AdSize(728, 90);

		public static readonly AdSize SmartBanner = new AdSize(isSmartBanner: true);

		public static readonly int FullWidth = -1;

		public int Width => width;

		public int Height => height;

		public bool IsSmartBanner => isSmartBanner;

		public AdSize(int width, int height)
		{
			isSmartBanner = false;
			this.width = width;
			this.height = height;
		}

		private AdSize(bool isSmartBanner)
			: this(0, 0)
		{
			this.isSmartBanner = isSmartBanner;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}
			AdSize adSize = (AdSize)obj;
			return width == adSize.width && height == adSize.height && isSmartBanner == adSize.isSmartBanner;
		}

		public static bool operator ==(AdSize a, AdSize b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(AdSize a, AdSize b)
		{
			return !a.Equals(b);
		}

		public override int GetHashCode()
		{
			int num = 71;
			int num2 = 11;
			int num3 = num;
			num3 = ((num3 * num2) ^ width.GetHashCode());
			num3 = ((num3 * num2) ^ height.GetHashCode());
			return (num3 * num2) ^ isSmartBanner.GetHashCode();
		}
	}
}
