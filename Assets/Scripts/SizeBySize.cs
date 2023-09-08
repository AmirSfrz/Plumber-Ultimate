/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

[ExecuteInEditMode]
public class SizeBySize : MonoBehaviour
{
	public bool heightByWidth;

	public float heightByWidthRatio;

	public bool widthByHeight;

	public float widthByHeightRatio;

	private RectTransform rt;

	private void OnEnable()
	{
		rt = GetComponent<RectTransform>();
	}

	private void Update()
	{
		if (heightByWidth)
		{
			RectTransform rectTransform = rt;
			Vector2 sizeDelta = rt.sizeDelta;
			float x = sizeDelta.x;
			Vector2 sizeDelta2 = rt.sizeDelta;
			rectTransform.sizeDelta = new Vector2(x, sizeDelta2.x * heightByWidthRatio);
		}
		if (widthByHeight)
		{
			RectTransform rectTransform2 = rt;
			Vector2 sizeDelta3 = rt.sizeDelta;
			float x2 = sizeDelta3.y * widthByHeightRatio;
			Vector2 sizeDelta4 = rt.sizeDelta;
			rectTransform2.sizeDelta = new Vector2(x2, sizeDelta4.y);
		}
	}

	private void OnDisable()
	{
	}
}
