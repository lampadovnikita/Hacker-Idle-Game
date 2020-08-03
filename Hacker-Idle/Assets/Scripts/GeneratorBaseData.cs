using System;
using UnityEngine;

[CreateAssetMenu]
public class GeneratorBaseData : ScriptableObject
{
	private static String NEGATIVE_VALUE_EXCEPTION_MESSAGE = "The value must be non-negative!";

	[SerializeField]
	private float purchaseCost;

	[SerializeField]
	private float upgradeCostGrowthRate;

	[SerializeField]
	private float baseProductionRate;

	[SerializeField]
	private float productionMultiplier;

	#region Properties

	public float PurchaseCost
	{
		get => purchaseCost;
	}

	public float UpgradeCostGrowthRate
	{
		get => upgradeCostGrowthRate;
	}

	public float ProductionMultiplier
	{
		get => productionMultiplier;
	}

	public float BaseProductionRate
	{
		get => baseProductionRate;
	}

	#endregion

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref purchaseCost);
		Validator.ValidateNonNegative(ref upgradeCostGrowthRate);
		Validator.ValidateNonNegative(ref baseProductionRate);
		Validator.ValidateNonNegative(ref productionMultiplier);
	}
}
