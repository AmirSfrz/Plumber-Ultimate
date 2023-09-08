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

public class LevelDesigner : MonoBehaviour
{
	public List<Cell> _cellPrefab;

	public LevelDesignCell _levelDesigncellPrefab;

	public Button newBtn;

	public Button openBtn;

	public Button saveBtn;

	public int row;

	public int column;

	public InputField rowInput;

	public InputField columnInput;

	public Text levelSizeLbl;

	public Popup cellTypePopup;

	public Text pipeColorLbl;

	public GameObject chooseColorBegin;

	public GameObject chooseColorEnd;

	[Header("Layout Setting")]
	public RectTransform _levelContainer;

	public float _space = 1f;

	public bool enableSaveWithoutCheck;

	[HideInInspector]
	public List<LevelDesignCell> allCellList;

	private RectTransform rootCanvas;

	private float _levelContainerMaxHeight;

	public static LevelDesigner instance;

	private LevelDesignCell _selectedCell;

	public LevelDesignCell SelectedCell
	{
		get
		{
			return _selectedCell;
		}
		set
		{
			if (_selectedCell != null)
			{
				_selectedCell.selectionImage.gameObject.SetActive(value: false);
			}
			_selectedCell = value;
			if (_selectedCell != null)
			{
				chooseColorBegin.gameObject.SetActive(SelectedCell != null && SelectedCell.cell.pipeCellType == CellType.Start);
				chooseColorEnd.gameObject.SetActive(SelectedCell != null && SelectedCell.cell.pipeCellType == CellType.End);
				_selectedCell.selectionImage.gameObject.SetActive(value: true);
			}
			else
			{
				chooseColorBegin.gameObject.SetActive(value: false);
				chooseColorEnd.gameObject.SetActive(value: false);
				UpdatePipes();
			}
		}
	}

	private void Start()
	{
		instance = this;
		rowInput.onValueChanged.AddListener(delegate(string arg)
		{
			if (arg.StartsWith("-"))
			{
				rowInput.text = string.Empty;
			}
			else
			{
				int.TryParse(arg, out row);
			}
		});
		columnInput.onValueChanged.AddListener(delegate(string arg)
		{
			if (arg.StartsWith("-"))
			{
				columnInput.text = string.Empty;
			}
			else
			{
				int.TryParse(arg, out column);
			}
		});
		ResetLevelDesigner();
		rootCanvas = base.transform.parent.GetComponent<RectTransform>();
		_levelContainerMaxHeight = _levelContainer.rect.height;
	}

