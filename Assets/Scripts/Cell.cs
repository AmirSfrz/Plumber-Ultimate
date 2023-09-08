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
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
	public Vector2Int pos;

	public PipeColor defaultColor;

	public int rightRotationValue;

	[Header("PipeDetail")]
	public RectTransform pipeParent;

	public CellType pipeCellType;

	public List<Pipe> pipes;

	public List<Sprite> pipeSprites;

	public int correctRotateDelta = 4;

	[HideInInspector]
	public bool redundant;

	private bool _isHint;

	private int _rotationValue;

	public bool IsHint
	{
		get
		{
			return _isHint;
		}
		set
		{
			if (value)
			{
				RotationValue = rightRotationValue;
				ApplyRotationOnImage(0.05f);
				GetComponent<Image>().color = ColorManager.intance.hintCellBGColor;
			}
			else
			{
				GetComponent<Image>().color = ColorManager.intance.normalCellBGColor;
			}
			_isHint = value;
		}
	}

	public int RotationValue
	{
		get
		{
			return _rotationValue;
		}
		set
		{
			if (!IsHint)
			{
				int num = (value < 4) ? value : 0;
				num = (_rotationValue = ((num >= 0) ? num : 3));
				pipes.ForEach(delegate(Pipe obj)
				{
					obj.SetPipeRotation(_rotationValue);
				});
			}
		}
	}

	public Cell RightCell
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

	public Cell LeftCell
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

	public Cell TopCell
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

	public Cell BottomCell
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

	public void SetLevelData(LevelCellData lcd)
	{
		defaultColor = lcd.DefaultColor;
		if (pipeCellType != 0)
		{
			GetComponent<Button>().onClick.AddListener(delegate
			{
				if (GamePlayManager.instance != null)
				{
					GamePlayManager.instance.OnButtonClick(this);
				}
			});
		}
		rightRotationValue = lcd.RightRotationValue;
		RotationValue = lcd.CurrentRotationValue;
		ApplyRotationOnImage();
		UpdatePipeColor();
		IsHint = false;
		redundant = lcd.Redundant;
	}

	public void UpdatePipeColor()
	{
		if (pipeCellType == CellType.Middle)
		{
			pipes.ForEach(delegate(Pipe p)
			{
				p.pipeImage.sprite = pipeSprites[(int)ColorManager.MixPipeColor(p.fillColor)];
			});
		}
		else
		{
			pipes.ForEach(delegate(Pipe p)
			{
				p.pipeImage.sprite = pipeSprites[(int)defaultColor];
			});
		}
	}

	public void ApplyRotationOnImage(float t = 0f)
	{
		LeanTween.rotateZ(pipeParent.gameObject, (float)_rotationValue * -90f, t);
	}

	private bool IsValidIndex(Vector2Int v)
	{
		if (v.x < 0 || v.y < 0 || v.x >= GameManager.currentLevel.column || v.y >= GameManager.currentLevel.row)
		{
			return false;
		}
		Cell cellFromVector = GetCellFromVector(v);
		if (cellFromVector.pipeCellType == CellType.Blank)
		{
			return false;
		}
		return true;
	}

	private Cell GetCellFromVector(Vector2Int v)
	{
		int index = v.y * GameManager.currentLevel.column + v.x;
		return GamePlayManager.instance.allCellList[index];
	}

	public bool HasSide(Side side)
	{
		foreach (Pipe pipe in pipes)
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
		foreach (Pipe pipe in pipes)
		{
			if (pipe.fillColor.Count > 0)
			{
				return true;
			}
		}
		return false;
	}

	public bool IsInRighRotation()
	{
		return (Mathf.Max(RotationValue, rightRotationValue) - Mathf.Min(RotationValue, rightRotationValue)) % correctRotateDelta == 0;
	}

	public void RemoveAllPipeColor()
	{
		pipes.ForEach(delegate(Pipe obj)
		{
			obj.RemoveAll();
		});
	}

	public void FillColor(List<PipeColor> c, Cell source, Side sourceSide)
	{
		if (pipeCellType != CellType.Start)
		{
			foreach (Pipe pipe in pipes)
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
