/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
	public bool countUp = true;

	public bool runOnStart;

	public int timeValue;

	public bool showHour;

	public bool showMinute = true;

	public bool showSecond = true;

	public Action onCountDownComplete;

	private bool isRunning;

	private Text textUI;

	private void Start()
	{
		textUI = GetComponent<Text>();
		UpdateText();
		if (runOnStart)
		{
			Run();
		}
	}

	private void OnDisable()
	{
		isRunning = false;
	}

	public void Run()
	{
		if (isRunning)
		{
			return;
		}
		if (timeValue <= 0)
		{
			if (onCountDownComplete != null)
			{
				onCountDownComplete();
			}
		}
		else
		{
			isRunning = true;
			StartCoroutine(UpdateClockText());
		}
	}

	private IEnumerator UpdateClockText()
	{
		while (isRunning)
		{
			UpdateText();
			yield return new WaitForSeconds(1f);
			if (countUp)
			{
				timeValue++;
			}
			else if (timeValue == 0)
			{
				if (onCountDownComplete != null)
				{
					onCountDownComplete();
				}
				Stop();
			}
			else
			{
				timeValue--;
			}
		}
	}

	public void SetTime(int value)
	{
		if (value < 0)
		{
			value = 0;
		}
		timeValue = value;
		UpdateText();
	}

	public void AddTime(int value)
	{
		timeValue += value;
		UpdateText();
	}

	private void UpdateText()
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds(timeValue);
		string text = (showHour && showMinute && showSecond) ? $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}" : ((!showHour || !showMinute) ? $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}" : $"{timeSpan.Hours:D2}:{timeSpan.Minutes:D2}");
		textUI.text = text;
	}

	public void Stop()
	{
		isRunning = false;
	}
}