	private void Update()
	{
		if (SelectedCell != null)
		{
			if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha1) || UnityEngine.Input.GetKeyUp(KeyCode.Keypad1))
			{
				OnCellSilider(2f);
				OnCRotSilider(1f);
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha2) || UnityEngine.Input.GetKeyUp(KeyCode.Keypad2))
			{
				OnCellSilider(2f);
				OnCRotSilider(0f);
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.Alpha3) || UnityEngine.Input.GetKeyUp(KeyCode.Keypad3))
			{
				OnCellSilider(6f);
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.X))
			{
				OnCellSilider(0f);
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.RightArrow))
			{
				OnCRotSilider((SelectedCell.cellData.RightRotationValue + 1) % 4);
			}
			else if (UnityEngine.Input.GetKeyUp(KeyCode.LeftArrow))
			{
				OnCRotSilider((SelectedCell.cellData.RightRotationValue - 1 >= 0) ? (SelectedCell.cellData.RightRotationValue - 1) : 3);
			}
		}
		saveBtn.interactable = (saveBtn.interactable || enableSaveWithoutCheck);
	}

	public void OnNewBtn()
	{
		if (rowInput.gameObject.activeSelf)
		{
			if (string.IsNullOrEmpty(rowInput.text) || string.IsNullOrEmpty(columnInput.text))
			{
				Toast.instance.ShowMessage("You can't leave row or column empty");
				return;
			}
			if (row == 0 || column == 0)
			{
				Toast.instance.ShowMessage("Row and column must be larger than 0");
				return;
			}
			SetupLevel(row, column);
			newBtn.GetComponentInChildren<Text>().text = "New";
			openBtn.gameObject.SetActive(value: true);
			saveBtn.gameObject.SetActive(value: true);
			rowInput.gameObject.SetActive(value: false);
			columnInput.gameObject.SetActive(value: false);
		}
		else
		{
			newBtn.GetComponentInChildren<Text>().text = "Ok";
			openBtn.gameObject.SetActive(value: false);
			saveBtn.gameObject.SetActive(value: false);
			rowInput.gameObject.SetActive(value: true);
			columnInput.gameObject.SetActive(value: true);
			ResetLevelDesigner();
		}
	}

	public void OnOpenBtn()
	{
	}

	public void OnSaveBtn()
	{
	}

	public void ResetLevelDesigner()
	{
		SelectedCell = null;
		saveBtn.interactable = false;
		allCellList = new List<LevelDesignCell>();
		for (int num = _levelContainer.childCount - 1; num >= 0; num--)
		{
			UnityEngine.Object.DestroyImmediate(_levelContainer.GetChild(num).gameObject);
		}
	}

	public void SetupLevel(int row, int column)
	{
		ResetLevelDesigner();
		_levelContainer.sizeDelta = new Vector2(rootCanvas.rect.width, _levelContainerMaxHeight);
		float a = Mathf.Min(_levelContainer.rect.width / (float)column, _levelContainer.rect.height / (float)row);
		a = Mathf.Min(a, 140f);
		_levelContainer.sizeDelta = new Vector2((float)column * a, (float)row * a);
		for (int i = 0; i < row; i++)
		{
			for (int j = 0; j < column; j++)
			{
				LevelDesignCell levelDesignCell = UnityEngine.Object.Instantiate(_levelDesigncellPrefab, _levelContainer);
				levelDesignCell.GetComponent<RectTransform>().sizeDelta = Vector2.one * a;
				levelDesignCell.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)j * a + (float)j * _space, (float)(-i) * a - (float)i * _space);
				levelDesignCell.cellData = new LevelCellData(0, 0, 0, PipeColor.None, redundant: false);
				levelDesignCell.pos = new Vector2Int(j, i);
				allCellList.Add(levelDesignCell);
				levelDesignCell.UpdateCell();
			}
		}
		SelectedCell = null;
	}

	public void OnCellSilider(float t)
	{
		int cellIndex = (int)t;
		if (SelectedCell != null)
		{
			SelectedCell.cellData.CellIndex = cellIndex;
			SelectedCell.cellData.DefaultColor = PipeColor.None;
			SelectedCell.UpdateCell();
		}
		chooseColorBegin.gameObject.SetActive(SelectedCell != null && SelectedCell.cell.pipeCellType == CellType.Start);
		chooseColorEnd.gameObject.SetActive(SelectedCell != null && SelectedCell.cell.pipeCellType == CellType.End);
		UpdatePipes();
	}

	public void OnCRotSilider(float t)
	{
		int num = (int)t;
		if (SelectedCell != null)
		{
			SelectedCell.cellData.RightRotationValue = num;
			SelectedCell.cell.RotationValue = num;
			SelectedCell.cell.ApplyRotationOnImage();
			UpdatePipes();
		}
	}

	public void OnColorChosen(int v)
	{
		Text text = pipeColorLbl;
		PipeColor pipeColor = (PipeColor)v;
		text.text = "Color : " + pipeColor.ToString();
		SelectedCell.cellData.DefaultColor = (PipeColor)v;
		SelectedCell.cell.defaultColor = (PipeColor)v;
		UpdatePipes();
	}

	public void OnButtonClick(LevelDesignCell c)
	{
		SelectedCell = c;
	}

	private void UpdatePipes()
	{
		allCellList.ForEach(delegate(LevelDesignCell obj)
		{
			obj.RemoveAllPipeColor();
		});
		allCellList.ForEach(delegate(LevelDesignCell designCell)
		{
			Cell cell = designCell.cell;
			if (cell.pipeCellType == CellType.Start)
			{
				LevelDesignCell levelDesignCell = cell.pipes[0].T ? designCell.TopCell : (cell.pipes[0].B ? designCell.BottomCell : ((!cell.pipes[0].L) ? designCell.RightCell : designCell.LeftCell));
				Side sourceSide = cell.pipes[0].T ? Side.T : (cell.pipes[0].R ? Side.R : (cell.pipes[0].B ? Side.B : Side.L));
				if (levelDesignCell != null)
				{
					levelDesignCell.FillColor(new List<PipeColor>
					{
						cell.defaultColor
					}, designCell, sourceSide);
				}
			}
		});
		allCellList.ForEach(delegate(LevelDesignCell obj)
		{
			obj.cell.UpdatePipeColor();
		});
		saveBtn.interactable = IsGameOver();
	}

	public bool IsGameOver()
	{
		List<LevelDesignCell> list = allCellList.FindAll((LevelDesignCell obj) => obj.cell.pipeCellType == CellType.End);
		List<LevelDesignCell> list2 = allCellList.FindAll((LevelDesignCell obj) => obj.cell.pipeCellType == CellType.Start);
		List<LevelDesignCell> list3 = list.FindAll((LevelDesignCell x) => x.cell.defaultColor == PipeColor.None);
		List<LevelDesignCell> list4 = list2.FindAll((LevelDesignCell x) => x.cell.defaultColor == PipeColor.None);
		if (list.Count == 0 || list2.Count == 0 || list3.Count != 0 || list4.Count != 0)
		{
			return false;
		}
		foreach (LevelDesignCell item in list)
		{
			if (item.cell.defaultColor != ColorManager.MixPipeColor(item.cell.pipes[0].fillColor))
			{
				return false;
			}
		}
		foreach (LevelDesignCell allCell in allCellList)
		{
			Cell cell = allCell.cell;
			foreach (Pipe pipe in cell.pipes)
			{
				if (((cell.pipeCellType != CellType.Start && cell.pipeCellType != CellType.End) ? ColorManager.MixPipeColor(pipe.fillColor) : cell.defaultColor) != 0)
				{
					if ((pipe.L && allCell.LeftCell == null) || (pipe.R && allCell.RightCell == null) || (pipe.T && allCell.TopCell == null) || (pipe.B && allCell.BottomCell == null))
					{
						return false;
					}
					if ((pipe.L && !allCell.LeftCell.HasSide(Side.R)) || (pipe.R && !allCell.RightCell.HasSide(Side.L)) || (pipe.T && !allCell.TopCell.HasSide(Side.B)) || (pipe.B && !allCell.BottomCell.HasSide(Side.T)))
					{
						return false;
					}
				}
			}
		}
		return true;
	}
}
