/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using UnityEngine.Events;

namespace MS
{
	public class Popup : MonoBehaviour
	{
		public static Popup current;

		[Header("Anim Setting")]
		public Animator animator;

		public AnimationClip openClip;

		public AnimationClip closeClip;

		[Header("Popup Setting")]
		public bool isOpen;

		public bool closeOnEsc;

		private bool isPlaying;

		public UnityEvent onClose;

		public UnityEvent onOpen;

		private void Start()
		{
			AnimationEvent animationEvent = new AnimationEvent();
			animationEvent.functionName = "onStopAnim";
			animationEvent.time = openClip.length;
			openClip.AddEvent(animationEvent);
			AnimationEvent animationEvent2 = new AnimationEvent();
			animationEvent2.functionName = "onStopAnim";
			animationEvent2.time = openClip.length;
			closeClip.AddEvent(animationEvent2);
			if (isOpen)
			{
				Open();
			}
			else
			{
				Close();
			}
		}

		[ContextMenu("Open")]
		public void Open(bool onEnd = false)
		{
			if (!isPlaying && !isOpen)
			{
				current = this;
				isOpen = true;
				isPlaying = true;
				animator.Play(openClip.name);
				if (onEnd)
				{
					onOpen.Invoke();
				}
			}
		}

		[ContextMenu("Close")]
		public void Close(bool onEnd = false)
		{
			if (!isPlaying && isOpen)
			{
				isPlaying = true;
				animator.Play(closeClip.name);
				isOpen = false;
				if (onEnd)
				{
					onClose.Invoke();
				}
			}
		}

		[ContextMenu("Open")]
		public void Open()
		{
			Open(onEnd: false);
		}

		[ContextMenu("Close")]
		public void Close()
		{
			Close(onEnd: false);
		}

		public void onStopAnim()
		{
			isPlaying = false;
		}
	}
}
