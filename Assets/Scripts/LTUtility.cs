/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

public class LTUtility
{
	public static Vector3[] reverse(Vector3[] arr)
	{
		int num = arr.Length;
		int num2 = 0;
		int num3 = num - 1;
		while (num2 < num3)
		{
			Vector3 vector = arr[num2];
			arr[num2] = arr[num3];
			arr[num3] = vector;
			num2++;
			num3--;
		}
		return arr;
	}
}
