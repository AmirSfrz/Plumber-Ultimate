/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

public class Compliment : MonoBehaviour
{
	public enum Type
	{
		None,
		Amazing,
		Good,
		Welldone
	}

	public Animator anim;

	public SpriteRenderer sRenderer;

	public Sprite[] sprites;

	public static Compliment instance;

	private void Awake()
	{
		instance = this;
	}

	public void Show(Type type)
	{
		if (IsAvailable2Show())
		{
			sRenderer.sprite = sprites[(int)(type - 1)];
			anim.SetTrigger("show");
		}
	}

	public void ShowRandom()
	{
		if (IsAvailable2Show())
		{
			sRenderer.sprite = CUtils.GetRandom(sprites);
			anim.SetTrigger("show");
		}
	}

	private bool IsAvailable2Show()
	{
		return anim.GetCurrentAnimatorStateInfo(0).IsName("Idle");
	}
}
