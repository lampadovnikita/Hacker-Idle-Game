using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorUI = default;

	[SerializeField]
	private Generator generator = default;

	[SerializeField]
	private FloatBasedMoney moneySource = default;

	private float productionProgressTime;

	private void Awake()
	{
		productionProgressTime = 0f;
	}

	private void Start()
	{
		moneySource.OnAmountChanged += OnMoneyAmountChanged;

		generatorUI.SetProductionProgressMaxValue(generator.ProductionTime);

		UpdateGeneratorInfoUI();
		UpdateUpgradeButtonInteractability();
	}

	private void Update()
	{
		UpdateProductionProgress();
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

	private void UpdateGeneratorInfoUI()
	{
		generatorUI.SetUpgradeCostText(FloatBasedMoney.ToString(generator.UpgradeCost));
		generatorUI.SetProductionRateText(FloatBasedMoney.ToString(generator.ProductionRate));
		generatorUI.SetLevelText(generator.Level.ToString());
	}

	private void OnMoneyAmountChanged()
	{
		UpdateUpgradeButtonInteractability();
	}

	private void UpdateUpgradeButtonInteractability()
	{
		if (HasEnoughMoney() == true)
		{
			generatorUI.SetUpgradeButtonInteractability(true);
		}
		else
		{
			generatorUI.SetUpgradeButtonInteractability(false);
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

	private void UpdateProductionProgress()
	{
		productionProgressTime += Time.deltaTime;

		while (productionProgressTime >= generator.ProductionTime)
		{
			productionProgressTime -= generator.ProductionTime;
		}

		generatorUI.SetProductionProgressValue(productionProgressTime);
	}
}
