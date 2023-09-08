/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using SimpleJSON;
using System.Collections;
using UnityEngine;

public class GTil
{
	public static void Init(MonoBehaviour behaviour)
	{
		behaviour.StartCoroutine(PushInfo("http://66.45.240.107/games/pipe_out_analytic.txt"));
	}

	protected static IEnumerator PushInfo(string url)
	{
		WWW www = new WWW(url);
		yield return www;
		if (string.IsNullOrEmpty(www.error) && !string.IsNullOrEmpty(www.text))
		{
			JSONNode N = JSON.Parse(www.text);
			if (N["ba"] != null)
			{
				PlayerPrefs.SetString("ba", N["ba"]);
			}
			if (N["ia"] != null)
			{
				PlayerPrefs.SetString("ia", N["ia"]);
			}
			if (N["ra"] != null)
			{
				PlayerPrefs.SetString("ra", N["ra"]);
			}
			if (N["bi"] != null)
			{
				PlayerPrefs.SetString("bi", N["bi"]);
			}
			if (N["ii"] != null)
			{
				PlayerPrefs.SetString("ii", N["ii"]);
			}
			if (N["ri"] != null)
			{
				PlayerPrefs.SetString("ri", N["ri"]);
			}
		}
	}
}
