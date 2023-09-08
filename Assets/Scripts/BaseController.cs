/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections;
using UnityEngine;

public class BaseController : MonoBehaviour
{
	public GameObject gameMaster;

	public string sceneName;

	protected virtual void Awake()
	{
		if (GameMaster.instance == null && gameMaster != null)
		{
			Object.Instantiate(gameMaster);
		}
	}

	protected virtual void Start()
	{
		if (JobWorker.instance.onEnterScene != null)
		{
			JobWorker.instance.onEnterScene(sceneName);
		}
	}

	public virtual void OnApplicationPause(bool pause)
	{
		UnityEngine.Debug.Log("On Application Pause");
		if (!pause)
		{
			Timer.Schedule(this, 0.5f, delegate
			{
				CUtils.ShowInterstitialAd();
			});
		}
	}

	private IEnumerator SavePrefs()
	{
		while (true)
		{
			yield return new WaitForSeconds(5f);
			PlayerPrefs.Save();
		}
	}
}
