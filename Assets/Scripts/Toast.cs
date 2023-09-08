/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toast : MonoBehaviour
{
	private class AToast
	{
		public string msg;

		public float time;

		public AToast(string msg, float time)
		{
			this.msg = msg;
			this.time = time;
		}
	}

	public RectTransform backgroundTransform;

	public RectTransform messageTransform;

	public static Toast instance;

	[HideInInspector]
	public bool isShowing;

	private Queue<AToast> queue = new Queue<AToast>();

	private void Awake()
	{
		instance = this;
		SetEnabled(enabled: false);
	}

	public void SetMessage(string msg)
	{
		messageTransform.GetComponent<Text>().text = msg;
		Timer.Schedule(this, 0f, delegate
		{
			RectTransform rectTransform = backgroundTransform;
			float x = messageTransform.GetComponent<Text>().preferredWidth + 30f;
			Vector2 sizeDelta = backgroundTransform.sizeDelta;
			rectTransform.sizeDelta = new Vector2(x, sizeDelta.y);
		});
	}

	private void Show(AToast aToast)
	{
		SetMessage(aToast.msg);
		SetEnabled(enabled: true);
		GetComponent<Animator>().SetBool("show", value: true);
		Invoke("Hide", aToast.time);
		isShowing = true;
	}

	public void ShowMessage(string msg, float time = 1.5f)
	{
		AToast item = new AToast(msg, time);
		queue.Enqueue(item);
		ShowOldestToast();
	}

	private void Hide()
	{
		GetComponent<Animator>().SetBool("show", value: false);
		Invoke("CompleteHiding", 1f);
	}

	private void CompleteHiding()
	{
		SetEnabled(enabled: false);
		isShowing = false;
		ShowOldestToast();
	}

	private void ShowOldestToast()
	{
		if (queue.Count != 0 && !isShowing)
		{
			AToast aToast = queue.Dequeue();
			Show(aToast);
		}
	}

	private void SetEnabled(bool enabled)
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				Transform transform = (Transform)enumerator.Current;
				transform.gameObject.SetActive(enabled);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}
}
