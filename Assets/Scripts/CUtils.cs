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
using UnityEngine.SceneManagement;

public class CUtils
{
	public static void OpenStore()
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=" + GameConfig.instance.androidPackageID);
	}

	public static void OpenStore(string id)
	{
		Application.OpenURL("https://play.google.com/store/apps/details?id=" + id);
	}

	public static void LikeFacebookPage(string faceID)
	{
		if (Application.platform == RuntimePlatform.IPhonePlayer || (Application.platform == RuntimePlatform.Android && CheckPackageAppIsPresent("com.facebook.katana")))
		{
			Application.OpenURL("fb://page/" + faceID);
		}
		else
		{
			Application.OpenURL("https://www.facebook.com/" + faceID);
		}
		SetLikeFbPage(faceID);
	}

	private static bool CheckPackageAppIsPresent(string package)
	{
		AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		AndroidJavaObject @static = androidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");
		AndroidJavaObject androidJavaObject = @static.Call<AndroidJavaObject>("getPackageManager", new object[0]);
		AndroidJavaObject androidJavaObject2 = androidJavaObject.Call<AndroidJavaObject>("getInstalledPackages", new object[1]
		{
			0
		});
		int num = androidJavaObject2.Call<int>("size", new object[0]);
		for (int i = 0; i < num; i++)
		{
			AndroidJavaObject androidJavaObject3 = androidJavaObject2.Call<AndroidJavaObject>("get", new object[1]
			{
				i
			});
			string text = androidJavaObject3.Get<string>("packageName");
			if (text.CompareTo(package) == 0)
			{
				return true;
			}
		}
		return false;
	}

	public static void SetBuyItem()
	{
		SetBool("buy_item", value: true);
	}

	public static void SetRemoveAds(bool value)
	{
		SetBool("remove_ads", value);
	}

	public static bool IsAdsRemoved()
	{
		return GetBool("remove_ads");
	}

	public static bool IsBuyItem()
	{
		return GetBool("buy_item");
	}

	public static void SetRateGame()
	{
		SetBool("rate_game", value: true);
	}

	public static bool IsGameRated()
	{
		return GetBool("rate_game");
	}

	public static void SetLikeFbPage(string id)
	{
		SetBool("like_page_" + id, value: true);
	}

	public static bool IsLikedFbPage(string id)
	{
		return GetBool("like_page_" + id);
	}

	public static void SetDouble(string key, double value)
	{
		PlayerPrefs.SetString(key, DoubleToString(value));
	}

	public static double GetDouble(string key, double defaultValue)
	{
		string defaultValue2 = DoubleToString(defaultValue);
		return StringToDouble(PlayerPrefs.GetString(key, defaultValue2));
	}

	public static double GetDouble(string key)
	{
		return GetDouble(key, 0.0);
	}

	private static string DoubleToString(double target)
	{
		return target.ToString("R");
	}

	private static double StringToDouble(string target)
	{
		if (string.IsNullOrEmpty(target))
		{
			return 0.0;
		}
		return double.Parse(target);
	}

	public static void SetBool(string key, bool value)
	{
		PlayerPrefs.SetInt(key, value ? 1 : 0);
	}

	public static bool GetBool(string key, bool defaultValue = false)
	{
		int defaultValue2 = defaultValue ? 1 : 0;
		return PlayerPrefs.GetInt(key, defaultValue2) == 1;
	}

	public static double GetCurrentTime()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
	}

	public static double GetCurrentTimeInDays()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalDays;
	}

	public static double GetCurrentTimeInMills()
	{
		return DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
	}

	public static T GetRandom<T>(params T[] arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Length)];
	}

	public static bool IsActionAvailable(string action, int time, bool availableFirstTime = true)
	{
		if (!PlayerPrefs.HasKey(action + "_time"))
		{
			if (!availableFirstTime)
			{
				SetActionTime(action);
			}
			return availableFirstTime;
		}
		int num = (int)(GetCurrentTime() - GetActionTime(action));
		return num >= time;
	}

	public static double GetActionDeltaTime(string action)
	{
		if (GetActionTime(action) == 0.0)
		{
			return 0.0;
		}
		return GetCurrentTime() - GetActionTime(action);
	}

	public static void SetActionTime(string action)
	{
		SetDouble(action + "_time", GetCurrentTime());
	}

	public static void SetActionTime(string action, double time)
	{
		SetDouble(action + "_time", time);
	}

	public static double GetActionTime(string action)
	{
		return GetDouble(action + "_time");
	}

	public static void ShowInterstitialAd()
	{
		if (IsActionAvailable("show_ads", GameConfig.instance.interstitialAdPeriod))
		{
			bool flag = AdmobController.instance.ShowInterstitial();
			if (!flag)
			{
				AdmobController.instance.RequestInterstitial();
			}
			if (flag)
			{
				SetActionTime("show_ads");
			}
		}
	}

	public static void LoadScene(int sceneIndex, bool useScreenFader = false)
	{
		if (useScreenFader)
		{
			ScreenFader.instance.GotoScene(sceneIndex);
		}
		else
		{
			SceneManager.LoadScene(sceneIndex);
		}
	}

	public static void ReloadScene(bool useScreenFader = false)
	{
		int buildIndex = SceneManager.GetActiveScene().buildIndex;
		LoadScene(buildIndex, useScreenFader);
	}
}
