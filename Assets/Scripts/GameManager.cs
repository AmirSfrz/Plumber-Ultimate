/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;

public class GameManager
{
	public static LevelGroup currentLevelGroup;

	public static Level currentLevel;

	public static bool openLevelSelection;

	public static int CurrentLevelNo
	{
		get
		{
			return PlayerPrefs.GetInt("currentLevelNo", 1);
		}
		set
		{
			PlayerPrefs.SetInt("currentLevelNo", value);
		}
	}

	public static int CurrentLevelGroupIndex
	{
		get
		{
			return PlayerPrefs.GetInt("CurrentLevelGroupIndex", 0);
		}
		set
		{
			PlayerPrefs.SetInt("CurrentLevelGroupIndex", value);
		}
	}

	public static bool IsSound
	{
		get
		{
			return PlayerPrefs.GetInt("IsSound", 1) == 1;
		}
		set
		{
			PlayerPrefs.SetInt("IsSound", value ? 1 : 0);
		}
	}

	public static bool IsMusic
	{
		get
		{
			return PlayerPrefs.GetInt("IsMusic", 1) == 1;
		}
		set
		{
			PlayerPrefs.SetInt("IsMusic", value ? 1 : 0);
		}
	}

	public static int Coin
	{
		get
		{
			return PlayerPrefs.GetInt("Coin", 50);
		}
		set
		{
			PlayerPrefs.SetInt("Coin", value);
		}
	}

	public static int StarLevel
	{
		get
		{
			return PlayerPrefs.GetInt("CurrentStarLevelget", 1);
		}
		set
		{
			PlayerPrefs.SetInt("CurrentStarLevelget", value);
		}
	}

	public static float StarLevelProgress => (float)CurrentStar / (float)TotalStar;

	private static int CurrentStar
	{
		get
		{
			return PlayerPrefs.GetInt("CurrentStar", 0);
		}
		set
		{
			PlayerPrefs.SetInt("CurrentStar", value);
		}
	}

	private static int TotalStar
	{
		get
		{
			return PlayerPrefs.GetInt("TotalStar", 3);
		}
		set
		{
			PlayerPrefs.SetInt("TotalStar", value);
		}
	}

	public static DateTime LastSpin
	{
		get
		{
			return DateTime.FromFileTime(long.Parse(PlayerPrefs.GetString("LastSpinDate", "0")));
		}
		set
		{
			PlayerPrefs.SetString("LastSpinDate", value.ToFileTime().ToString());
		}
	}

	public static bool CanSpin => SpinAfter.TotalDays >= 1.0;

	public static TimeSpan SpinAfter => DateTime.Now - LastSpin;

	public static TimeSpan RemandingForSpin => LastSpin.AddDays(1.0) - DateTime.Now;

	public static void AddStar(int s)
	{
		CurrentStar += s;
		if (CurrentStar >= TotalStar)
		{
			CurrentStar %= TotalStar;
			StarLevel++;
			TotalStar = 0;
			for (int i = 1; i <= StarLevel; i++)
			{
				TotalStar += i * 3;
			}
		}
	}
}
