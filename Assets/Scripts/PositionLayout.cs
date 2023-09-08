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
public class PositionLayout : MonoBehaviour
{
	public bool paddingLeft;

	public float paddingLeftValue;

	public bool paddingRight;

	public float paddingRightValue;

	public bool paddingTop;

	public float paddingTopValue;

	public bool paddingBottom;

	public float paddingBottomValue;

	public bool minLeft;

	public float minLeftValue;

	public bool maxRight;

	public float maxRightValue;

	public bool minBottom;

	public float minBottomValue;

	public bool maxTop;

	public float maxTopValue;

	private RectTransform parentRt;

	private void Start()
	{
		parentRt = base.transform.parent.GetComponent<RectTransform>();
	}

	private void Update()
	{
		Vector3 localPosition = base.transform.localPosition;
		float num = localPosition.x;
		Vector3 localPosition2 = base.transform.localPosition;
		float num2 = localPosition2.y;
		if (paddingLeft)
		{
			num = (0f - parentRt.rect.width) / 2f + paddingLeftValue;
		}
		if (paddingRight)
		{
			num = parentRt.rect.width / 2f - paddingRightValue;
		}
		if (paddingTop)
		{
			num2 = parentRt.rect.height / 2f - paddingTopValue;
		}
		if (paddingBottom)
		{
			num2 = (0f - parentRt.rect.height) / 2f + paddingBottomValue;
		}
		if (minLeft && num < minLeftValue)
		{
			num = minLeftValue;
		}
		if (maxRight && num > maxRightValue)
		{
			num = maxRightValue;
		}
		if (minBottom && num2 < minBottomValue)
		{
			num2 = minBottomValue;
		}
		if (maxTop && num2 > maxTopValue)
		{
			num2 = maxTopValue;
		}
		Transform transform = base.transform;
		float x = num;
		float y = num2;
		Vector3 localPosition3 = base.transform.localPosition;
		transform.localPosition = new Vector3(x, y, localPosition3.z);
	}

	private void OnDisable()
	{
	}
}
