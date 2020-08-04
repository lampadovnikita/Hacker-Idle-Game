using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorUI : MonoBehaviour
{
	[SerializeField]
	private Generator generator = default;

	[SerializeField]
	private Button upgradeButton = default;

	[SerializeField]
	private TextMeshProUGUI upgradeCostUGUI = default;

	[SerializeField]
	private TextMeshProUGUI productionRateUGUI = default;

	[SerializeField]
	private TextMeshProUGUI levelUGUI = default;

	private CurrencyAmount currencyAmount;

	private void Start()
	{
		currencyAmount = CurrencyAmount.Instance;
		currencyAmount.OnAmountChanged += OnCurrencyAmountChanged;

		generator.OnUpgraded += OnGeneratorUpgraded;

		UpdateGeneratorInfoUI();
		UpdateUpgradeButtonIntactability();
	}

	public void OnUpgradeButtonPressed()
	{
		bool isWritedOff = currencyAmount.AttemptWriteOff(generator.UpgradeCost);

		if (isWritedOff == true)
		{
			generator.Upgrade();
		}
	}

	private void OnGeneratorUpgraded(Generator senderGenerator)
	{
		UpdateGeneratorInfoUI();
	}

	private void OnCurrencyAmountChanged()
	{
		UpdateUpgradeButtonIntactability();
	}

	private void UpdateGeneratorInfoUI()
	{
		upgradeCostUGUI.text = generator.UpgradeCost.ToString();
		productionRateUGUI.text = generator.ProductionRate.ToString();
		levelUGUI.text = generator.Level.ToString();
	}

	private void UpdateUpgradeButtonIntactability()
	{
		if (currencyAmount.CanWriteOff(generator.UpgradeCost) == true)
		{
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeButton.interactable = false;
		}
	}
}
