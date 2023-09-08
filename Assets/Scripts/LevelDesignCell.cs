/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using MS;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDesignCell : MonoBehaviour
{
	public Vector2Int pos;

	public Image selectionImage;

	public LevelCellData cellData;

	public Cell cell;

	public LevelDesignCell RightCell
	{
		get
		{
			Vector2Int v = pos + Vector2Int.right;
			if (!IsValidIndex(v))
			{
				return null;
			}
			return GetCellFromVector(v);
		}
	}

	public LevelDesignCell LeftCell
	{
		get
		{
			Vector2Int v = pos + Vector2Int.left;
			if (!IsValidIndex(v))
			{
				return null;
			}
			return GetCellFromVector(v);
		}
	}

	public LevelDesignCell TopCell
	{
		get
		{
			Vector2Int v = pos + Vector2Int.down;
			if (!IsValidIndex(v))
			{
				return null;
			}
			return GetCellFromVector(v);
		}
	}

	public LevelDesignCell BottomCell
	{
		get
		{
			Vector2Int v = pos + Vector2Int.up;
			if (!IsValidIndex(v))
			{
				return null;
			}
			return GetCellFromVector(v);
		}
	}

	private void Start()
	{
		if (LevelDesigner.instance != null)
		{
			GetComponent<PointerEvent>().onLeftClick.AddListener(delegate
			{
				LevelDesigner.instance.OnButtonClick(this);
			});
			GetComponent<PointerEvent>().onRightClick.AddListener(delegate
			{
				LevelDesigner.instance.OnButtonClick(this);
				LevelDesigner.instance.cellTypePopup.Open();
			});
		}
	}

	[ContextMenu("Update")]
	public void UpdateCell()
	{
		if (cell != null)
		{
			UnityEngine.Object.Destroy(cell.gameObject);
		}
		cell = UnityEngine.Object.Instantiate(LevelDesigner.instance._cellPrefab[cellData.CellIndex], base.transform);
		cell.SetLevelData(cellData);
		cell.GetComponent<Image>().enabled = false;
		cell.GetComponent<Button>().enabled = false;
		cell.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
		cell.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
	}

	public void RemoveAllPipeColor()
	{
		cell.RemoveAllPipeColor();
	}

	private bool IsValidIndex(Vector2Int v)
	{
		if (v.x < 0 || v.y < 0 || v.x >= LevelDesigner.instance.column || v.y >= LevelDesigner.instance.row)
		{
			return false;
		}
		Cell cell = GetCellFromVector(v).cell;
		if (cell.pipeCellType == CellType.Blank)
		{
			return false;
		}
		return true;
	}

	private LevelDesignCell GetCellFromVector(Vector2Int v)
	{
		int index = v.y * LevelDesigner.instance.column + v.x;
		return LevelDesigner.instance.allCellList[index];
	}

	public bool HasSide(Side side)
	{
		foreach (Pipe pipe in cell.pipes)
		{
			if (pipe.R && side == Side.R)
			{
				return true;
			}
			if (pipe.L && side == Side.L)
			{
				return true;
			}
			if (pipe.T && side == Side.T)
			{
				return true;
			}
			if (pipe.B && side == Side.B)
			{
				return true;
			}
		}
		return false;
	}

	public bool HasAnyColor()
	{
		foreach (Pipe pipe in cell.pipes)
		{
			if (pipe.fillColor.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	public void FillColor(List<PipeColor> c, LevelDesignCell source, Side sourceSide)
	{
		if (cell.pipeCellType != CellType.Start)
		{
			foreach (Pipe pipe in cell.pipes)
			{
				if ((pipe.L && sourceSide == Side.R) || (pipe.R && sourceSide == Side.L) || (pipe.T && sourceSide == Side.B) || (pipe.B && sourceSide == Side.T))
				{
					if (pipe.AddColor(c) == 0)
					{
						break;
					}
					if (pipe.L && LeftCell != null && LeftCell != source)
					{
						LeftCell.FillColor(c, this, Side.L);
					}
					if (pipe.R && RightCell != null && RightCell != source)
					{
						RightCell.FillColor(c, this, Side.R);
					}
					if (pipe.T && TopCell != null && TopCell != source)
					{
						TopCell.FillColor(c, this, Side.T);
					}
					if (pipe.B && BottomCell != null && BottomCell != source)
					{
						BottomCell.FillColor(c, this, Side.B);
					}
				}
			}
		}
	}
}
