using System.Collections;
using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorView = default;

	[SerializeField]
	private Generator generator = default;

	private FloatAccumulator moneySource;

	private void Start()
	{
		moneySource = Player.Instance.FlopcoinAccumulator;

		// Need to update interactability in both cases,
		// because if update only when the amount of money was changed there possible bug
		// when the money has already been writed off, but the generator still hasn't changed its state.
		// So there still old price to upgrade generator, but actual amount of money may be smaller than 
		// real amount to upgrade
		generator.OnUpgraded += (object sender) => UpdateUpgradeButtonInteractability();
		moneySource.OnAmountChanged += (object sender) => UpdateUpgradeButtonInteractability();

		generatorView.SetProductionProgressMaxValue(generator.ProductionCycleTime);

		UpdateGeneratorViewInfo();
		UpdateUpgradeButtonInteractability();
	}

	private void Update()
	{
		generatorView.SetProductionProgressValue(generator.ProductionProgressTime);
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
		generatorView.SetUpgradeCostText(FloatAccumulator.ToString(generator.UpgradeCost));
		generatorView.SetProductionRateText(FloatAccumulator.ToString(generator.ProductionAmount));
		generatorView.SetLevelText(generator.Level.ToString());
	}

	private void OnGeneratorUpgraded(Generator sender)
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
}
