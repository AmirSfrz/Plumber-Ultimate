/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

[CreateAssetMenu(fileName = "LevelGroup", menuName = "PipeOut/Create LevelGroup", order = 1)]
public class LevelGroup : ScriptableObject
{
	public string LevelGroupName = "Easy";

	public int TotalLevel = 50;

	public string ResourcesFolderPath = "Easy\\";

	public Color bgColor;

	public int StarXPReward = 1;

	public int NoOfHint = 4;

	public Sprite LevelDetailBG;

	public Sprite LevelHeaderBG;

	public int CompletedLevel
	{
		get
		{
			return PlayerPrefs.GetInt("Completed_" + LevelGroupName.ToUpper(), 0);
		}
		set
		{
			PlayerPrefs.SetInt("Completed_" + LevelGroupName.ToUpper(), value);
		}
	}

	public float AverageCompletedTime
	{
		get
		{
			return PlayerPrefs.GetFloat("AverageTime_" + LevelGroupName.ToUpper(), 0f);
		}
		set
		{
			PlayerPrefs.SetFloat("AverageTime_" + LevelGroupName.ToUpper(), value);
		}
	}

	public string AverageCompletedTimeString
	{
		get
		{
			int num = Mathf.FloorToInt(AverageCompletedTime);
			return (num / 60).ToString("00") + " : " + (num % 60).ToString("00");
		}
	}

	public void SetLevelCompleted(int LevelNo, float TimeToComplete)
	{
		if (CompletedLevel <= LevelNo)
		{
			CompletedLevel = LevelNo;
		}
		float averageCompletedTime = AverageCompletedTime;
		averageCompletedTime = Mathf.Abs(averageCompletedTime * (float)(CompletedLevel - 1) + TimeToComplete);
		averageCompletedTime = (AverageCompletedTime = averageCompletedTime / (float)CompletedLevel);
	}
}
