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

	[SerializeField]
	private FloatBasedMoney moneySource = default;

	private void Start()
	{
		moneySource.OnAmountChanged += OnMoneyAmountChanged;

		UpdateGeneratorInfoUI();
		UpdateUpgradeButtonInteractability();
	}

	public void OnUpgradeButtonPressed()
	{
		bool isWritedOff = moneySource.AttemptWriteOff(generator.UpgradeCost);

		if (isWritedOff == true)
		{
			generator.Upgrade();
			UpdateGeneratorInfoUI();
		}
	}

	private void OnMoneyAmountChanged()
	{
		UpdateUpgradeButtonInteractability();
	}

	private void UpdateGeneratorInfoUI()
	{
		upgradeCostUGUI.text = generator.UpgradeCost.ToString();
		productionRateUGUI.text = generator.ProductionRate.ToString();
		levelUGUI.text = generator.Level.ToString();
	}

	private void UpdateUpgradeButtonInteractability()
	{
		if (HasEnoughMoney() == true)
		{
			upgradeButton.interactable = true;
		}
		else
		{
			upgradeButton.interactable = false;
		}
	}

	private bool HasEnoughMoney()
	{
		if (moneySource.Amount > generator.UpgradeCost)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
