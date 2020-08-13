using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorView = default;

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

		generatorView.SetProductionProgressMaxValue(generator.ProductionTime);

		UpdateGeneratorViewInfo();
		UpdateUpgradeButtonInteractability();
	}

	private void Update()
	{
		if (generator.IsPurchased() == true)
		{ 
			UpdateProductionProgress();
		}
	}

	public void OnUpgradeButtonPressed()
	{
		bool isWritedOff = moneySource.AttemptWriteOff(generator.UpgradeCost);

		if (isWritedOff == true)
		{
			generator.Upgrade();
			UpdateGeneratorViewInfo();
		}
	}

	private void UpdateGeneratorViewInfo()
	{
		generatorView.SetUpgradeCostText(FloatBasedMoney.ToString(generator.UpgradeCost));
		generatorView.SetProductionRateText(FloatBasedMoney.ToString(generator.ProductionRate));
		generatorView.SetLevelText(generator.Level.ToString());
	}

	private void OnMoneyAmountChanged()
	{
		UpdateUpgradeButtonInteractability();
	}

	private void UpdateUpgradeButtonInteractability()
	{
		if (HasEnoughMoney() == true)
		{
			generatorView.SetUpgradeButtonInteractability(true);
		}
		else
		{
			generatorView.SetUpgradeButtonInteractability(false);
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

		generatorView.SetProductionProgressValue(productionProgressTime);
	}
}
