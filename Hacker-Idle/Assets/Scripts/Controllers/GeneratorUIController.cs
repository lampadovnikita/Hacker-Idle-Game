﻿using UnityEngine;

public class GeneratorUIController : MonoBehaviour
{
	[SerializeField]
	private GeneratorUI generatorUI = default;

	[SerializeField]
	private Generator generator = default;

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

	private void UpdateGeneratorInfoUI()
	{
		generatorUI.SetUpgradeCostText(generator.UpgradeCost.ToString());
		generatorUI.SetProductionRateText(generator.ProductionRate.ToString());
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
}
