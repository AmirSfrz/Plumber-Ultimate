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
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogOverlay : MonoBehaviour
{
	private Image overlay;

	private void Awake()
	{
		overlay = GetComponent<Image>();
	}

	private void Start()
	{
		DialogController instance = DialogController.instance;
		instance.onDialogsOpened = (Action)Delegate.Combine(instance.onDialogsOpened, new Action(OnDialogOpened));
		DialogController instance2 = DialogController.instance;
		instance2.onDialogsClosed = (Action)Delegate.Combine(instance2.onDialogsClosed, new Action(OnDialogClosed));
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	private void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		overlay.enabled = false;
	}

	private void OnDialogOpened()
	{
		overlay.enabled = true;
	}

	private void OnDialogClosed()
	{
		overlay.enabled = false;
	}

	private void OnDestroy()
	{
		DialogController instance = DialogController.instance;
		instance.onDialogsOpened = (Action)Delegate.Remove(instance.onDialogsOpened, new Action(OnDialogOpened));
		DialogController instance2 = DialogController.instance;
		instance2.onDialogsClosed = (Action)Delegate.Remove(instance2.onDialogsClosed, new Action(OnDialogClosed));
	}
}
