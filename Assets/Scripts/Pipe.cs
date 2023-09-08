/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections.Generic;
using UnityEngine.UI;

[Serializable]
public class Pipe
{
	public Image pipeImage;

	public bool L;

	public bool R;

	public bool T;

	public bool B;

	public List<PipeColor> fillColor = new List<PipeColor>();

	private int currentRotation;

	public int AddColor(PipeColor c)
	{
		int num = 0;
		if (c == PipeColor.None)
		{
			return 0;
		}
		if (!fillColor.Contains(c))
		{
			fillColor.Add(c);
			num++;
		}
		return num;
	}

	public int AddColor(List<PipeColor> c)
	{
		int addedColor = 0;
		c.ForEach(delegate(PipeColor obj)
		{
			addedColor += AddColor(obj);
		});
		return addedColor;
	}

	public void RemoveAll()
	{
		fillColor.Clear();
	}

	public void SetPipeRotation(int r)
	{
		while (currentRotation != r)
		{
			if (currentRotation < r)
			{
				bool r2 = R;
				R = T;
				T = L;
				L = B;
				B = r2;
				currentRotation++;
			}
			else
			{
				bool r3 = R;
				R = B;
				B = L;
				L = T;
				T = r3;
				currentRotation--;
			}
		}
	}
}
