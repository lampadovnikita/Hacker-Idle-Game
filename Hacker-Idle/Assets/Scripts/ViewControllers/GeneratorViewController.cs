using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorView = default;

	[SerializeField]
	private Generator generator = default;

	private FloatAccumulator moneySource;

	private float timeToUpdateReaminingTime;

	// Update remaining time each second
	private float remainingTimeUpdatePeriod = 1f;

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

		generator.OnBeginProduce += (object sender) => UpdateRemainingTime();

		generatorView.SetProductionProgressMaxValue(generator.ProductionCycleTime);

		generatorView.SetActivityNameTetxt(generator.BaseData.ActivityName);
		UpdateGeneratorViewInfo();
		UpdateUpgradeButtonInteractability();
	}

	private void Update()
	{
		generatorView.SetProductionProgressValue(generator.ProductionProgressTime);
	
		if (Time.time > timeToUpdateReaminingTime)
		{
			UpdateRemainingTime();
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
		generatorView.SetUpgradeCostText(FloatAccumulator.ToString(generator.UpgradeCost));
		generatorView.SetProductionRateText(FloatAccumulator.ToString(generator.ProductionAmount));
		generatorView.SetLevelText(generator.Level.ToString());
	}

	private void UpdateRemainingTime()
	{
		float remainingTime = generator.ProductionCycleTime - generator.ProductionProgressTime;
		generatorView.SetRemainingTimeText(remainingTime.ToString());

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
