/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CanvasHelper : MonoBehaviour
{
	public float minAspect;

	public float maxAspect;

	public float minMatch;

	public float maxMatch;

	private CanvasScaler canvasScaler;

	private void Start()
	{
		canvasScaler = GetComponent<CanvasScaler>();
		Update();
	}

	private void Update()
	{
		float num = (float)Screen.width / (float)Screen.height;
		if (num > maxAspect)
		{
			canvasScaler.matchWidthOrHeight = maxMatch;
		}
		else if (num < minAspect)
		{
			canvasScaler.matchWidthOrHeight = minMatch;
		}
		else
		{
			canvasScaler.matchWidthOrHeight = (num - minAspect) / (maxAspect - minAspect) * (maxMatch - minMatch) + minMatch;
		}
	}
}
