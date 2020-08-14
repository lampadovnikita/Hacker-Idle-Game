﻿using System.Collections;
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

		generator.OnBeginProduce += OnGeneratorBeginProduce;

		generatorView.SetProductionProgressMaxValue(generator.ProductionTime);

		UpdateGeneratorViewInfo();
		UpdateUpgradeButtonInteractability();
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
		generatorView.SetProductionRateText(FloatBasedMoney.ToString(generator.ProductionAmount));
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

	private void OnGeneratorBeginProduce(Generator sender)
	{
		// To prevent errors with multiple adjustment of the progress value
		// if the generator starts a new production cycle earlier than the
		// previous cycle is fully visualized
		StopCoroutine(VisualizeProductionProgressCycle());

		StartCoroutine(VisualizeProductionProgressCycle());
	}

	// Coroutine to visualize 1 cycle of generators production
	private IEnumerator VisualizeProductionProgressCycle()
	{
		productionProgressTime = 0f;
		while (productionProgressTime <= generator.ProductionTime)
		{
			generatorView.SetProductionProgressValue(productionProgressTime);

			yield return null;

			productionProgressTime += Time.deltaTime;
		}

		// At the end reset progress value to 0
		generatorView.SetProductionProgressValue(0f);
	}
}
