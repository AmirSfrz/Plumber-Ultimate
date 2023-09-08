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
using UnityEngine.EventSystems;

namespace MS
{
	public class PointerEvent : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IEventSystemHandler
	{
		[Header("Pointer Enter")]
		public UnityEvent onEnter;

		[Header("Pointer Hover")]
		public UnityEvent onHover;

		[Header("Pointer Exit")]
		public UnityEvent onExit;

		[Header("Pointer Up Event")]
		public UnityEvent onLeftUp;

		public UnityEvent onRightUp;

		[Header("Pointer Donw Event")]
		public UnityEvent onLeftDown;

		public UnityEvent onRightDown;

		[Header("Pointer Click Event")]
		public UnityEvent onLeftClick;

		public UnityEvent onRightClick;

		private bool isHover;

		public void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				onLeftUp.Invoke();
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				onRightUp.Invoke();
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				onLeftDown.Invoke();
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				onRightDown.Invoke();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button == PointerEventData.InputButton.Left)
			{
				onLeftClick.Invoke();
			}
			if (eventData.button == PointerEventData.InputButton.Right)
			{
				onRightClick.Invoke();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			onEnter.Invoke();
			isHover = true;
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			onExit.Invoke();
			isHover = false;
		}

		private void FixedUpdate()
		{
			if (isHover)
			{
				onHover.Invoke();
			}
		}
	}
}
