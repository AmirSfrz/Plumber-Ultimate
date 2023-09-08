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

public class CurrencyController
{
	public const string CURRENCY = "ruby";

	public const int DEFAULT_CURRENCY = 10;

	public static Action onBalanceChanged;

	public static Action<int> onBallanceIncreased;

	public static int GetBalance()
	{
		return PlayerPrefs.GetInt("ruby", 10);
	}

	public static void SetBalance(int value)
	{
		PlayerPrefs.SetInt("ruby", value);
		PlayerPrefs.Save();
	}

	public static void CreditBalance(int value)
	{
		int balance = GetBalance();
		SetBalance(balance + value);
		if (onBalanceChanged != null)
		{
			onBalanceChanged();
		}
		if (onBallanceIncreased != null)
		{
			onBallanceIncreased(value);
		}
	}

	public static bool DebitBalance(int value)
	{
		int balance = GetBalance();
		if (balance < value)
		{
			return false;
		}
		SetBalance(balance - value);
		if (onBalanceChanged != null)
		{
			onBalanceChanged();
		}
		return true;
	}
}
