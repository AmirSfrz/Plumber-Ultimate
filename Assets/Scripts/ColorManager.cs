/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColorManager : MonoBehaviour
{
	public Color normalCellBGColor = Color.white;

	public Color hintCellBGColor = Color.white;

	public static ColorManager intance;

	private void Awake()
	{
		intance = this;
	}

	public static PipeColor MixPipeColor(List<PipeColor> c)
	{
		if (c == null || c.Count == 0)
		{
			return PipeColor.None;
		}
		if (c.Count >= 3 && c.Contains(PipeColor.A) && c.Contains(PipeColor.B) && c.Contains(PipeColor.C))
		{
			return PipeColor.ABC;
		}
		if (c.Count >= 2)
		{
			if (c.Contains(PipeColor.A) && c.Contains(PipeColor.B))
			{
				return PipeColor.AB;
			}
			if (c.Contains(PipeColor.A) && c.Contains(PipeColor.C))
			{
				return PipeColor.AC;
			}
			if (c.Contains(PipeColor.C) && c.Contains(PipeColor.B))
			{
				return PipeColor.BC;
			}
		}
		else if (c.Count >= 1)
		{
			return c[0];
		}
		return PipeColor.None;
	}

	public static List<PipeColor> SeprateColor(PipeColor c)
	{
		List<PipeColor> list = new List<PipeColor>();
		switch (c)
		{
		case PipeColor.A:
			list.Add(PipeColor.A);
			break;
		case PipeColor.B:
			list.Add(PipeColor.B);
			break;
		case PipeColor.C:
			list.Add(PipeColor.C);
			break;
		case PipeColor.AB:
			list.Add(PipeColor.A);
			list.Add(PipeColor.B);
			break;
		case PipeColor.AC:
			list.Add(PipeColor.A);
			list.Add(PipeColor.C);
			break;
		case PipeColor.BC:
			list.Add(PipeColor.B);
			list.Add(PipeColor.C);
			break;
		case PipeColor.ABC:
			list.Add(PipeColor.A);
			list.Add(PipeColor.B);
			list.Add(PipeColor.C);
			break;
		}
		return list;
	}
}
