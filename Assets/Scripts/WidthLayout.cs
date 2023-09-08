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
public class WidthLayout : MonoBehaviour
{
	public bool padding;

	public float paddingValue;

	public bool maxWidth;

	public float maxWidthValue;

	public bool minWidth;

	public float minWidthValue;

	private RectTransform rt;

	private RectTransform parentRt;

	private void OnEnable()
	{
		rt = GetComponent<RectTransform>();
		parentRt = base.transform.parent.GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector2 sizeDelta = rt.sizeDelta;
		float num = sizeDelta.x;
		if (padding)
		{
			num = parentRt.rect.width - paddingValue * 2f;
		}
		if (maxWidth && num > maxWidthValue)
		{
			num = maxWidthValue;
		}
		if (minWidth && num < minWidthValue)
		{
			num = minWidthValue;
		}
		RectTransform rectTransform = rt;
		float x = num;
		Vector2 sizeDelta2 = rt.sizeDelta;
		rectTransform.sizeDelta = new Vector2(x, sizeDelta2.y);
		Vector2 pivot = rt.pivot;
		if (pivot.x == 0f)
		{
			RectTransform rectTransform2 = rt;
			float x2 = (0f - num) / 2f;
			Vector3 localPosition = rt.localPosition;
			float y = localPosition.y;
			Vector3 localPosition2 = rt.localPosition;
			rectTransform2.localPosition = new Vector3(x2, y, localPosition2.z);
		}
	}

	private void OnDisable()
	{
	}
}
