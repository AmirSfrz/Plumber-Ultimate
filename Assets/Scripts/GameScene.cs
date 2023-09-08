/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using MS;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
	[Header("Menu")]
	public Text coinLbl;

	public Text titleLevelGroupLbl;

	public Text titleLevelNoLbl;

	public Image titleBG;

	public Popup gameOverPopup;

	public Text rewardValueText;

	public Text coinForHintText;

	public GameObject skipCoinObj;

	public GameObject skipTextObj;

	public Text skipCoinValueText;

	public static GameScene instance;

	private void Start()
	{
		instance = this;
		rewardValueText.text = "+" + GameConfig.instance.rewardedVideoAmount;
		coinForHintText.text = GameConfig.instance.numCoinForHint.ToString();
		skipCoinValueText.text = GameConfig.instance.numCoinForSkipGame.ToString();
		Music.instance.Play(Music.Type.MainMusic);
	}

	public void UpdateUI()
	{
		coinLbl.text = GameManager.Coin + string.Empty;
		titleLevelGroupLbl.text = GameManager.currentLevelGroup.LevelGroupName;
		titleLevelNoLbl.text = GameManager.CurrentLevelNo + string.Empty;
		titleLevelNoLbl.color = GameManager.currentLevelGroup.bgColor;
		titleBG.sprite = GameManager.currentLevelGroup.LevelHeaderBG;
	}

	public void OnBackBtn()
	{
		if (!GamePlayManager.instance.isGameOver || GamePlayManager.instance.closeGameOver)
		{
			Sound.instance.PlayButton(Sound.Button.Back);
			GameManager.openLevelSelection = true;
			SceneManager.LoadScene("HomeScene");
		}
	}

	public void ShowMenuPopup()
	{
		bool flag = GameManager.CurrentLevelNo > GameManager.currentLevelGroup.CompletedLevel;
		skipCoinObj.SetActive(flag);
		Transform transform = skipTextObj.transform;
		float x = (!flag) ? 12 : (-19);
		Vector3 localPosition = skipTextObj.transform.localPosition;
		transform.localPosition = new Vector3(x, localPosition.y);
		Sound.instance.PlayButton();
	}

	public void OnGameOverCloseBtn()
	{
		Sound.instance.PlayButton(Sound.Button.Back);
		gameOverPopup.Close();
		GamePlayManager.instance.closeGameOver = true;
	}

	public void OnHomeBtn()
	{
		if (!GamePlayManager.instance.isGameOver || GamePlayManager.instance.closeGameOver)
		{
			Sound.instance.PlayButton();
			SceneManager.LoadScene("HomeScene");
		}
	}

	public void OnVideoRewarded()
	{
		int rewardedVideoAmount = GameConfig.instance.rewardedVideoAmount;
		GameManager.Coin += rewardedVideoAmount;
		UpdateUI();
		Toast.instance.ShowMessage($"You got {rewardedVideoAmount} free coins");
	}

	public void OnHintBtn()
	{
		if (!GamePlayManager.instance.isGameOver)
		{
			Sound.instance.PlayButton();
			if (GameManager.Coin < GameConfig.instance.numCoinForHint)
			{
				Toast.instance.ShowMessage("You don't have enough coins");
				return;
			}
			GameManager.Coin -= GameConfig.instance.numCoinForHint;
			UpdateUI();
			GamePlayManager.instance.GiveHint();
		}
	}

	public void OnUndoBtn()
	{
		if (!GamePlayManager.instance.isGameOver)
		{
			Sound.instance.PlayButton();
			GamePlayManager.instance.Undo();
		}
	}

	public void OnRestart()
	{
		if (!GamePlayManager.instance.isGameOver || GamePlayManager.instance.closeGameOver)
		{
			Sound.instance.PlayButton();
			SceneManager.LoadScene("GameScene");
		}
	}

	public void PlayButton()
	{
		Sound.instance.PlayButton();
	}

	public void PlayBackButton()
	{
		Sound.instance.PlayButton(Sound.Button.Back);
	}
}
