/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using GoogleMobileAds.Api;
using MS;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    public List<Cell> _cellPrefab;

    public Popup menuPopup;

    [Header("GameOver Setting")]
    public Popup gameOverPopup;

    public Text starLevel;

    public Button nextBtn;

    public Button closeBtn;

    public Image fillBar;

    public ParticleSystem goParticle;

    [Header("Layout Setting")]
    public RectTransform _levelContainer;

    public float _space;

    public LevelGroup[] levelGroups;

    public static GamePlayManager instance;

    [HideInInspector]
    public List<Cell> allCellList;

    [HideInInspector]
    public List<Cell> startCellList;

    [HideInInspector]
    public List<Cell> midCellList;

    [HideInInspector]
    public List<Cell> endCellList;

    [HideInInspector]
    public List<Cell> visibleCellList;

    [HideInInspector]
    public bool isGameOver;

    [HideInInspector]
    public bool closeGameOver;

    [HideInInspector]
    public int row;

    [HideInInspector]
    public int column;

    private List<UndoAction> undoList;

    private float startTime;

    private RectTransform rootCanvas;

    private float _levelContainerMaxHeight;

    private Vector3 _levelContainerPosition;

    private bool isRotating;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        rootCanvas = base.transform.parent.GetComponent<RectTransform>();
        _levelContainerMaxHeight = _levelContainer.rect.height;
        _levelContainerPosition = _levelContainer.localPosition;
        SetupLevel();
    }

    private void ShowMessagePopup()
    {
        int messagePopupIndex = GameManager.currentLevel.messagePopupIndex;
        if (messagePopupIndex != -1)
        {
            MessagePopup.instance.ShowMessage(messagePopupIndex);
        }
    }


    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Side GetRandomSide()
    {
        int rand = UnityEngine.Random.Range(0, 4);
        switch (rand)
        {
            case 0:
                return Side.L;
                break;
            case 1:
                return Side.R;
                break;
            case 2:
                return Side.T;
                break;
            case 3:
                return Side.B;
                break;
        }
        return Side.L;
    }

    private Side OppositeSide(Side side)
    {
        switch (side)
        {
            case Side.L:
                return Side.R;
            case Side.R:
                return Side.L;
            case Side.T:
                return Side.B;
            case Side.B:
                return Side.T;
            default:
                return Side.R;
        }
    }

    private int getNeighbour(int position, Side side)
    {
        switch (side)
        {
            case Side.L:
                if (position % GameManager.currentLevel.column == 0)
                    return -1;
                else
                    return position - 1;
            case Side.R:
                if (position % GameManager.currentLevel.column == GameManager.currentLevel.column - 1)
                    return -1;
                else
                    return position + 1;
            case Side.T:
                if (position / GameManager.currentLevel.row == 0)
                    return -1;
                return position - GameManager.currentLevel.column;
            case Side.B:
                if (position / GameManager.currentLevel.row == GameManager.currentLevel.row - 1)
                    return -1;
                return position + GameManager.currentLevel.column;
            default:
                break;
        }
        return -1;
    }

    public PipeColor ColorMerge(PipeColor color1, PipeColor color2)
    {
        string color1String = color1.ToString();
        string color2String = color2.ToString();

        string result = new string(string.Concat(color1String, color2String).Distinct().OrderBy(c => c).ToArray());
        // Parse the concatenated string into an enum
        PipeColor parsedEnum;
        if (Enum.TryParse(result, out parsedEnum))
        {
            return parsedEnum;
        }
        else
        {
            return PipeColor.None;
        }
    }



    public void GenerateNewLevel()
    {
        GameManager.currentLevel = new Level();

        GameManager.currentLevel.row = 6;
        GameManager.currentLevel.column = 6;

        GameManager.currentLevel.cellList = new List<LevelCellData>();
        List<LevelGenerateNode> nodes = new List<LevelGenerateNode>();
        for (int i = 0; i < GameManager.currentLevel.row * GameManager.currentLevel.column; i++)
        {
            LevelCellData cellData = new LevelCellData(0, 0, 0, PipeColor.None, false);

            GameManager.currentLevel.cellList.Add(cellData);
            nodes.Add(new LevelGenerateNode());
        }

        //LevelCellData cellData = new LevelCellData(1, 3, 0, PipeColor.A, false);
        //GameManager.currentLevel.cellList.Add(cellData);
        //cellData = new LevelCellData(2, 2, 0, PipeColor.None, false);
        //GameManager.currentLevel.cellList.Add(cellData);
        //cellData = new LevelCellData(2, 0, 0, PipeColor.None, false);
        //GameManager.currentLevel.cellList.Add(cellData);
        //cellData = new LevelCellData(7, 1, 0, PipeColor.A, false);
        //GameManager.currentLevel.cellList.Add(cellData);

        Dictionary<int, bool> notEmptyPlaces = new Dictionary<int, bool>();
        /// start cells position
        int startPointsCount = UnityEngine.Random.Range(1, 4);
        int visitorPointIndex = UnityEngine.Random.Range(0, startPointsCount);
        PipeColor visitorColor = PipeColor.None;
        for (int i = 0; i < startPointsCount; i++)
        {
            LevelCellData startCell = new LevelCellData(1, UnityEngine.Random.Range(0, 4), 0, i == 0 ? PipeColor.A : i == 1 ? PipeColor.B : PipeColor.C, false);

            int position = UnityEngine.Random.Range(0, GameManager.currentLevel.cellList.Count);
            //check if already exists
            if (notEmptyPlaces.ContainsKey(position))
            {
                i--;
                continue;
            }

            GameManager.currentLevel.cellList[position] = startCell;
            notEmptyPlaces.Add(position, true);

            nodes[position].lockWhenVisited = true;

            if (i == visitorPointIndex)
            {
                visitorPointIndex = position;
                visitorColor = startCell.DefaultColor;
            }
        }

        //end position
        LevelCellData endCell = new LevelCellData(7, UnityEngine.Random.Range(0, 4), 0, PipeColor.None, false);
        while (true)
        {
            int position = UnityEngine.Random.Range(0, GameManager.currentLevel.cellList.Count);
            if (notEmptyPlaces.ContainsKey(position))
            {
                continue;
            }
            GameManager.currentLevel.cellList[position] = endCell;
            notEmptyPlaces.Add(position, true);
            break;
        }

        int loopCounter = 5000;
        //start the process.
        while (true)
        {
            loopCounter--;
            if (loopCounter == 0)
            {
                GenerateNewLevel();
                return;
            }

            Side nextMove = GetRandomSide();

            int nextPos = getNeighbour(visitorPointIndex, nextMove);
            if (nextPos == -1)
                continue;

            if (nodes[visitorPointIndex].IsVisitAvailable(nextMove) && nodes[nextPos].IsVisitAvailable(OppositeSide(nextMove)))
            {
                nodes[visitorPointIndex].Visit(nextMove);
                nodes[nextPos].Visit(OppositeSide(nextMove));

                visitorPointIndex = nextPos;
                //check if needs a color change
                if (GameManager.currentLevel.cellList[visitorPointIndex].CellIndex == 1)
                {
                    visitorColor = ColorMerge(visitorColor, GameManager.currentLevel.cellList[visitorPointIndex].DefaultColor);
                }

                //check if finished
                if (GameManager.currentLevel.cellList[visitorPointIndex].CellIndex == 7)
                {
                    //set the finish color
                    GameManager.currentLevel.cellList[visitorPointIndex].DefaultColor = visitorColor;
                    break;
                }
            }
            else
                continue;
        }


        //place the cells
        for (int i = 0; i < nodes.Count; i++)
        {
            int cellIndex = 0;

            int visitCount = nodes[i].visitedSidesCount;
            if (visitCount == 0)
            {
                while (true)
                {
                    cellIndex = UnityEngine.Random.Range(0, 7);
                    if (cellIndex == 1 || cellIndex == 7 || cellIndex == 5)
                        continue;
                    else
                        break;
                }
            }
            if (visitCount == 2)
            {
                //check if its an straight line
                if (nodes[i].visitedSides[0] == OppositeSide(nodes[i].visitedSides[1]))
                    cellIndex = 2;
                else
                    cellIndex = 4;
            }
            if (visitCount == 3)
                cellIndex = 3;
            if (visitCount == 4)
                cellIndex = 6;

            if (cellIndex == 0)
                continue;

            LevelCellData cell = new LevelCellData(cellIndex, UnityEngine.Random.Range(0, 4), 0, PipeColor.None, false);
            GameManager.currentLevel.cellList[i] = cell;
        }

    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetupLevel()
    {
        isGameOver = false;
        closeGameOver = false;
        MessagePopup.instance.HideAll();
        GameManager.currentLevelGroup = (GameManager.currentLevelGroup ?? levelGroups[GameManager.CurrentLevelGroupIndex]);
        GameManager.CurrentLevelGroupIndex = Array.IndexOf(levelGroups, GameManager.currentLevelGroup);
        GameManager.currentLevel = Resources.Load<Level>(GameManager.currentLevelGroup.ResourcesFolderPath + GameManager.CurrentLevelNo);

        //TODO: generate new level
        //////////////////////////////////////////////////////////////////

        GenerateNewLevel();


        //////////////////////////////////////////////////////////////////




        if (GameManager.currentLevel == null)
        {
            UnityEngine.Debug.LogError("Level Not Found at : " + GameManager.currentLevelGroup.ResourcesFolderPath + GameManager.CurrentLevelNo);
            return;
        }
        row = GameManager.currentLevel.row;
        column = GameManager.currentLevel.column;
        float maxCellSize = GameManager.currentLevel.maxCellSize;
        startTime = Time.time;
        undoList = new List<UndoAction>();
        _levelContainer.sizeDelta = new Vector2(rootCanvas.rect.width, _levelContainerMaxHeight);
        float a = Mathf.Min(_levelContainer.rect.width / (float)column, _levelContainer.rect.height / (float)row);
        a = Mathf.Min(a, maxCellSize);
        _levelContainer.sizeDelta = new Vector2((float)column * a, (float)row * a);
        _levelContainer.transform.localPosition = _levelContainerPosition + GameManager.currentLevel.levelContainerDelta * a;
        for (int num = _levelContainer.childCount - 1; num >= 0; num--)
        {
            UnityEngine.Object.Destroy(_levelContainer.GetChild(num).gameObject);
        }
        startCellList = new List<Cell>();
        midCellList = new List<Cell>();
        endCellList = new List<Cell>();
        allCellList = new List<Cell>();
        visibleCellList = new List<Cell>();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < column; j++)
            {
                LevelCellData levelCellData = GameManager.currentLevel.cellList[i * column + j];
                Cell cell = UnityEngine.Object.Instantiate(_cellPrefab[levelCellData.CellIndex], _levelContainer);
                cell.GetComponent<RectTransform>().sizeDelta = Vector2.one * a;
                cell.GetComponent<RectTransform>().anchoredPosition = new Vector2((float)j * a + (float)j * _space, (float)(-i) * a - (float)i * _space);
                cell.name = i + " " + j;
                cell.pos = new Vector2Int(j, i);
                cell.SetLevelData(levelCellData);
                allCellList.Add(cell);
                if (cell.pipeCellType != 0)
                {
                    if (cell.pipeCellType == CellType.Start)
                    {
                        startCellList.Add(cell);
                    }
                    else if (cell.pipeCellType == CellType.Middle)
                    {
                        midCellList.Add(cell);
                    }
                    else
                    {
                        endCellList.Add(cell);
                    }
                    visibleCellList.Add(cell);
                }
            }
        }
        GameScene.instance.UpdateUI();
        Timer.Schedule(this, 0.5f, ShowMessagePopup);
        RecursivePipeColors();
        visibleCellList.ForEach(delegate (Cell obj)
        {
            obj.UpdatePipeColor();
        });
        CheckGameOver();
        CUtils.ShowInterstitialAd();
    }

    public void OnButtonClick(Cell c)
    {
        if (!isRotating && !isGameOver)
        {
            undoList.Add(new UndoAction(c, c.RotationValue));
            c.RotationValue++;
            StartCoroutine(RotatePipe(c));
        }
    }

    public void Undo()
    {
        if (!isRotating && undoList != null && undoList.Count != 0)
        {
            UndoAction undoAction = undoList[undoList.Count - 1];
            undoList.RemoveAt(undoList.Count - 1);
            undoAction.cell.RotationValue = undoAction.rotValue;
            StartCoroutine(RotatePipe(undoAction.cell));
        }
    }

    private IEnumerator RotatePipe(Cell c)
    {
        isRotating = true;
        if (c != null)
        {
            c.ApplyRotationOnImage(0.1f);
        }
        RecursivePipeColors();
        yield return new WaitForSeconds(0.1f);
        visibleCellList.ForEach(delegate (Cell obj)
        {
            obj.UpdatePipeColor();
        });
        isRotating = false;
        CheckGameOver();
    }

    private void RecursivePipeColors()
    {
        visibleCellList.ForEach(delegate (Cell obj)
        {
            obj.RemoveAllPipeColor();
        });
        startCellList.ForEach(delegate (Cell cell)
        {
            if (cell.pipeCellType == CellType.Start)
            {
                Cell cell2 = cell.pipes[0].T ? cell.TopCell : (cell.pipes[0].B ? cell.BottomCell : ((!cell.pipes[0].L) ? cell.RightCell : cell.LeftCell));
                Side sourceSide = cell.pipes[0].T ? Side.T : (cell.pipes[0].R ? Side.R : (cell.pipes[0].B ? Side.B : Side.L));
                if (cell2 != null)
                {
                    cell2.FillColor(new List<PipeColor>
                    {
                        cell.defaultColor
                    }, cell, sourceSide);
                }
            }
        });
    }

    public void GiveHint()
    {
        for (int i = 0; i < GameManager.currentLevelGroup.NoOfHint; i++)
        {
            List<Cell> list = visibleCellList.FindAll((Cell obj) => !obj.IsHint && !obj.redundant && !obj.IsInRighRotation());
            if (list != null && list.Count > 0)
            {
                list[UnityEngine.Random.Range(0, list.Count)].IsHint = true;
            }
        }
        StartCoroutine(RotatePipe(null));
    }

    private void CheckGameOver()
    {
        isGameOver = IsGameOver();
        if (isGameOver)
        {
            List<Cell> list = midCellList.FindAll((Cell x) => !x.HasAnyColor());
            foreach (Cell item in list)
            {
                item.transform.SetSiblingIndex(base.transform.childCount - 1);
                Rigidbody2D rigidbody2D = item.gameObject.AddComponent<Rigidbody2D>();
                rigidbody2D.gravityScale = 1.2f;
                rigidbody2D.AddForce(Quaternion.Euler(0f, 0f, UnityEngine.Random.Range(-30, 30)) * Vector3.up * 300f);
            }
            bool flag = list.Count > 0;
            float num = 0.7f;
            if (flag)
            {
                num += 0.6f;
            }
            Compliment.Type complimentType = GameManager.currentLevel.complimentType;
            if (complimentType != 0)
            {
                num += 0.6f;
                Compliment.instance.Show(complimentType);
            }
            StartCoroutine(ShowingGameOver(num));
        }
    }

    public bool IsGameOver()
    {
        foreach (Cell endCell in endCellList)
        {
            if (endCell.defaultColor != ColorManager.MixPipeColor(endCell.pipes[0].fillColor))
            {
                return false;
            }
        }
        foreach (Cell visibleCell in visibleCellList)
        {
            foreach (Pipe pipe in visibleCell.pipes)
            {
                if (((visibleCell.pipeCellType != CellType.Start && visibleCell.pipeCellType != CellType.End) ? ColorManager.MixPipeColor(pipe.fillColor) : visibleCell.defaultColor) != 0)
                {
                    if ((pipe.L && visibleCell.LeftCell == null) || (pipe.R && visibleCell.RightCell == null) || (pipe.T && visibleCell.TopCell == null) || (pipe.B && visibleCell.BottomCell == null))
                    {
                        Debug.Log("true 11111111111111111111111111111111111111111");
                        return true;
                    }
                    if ((pipe.L && !visibleCell.LeftCell.HasSide(Side.R)) || (pipe.R && !visibleCell.RightCell.HasSide(Side.L)) || (pipe.T && !visibleCell.TopCell.HasSide(Side.B)) || (pipe.B && !visibleCell.BottomCell.HasSide(Side.T)))
                    {
                        Debug.Log("true 222222222222222222222222222222222222222222");
                        return true;
                    }
                }
            }
        }
        return true;
    }

    private IEnumerator ShowingGameOver(float delay)
    {
        MessagePopup.instance.HideAll();
        yield return new WaitForSeconds(delay);
        starLevel.text = GameManager.StarLevel + string.Empty;
        fillBar.fillAmount = GameManager.StarLevelProgress;
        nextBtn.onClick.RemoveAllListeners();
        if (GameManager.currentLevelGroup.TotalLevel > GameManager.CurrentLevelNo)
        {
            nextBtn.onClick.AddListener(delegate
            {
                gameOverPopup.Close();
                Sound.instance.PlayButton();
                GameManager.CurrentLevelNo++;
                SetupLevel();
            });
            nextBtn.GetComponentInChildren<Text>().text = "NEXT";
        }
        else
        {
            nextBtn.onClick.AddListener(delegate
            {
                gameOverPopup.Close();
                closeGameOver = true;
                GameScene.instance.OnBackBtn();
            });
            nextBtn.GetComponentInChildren<Text>().text = "BACK";
        }
        nextBtn.interactable = false;
        closeBtn.interactable = false;
        gameOverPopup.Open();
        yield return new WaitForSeconds(0.5f);
        if (GameManager.currentLevelGroup.CompletedLevel < GameManager.CurrentLevelNo)
        {
            int sl = GameManager.StarLevel;
            for (int i = 0; i < GameManager.currentLevelGroup.StarXPReward; i++)
            {
                GameManager.AddStar(1);
                starLevel.text = GameManager.StarLevel + string.Empty;
                if (fillBar.fillAmount > GameManager.StarLevelProgress)
                {
                    while (fillBar.fillAmount < 1f)
                    {
                        fillBar.fillAmount += 0.01f;
                        yield return new WaitForSeconds(0.01f);
                    }
                    fillBar.fillAmount = 0f;
                }
                while (fillBar.fillAmount < GameManager.StarLevelProgress)
                {
                    fillBar.fillAmount += 0.01f;
                    yield return new WaitForSeconds(0.01f);
                }
            }
            if (sl < GameManager.StarLevel)
            {
                goParticle.gameObject.SetActive(value: true);
                goParticle.Emit(30);
                GameManager.Coin += UnityEngine.Random.Range(30, 45);
                GameScene.instance.UpdateUI();
            }
        }
        nextBtn.interactable = true;
        closeBtn.interactable = true;
        GameManager.currentLevelGroup.SetLevelCompleted(GameManager.CurrentLevelNo, Time.time - startTime);
    }

    public void NextGame()
    {
        if (GameManager.CurrentLevelNo > GameManager.currentLevelGroup.CompletedLevel)
        {
            if (GameManager.Coin < GameConfig.instance.numCoinForSkipGame)
            {
                Toast.instance.ShowMessage("You don't have enough coins");
                return;
            }
            GameManager.Coin -= GameConfig.instance.numCoinForSkipGame;
            GameManager.currentLevelGroup.SetLevelCompleted(GameManager.CurrentLevelNo, Time.time - startTime);
        }
        menuPopup.Close();
        Sound.instance.PlayButton();
        if (GameManager.currentLevelGroup.TotalLevel > GameManager.CurrentLevelNo)
        {
            Timer.Schedule(this, 0.8f, delegate
            {
                GameManager.CurrentLevelNo++;
                SetupLevel();
            });
        }
        else
        {
            GameScene.instance.OnBackBtn();
        }
    }
}
public class LevelGenerateNode
{
    public List<Side> visitedSides = new List<Side>();
    public bool lockWhenVisited = false;

    public int visitedSidesCount { get { return visitedSides.Count; } }

    public void Visit(Side side)
    {
        if (!visitedSides.Contains(side))
        {
            visitedSides.Add(side);
        }
    }

    public bool IsVisitAvailable(Side side)
    {
        if (!lockWhenVisited)
            return true;

        if (visitedSidesCount == 0)
            return true;

        if (visitedSides[0] == side) return true;

        return false;
    }
}