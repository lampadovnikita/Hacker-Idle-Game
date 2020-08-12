using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorUI : MonoBehaviour
{
	[SerializeField]
	private Button upgradeButton = default;

	[SerializeField]
	private TextMeshProUGUI upgradeCostUGUI = default;

	[SerializeField]
	private TextMeshProUGUI productionRateUGUI = default;

	[SerializeField]
	private TextMeshProUGUI levelUGUI = default;


	public void SetUpgradeButtonInteractability(bool isInteractable)
	{
		upgradeButton.interactable = isInteractable;
	}

	public void SetUpgradeCostText(string text)
	{
		upgradeCostUGUI.text = text;
	}

	public void SetProductionRateText(string text)
	{
		productionRateUGUI.text = text;
	}

	public void SetLevelText(string text)
	{
		levelUGUI.text = text;
	}
}
