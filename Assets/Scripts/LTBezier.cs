/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

public class LTBezier
{
	public float length;

	private Vector3 a;

	private Vector3 aa;

	private Vector3 bb;

	private Vector3 cc;

	private float len;

	private float[] arcLengths;

	public LTBezier(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float precision)
	{
		this.a = a;
		aa = -a + 3f * (b - c) + d;
		bb = 3f * (a + c) - 6f * b;
		cc = 3f * (b - a);
		len = 1f / precision;
		arcLengths = new float[(int)len + 1];
		arcLengths[0] = 0f;
		Vector3 vector = a;
		float num = 0f;
		for (int i = 1; (float)i <= len; i++)
		{
			Vector3 vector2 = bezierPoint((float)i * precision);
			num += (vector - vector2).magnitude;
			arcLengths[i] = num;
			vector = vector2;
		}
		length = num;
	}

	private float map(float u)
	{
		float num = u * arcLengths[(int)len];
		int num2 = 0;
		int num3 = (int)len;
		int num4 = 0;
		while (num2 < num3)
		{
			num4 = num2 + ((int)((float)(num3 - num2) / 2f) | 0);
			if (arcLengths[num4] < num)
			{
				num2 = num4 + 1;
			}
			else
			{
				num3 = num4;
			}
		}
		if (arcLengths[num4] > num)
		{
			num4--;
		}
		if (num4 < 0)
		{
			num4 = 0;
		}
		return ((float)num4 + (num - arcLengths[num4]) / (arcLengths[num4 + 1] - arcLengths[num4])) / len;
	}

	private Vector3 bezierPoint(float t)
	{
		return ((aa * t + bb) * t + cc) * t + a;
	}

	public Vector3 point(float t)
	{
		return bezierPoint(map(t));
	}
}
