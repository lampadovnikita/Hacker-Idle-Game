using UnityEngine;

[CreateAssetMenu]
public class GeneratorBaseData : ScriptableObject
{
	[SerializeField]
	private float purchaseCost = 1f;

	[SerializeField]
	private float upgradeCostGrowthRate = 1f;

	[SerializeField]
	private float baseProductionRate = 1f; // Units per second

	[SerializeField]
	private float productionTime = 1f; // In seconds

	[SerializeField]
	private float productionMultiplier = 1f;

	#region Properties
	public float PurchaseCost => purchaseCost;

	public float UpgradeCostGrowthRate => upgradeCostGrowthRate;

	public float BaseProductionRate => baseProductionRate;

	public float ProductionMultiplier => productionMultiplier;

	public float ProductionTime => productionTime;
	#endregion

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref purchaseCost);
		Validator.ValidateNonNegative(ref upgradeCostGrowthRate);
		Validator.ValidateNonNegative(ref baseProductionRate);
		Validator.ValidateNonNegative(ref productionTime);
		Validator.ValidateNonNegative(ref productionMultiplier);
	}
}
