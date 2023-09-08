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
using UnityEngine.UI;

[ExecuteInEditMode]
public class LevelGroupButton : MonoBehaviour
{
	public LevelGroup levelGroup;

	public Text TitleLbl;

	public Text LevelCompletdLbl;

	public GameObject AwardGoldImage;

	private void Start()
	{
		UpdateUI();
		GetComponent<Button>().onClick.AddListener(delegate
		{
			HomeScene.instance.ShowDetailLevel(this);
			HomeScene.instance.PlayButton();
			GameObject.FindWithTag("LevelGroupPopup").GetComponent<Popup>().Close();
		});
	}

	private void Update()
	{
	}

	[ContextMenu("UpdateUI")]
	public void UpdateUI()
	{
		base.name = levelGroup.LevelGroupName;
		TitleLbl.text = base.name.ToUpper();
		LevelCompletdLbl.text = levelGroup.CompletedLevel + " / " + levelGroup.TotalLevel;
		AwardGoldImage.SetActive(levelGroup.CompletedLevel >= levelGroup.TotalLevel);
	}
}
