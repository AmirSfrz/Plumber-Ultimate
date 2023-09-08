/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
	public Animator anim;

	public AnimationClip hidingAnimation;

	public Text title;

	public Text message;

	public Action<Dialog> onDialogOpened;

	public Action<Dialog> onDialogClosed;

	public Action onDialogCompleteClosed;

	public Action<Dialog> onButtonCloseClicked;

	public DialogType dialogType;

	public bool enableAd = true;

	public bool enableEscape = true;

	private AnimatorStateInfo info;

	private bool isShowing;

	protected virtual void Awake()
	{
		if (anim == null)
		{
			anim = GetComponent<Animator>();
		}
	}

	protected virtual void Start()
	{
		onDialogCompleteClosed = (Action)Delegate.Combine(onDialogCompleteClosed, new Action(OnDialogCompleteClosed));
		GetComponent<Canvas>().worldCamera = Camera.main;
	}

	private void Update()
	{
		if (enableEscape && UnityEngine.Input.GetKeyDown(KeyCode.Escape))
		{
			Close();
		}
	}

	public virtual void Show()
	{
		base.gameObject.SetActive(value: true);
		if (anim != null && IsIdle())
		{
			isShowing = true;
			anim.SetTrigger("show");
			onDialogOpened(this);
		}
		if (enableAd)
		{
			Timer.Schedule(this, 0.3f, delegate
			{
				CUtils.ShowInterstitialAd();
			});
		}
	}

	public virtual void Close()
	{
		if (isShowing)
		{
			isShowing = false;
			if (anim != null && IsIdle() && hidingAnimation != null)
			{
				anim.SetTrigger("hide");
				Timer.Schedule(this, hidingAnimation.length, DoClose);
			}
			else
			{
				DoClose();
			}
			onDialogClosed(this);
		}
	}

	private void DoClose()
	{
		UnityEngine.Object.Destroy(base.gameObject);
		if (onDialogCompleteClosed != null)
		{
			onDialogCompleteClosed();
		}
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		isShowing = false;
	}

	public bool IsIdle()
	{
		info = anim.GetCurrentAnimatorStateInfo(0);
		return info.IsName("Idle");
	}

	public bool IsShowing()
	{
		return isShowing;
	}

	public virtual void OnDialogCompleteClosed()
	{
		onDialogCompleteClosed = (Action)Delegate.Remove(onDialogCompleteClosed, new Action(OnDialogCompleteClosed));
	}

	public void PlayButton()
	{
		Sound.instance.PlayButton();
	}
}
