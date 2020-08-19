using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TabGroup : MonoBehaviour
{
	[SerializeField]
	private Sprite idleTabSprite = default;

	[SerializeField]
	private Sprite hoverTabSprite = default;

	[SerializeField]
	private Sprite activeTabSprite = default;

	[SerializeField]
	private List<GameObject> objectsToSwap = default;

	private List<TabButton> tabButtons;

	private TabButton selectedTab;

	private void Awake()
	{
		tabButtons = new List<TabButton>();
	}

	public void Subscribe(TabButton tab)
	{
		tabButtons.Add(tab);

		if (tab.transform.GetSiblingIndex() == 0)
		{
			OnTabSelected(tab);
		}
		else
		{
			tab.BackgroundImage.sprite = idleTabSprite;
		}
	}

	public void OnTabEnter(TabButton tab)
	{
		ResetTabs();

		if (tab != selectedTab)
		{
			tab.BackgroundImage.sprite = hoverTabSprite;
		}
	}

	public void OnTabExit(TabButton tab)
	{
		ResetTabs();
	}

	public void OnTabSelected(TabButton tab)
	{
		if (selectedTab != null)
		{
			selectedTab.Deselect();
		}

		selectedTab = tab;
		selectedTab.Select();

		ResetTabs();

		tab.BackgroundImage.sprite = activeTabSprite;

		int index = tab.transform.GetSiblingIndex();
		for (int i = 0; i < objectsToSwap.Count; i++)
		{
			if (i == index)
			{
				objectsToSwap[i].SetActive(true);
			}
			else
			{
				objectsToSwap[i].SetActive(false);
			}
		}
	}

	private void ResetTabs()
	{
		foreach (TabButton tab in tabButtons)
		{
			if (tab == selectedTab)
			{
				continue;
			}

			tab.BackgroundImage.sprite = idleTabSprite;
		}
	}
}
