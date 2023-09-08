/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using MS;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyWheel : MonoBehaviour
{
	public Popup popup;

	public GameObject spinWheel;

	public Button closeBtn;

	public Button spinBtn;

	public Button spinAginBtn;

	public Text spinBtnText;

	public GameObject rewards;

	public Text rewardText;

	public ParticleSystem rewardParticle;

	public List<int> rewardAmount;

	public float spintime = 8f;

	public AnimationCurve spinCurve;

	public RewardedVideoButton rewardedVideoButton;

	private bool isAdShow;

	public static bool isOpenOnce;

	private void Start()
	{
		if (!PlayerPrefs.HasKey("LastSpinDate"))
		{
			GameManager.LastSpin = DateTime.Now;
		}
		spinBtn.onClick.AddListener(OnSpinBtn);
		spinAginBtn.onClick.AddListener(OnSpinAgainBtn);
		if (GameManager.CanSpin && !GameManager.openLevelSelection && !isOpenOnce)
		{
			Invoke("ShowDailyWheel", 0.5f);
		}
		var em = rewardParticle.emission;
		em.enabled = false;
	}

	[ContextMenu("Show")]
	public void ShowDailyWheel()
	{
		isOpenOnce = true;
		UpdateUI();
		popup.Open();
	}

	public void UpdateUI()
	{
		closeBtn.interactable = true;
		spinBtn.gameObject.SetActive(value: true);
		spinAginBtn.gameObject.SetActive(value: false);
		if (GameManager.CanSpin)
		{
			CancelInvoke("UpdateUI");
			spinBtn.interactable = true;
			spinBtnText.text = "Spin";
		}
		else if (isAdShow || !rewardedVideoButton.IsAdAvailable())
		{
			spinBtn.interactable = false;
			spinBtnText.text = "Next " + GameManager.RemandingForSpin.Hours + ":" + GameManager.RemandingForSpin.Minutes;
			InvokeRepeating("UpdateUI", 60f, 60f);
		}
		else
		{
			spinBtn.gameObject.SetActive(value: false);
			spinAginBtn.gameObject.SetActive(value: true);
		}
	}

	public void OnSpinBtn()
	{
		closeBtn.interactable = false;
		spinBtn.interactable = false;
		GameManager.LastSpin = DateTime.Now;
		int index = UnityEngine.Random.Range(0, rewardAmount.Count);
		LeanTween.rotateZ(spinWheel, 2520 + index * (360 / rewardAmount.Count), spintime).setOnComplete((Action)delegate
		{
			GameManager.Coin += rewardAmount[index];
			HomeScene.instance.UpdateUI();
			rewardText.text = rewardAmount[index] + string.Empty;
			LeanTween.scale(rewards, Vector3.one, 1f).setEaseOutQuad().setOnComplete((Action)delegate
			{
				var em = rewardParticle.emission;
				em.enabled = true;
				rewardParticle.Emit(rewardAmount[index]);
				LeanTween.scale(rewards, Vector3.zero, 1f).setEaseInQuad().setDelay(2f);
				UpdateUI();
			});
		}).setEase(spinCurve);
	}

	public void OnSpinAgainBtn()
	{
		rewardedVideoButton.OnClick();
	}

	public void AdsShowComplete()
	{
		spinAginBtn.interactable = false;
		isAdShow = true;
		OnSpinBtn();
	}
}
