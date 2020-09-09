using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorView = default;

	[SerializeField]
	private Generator generator = default;

	[SerializeField]
	private AmountMultiplierViewController upgradeAmountMultiplierViewController = default;

	private FloatAccumulator moneySource;

	// Next time you need to update the remainig time
	private float timeToUpdateReaminingTime;

	// Time period between the remaining time updates
	private float remainingTimeUpdatePeriod = 0.1f;

	private void Start()
	{
		moneySource = Player.Instance.FlopcoinAccumulator;

		// Need to update interactability in both cases,
		// because if update only when the amount of money was changed there possible bug
		// when the money has already been writed off, but the generator still hasn't changed its state.
		// So there still old price to upgrade generator, but actual amount of money may be smaller than 
		// real amount to upgrade
		//generator.OnUpgraded += (object sender) => UpdateUpgradeButtonInteractability();
		moneySource.OnAmountChanged += (object sender) => UpdateUpgradeButtonInteractability();

		generator.OnBeginProduce += (object sender) => UpdateRemainingTime();

		upgradeAmountMultiplierViewController.OnAmountMultiplierChanged +=
			(object sender, int newMultiplier) => OnUpgradeMultiplierChanged();

		generatorView.SetProductionProgressMaxValue(generator.ProductionCycleTime);

		generatorView.SetActivityNameTetxt(generator.BaseData.ActivityName);
		UpdateGeneratorViewInfo();
	}

	private void Update()
	{
		if (generator.IsPurchased() == true)
		{
			generatorView.SetProductionProgressValue(generator.ProductionProgressTime);

			if (Time.time > timeToUpdateReaminingTime)
			{
				UpdateRemainingTime();
			}
		}
	}

	public void OnUpgradeButtonPressed()
	{
		int amountOfUpgrades = upgradeAmountMultiplierViewController.CurrentMultiplier;

		bool isWritedOff = moneySource.AttemptWriteOff(generator.GetUpgradeCost(amountOfUpgrades));

		if (isWritedOff == true)
		{
			generator.Upgrade(amountOfUpgrades);

			UpdateGeneratorViewInfo();
		}
	}

	private void OnUpgradeMultiplierChanged()
	{
		UpdateUpgradeCost();
		UpdateUpgradeButtonInteractability();
	}

	private void UpdateGeneratorViewInfo()
	{
		UpdateUpgradeCost();

		generatorView.SetProductionRateText(FloatAccumulator.ToString(generator.ProductionAmount));
		generatorView.SetLevelText(generator.Level.ToString());

		UpdateUpgradeButtonInteractability();
	}

	private void UpdateUpgradeCost()
	{
		generatorView.SetUpgradeCostText(FloatAccumulator.ToString(
			generator.GetUpgradeCost(upgradeAmountMultiplierViewController.CurrentMultiplier)));
	}

	private void UpdateRemainingTime()
	{
		float remainingTime = generator.ProductionCycleTime - generator.ProductionProgressTime;
		generatorView.SetRemainingTimeText(string.Format("{0:F0} {1}", remainingTime, "s"));

		timeToUpdateReaminingTime = Time.time + remainingTimeUpdatePeriod;
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
		if (moneySource.Amount > generator.GetUpgradeCost(upgradeAmountMultiplierViewController.CurrentMultiplier))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
