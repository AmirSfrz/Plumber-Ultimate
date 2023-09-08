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
using UnityEngine;

public class DialogController : MonoBehaviour
{
	public static DialogController instance;

	[HideInInspector]
	public Dialog current;

	[HideInInspector]
	public Dialog[] baseDialogs;

	public Action onDialogsOpened;

	public Action onDialogsClosed;

	public Stack<Dialog> dialogs = new Stack<Dialog>();

	public void Awake()
	{
		instance = this;
	}

	public void ShowDialog(int type)
	{
		ShowDialog((DialogType)type, DialogShow.DONT_SHOW_IF_OTHERS_SHOWING);
	}

	public void ShowDialog(DialogType type, DialogShow option = DialogShow.REPLACE_CURRENT)
	{
		Dialog dialog = GetDialog(type);
		ShowDialog(dialog, option);
	}

	public void ShowYesNoDialog(string title, string content, Action onYesListener, Action onNoListenter, DialogShow option = DialogShow.REPLACE_CURRENT)
	{
		YesNoDialog yesNoDialog = (YesNoDialog)GetDialog(DialogType.YesNo);
		if (yesNoDialog.title != null)
		{
			yesNoDialog.title.text = title;
		}
		if (yesNoDialog.message != null)
		{
			yesNoDialog.message.text = content;
		}
		yesNoDialog.onYesClick = onYesListener;
		yesNoDialog.onNoClick = onNoListenter;
		ShowDialog(yesNoDialog, option);
	}

	public void ShowDialog(Dialog dialog, DialogShow option = DialogShow.REPLACE_CURRENT)
	{
		if (current != null)
		{
			switch (option)
			{
			case DialogShow.DONT_SHOW_IF_OTHERS_SHOWING:
				UnityEngine.Object.Destroy(dialog.gameObject);
				return;
			case DialogShow.REPLACE_CURRENT:
				current.Close();
				break;
			case DialogShow.STACK:
				current.Hide();
				break;
			}
		}
		current = dialog;
		if (option != DialogShow.SHOW_PREVIOUS)
		{
			Dialog dialog2 = current;
			dialog2.onDialogOpened = (Action<Dialog>)Delegate.Combine(dialog2.onDialogOpened, new Action<Dialog>(OnOneDialogOpened));
			Dialog dialog3 = current;
			dialog3.onDialogClosed = (Action<Dialog>)Delegate.Combine(dialog3.onDialogClosed, new Action<Dialog>(OnOneDialogClosed));
			dialogs.Push(current);
		}
		current.Show();
		if (onDialogsOpened != null)
		{
			onDialogsOpened();
		}
	}

	public Dialog GetDialog(DialogType type)
	{
		Dialog dialog = baseDialogs[(int)type];
		dialog.dialogType = type;
		return UnityEngine.Object.Instantiate(dialog, base.transform.position, base.transform.rotation);
	}

	public void CloseCurrentDialog()
	{
		if (current != null)
		{
			current.Close();
		}
	}

	public void CloseDialog(DialogType type)
	{
		if (!(current == null) && current.dialogType == type)
		{
			current.Close();
		}
	}

	public bool IsDialogShowing()
	{
		return current != null;
	}

	public bool IsDialogShowing(DialogType type)
	{
		if (current == null)
		{
			return false;
		}
		return current.dialogType == type;
	}

	private void OnOneDialogOpened(Dialog dialog)
	{
	}

	private void OnOneDialogClosed(Dialog dialog)
	{
		if (current == dialog)
		{
			current = null;
			dialogs.Pop();
			if (onDialogsClosed != null && dialogs.Count == 0)
			{
				onDialogsClosed();
			}
			if (dialogs.Count > 0)
			{
				ShowDialog(dialogs.Peek(), DialogShow.SHOW_PREVIOUS);
			}
		}
	}
}
