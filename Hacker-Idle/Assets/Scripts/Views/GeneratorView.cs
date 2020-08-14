using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorView : MonoBehaviour
{
	[SerializeField]
	private Button upgradeButton = default;

	[SerializeField]
	private TextMeshProUGUI upgradeCostUGUI = default;

	[SerializeField]
	private TextMeshProUGUI productionAmountUGUI = default;

	[SerializeField]
	private TextMeshProUGUI levelUGUI = default;

	[SerializeField]
	private Slider productionProgressSlider = default;

	public void SetProductionProgressValue(float value)
	{
		productionProgressSlider.value = value;
	}

	public void SetProductionProgressMaxValue(float maxValue)
	{
		productionProgressSlider.maxValue = maxValue;
	}

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
		productionAmountUGUI.text = text;
	}

	public void SetLevelText(string text)
	{
		levelUGUI.text = text;
	}
}
