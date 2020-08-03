using UnityEngine;

[CreateAssetMenu]
public class GeneratorBaseData : ScriptableObject
{
	[SerializeField]
	private float purchaseCost;

	[SerializeField]
	private float upgradeCostGrowthRate;

	[SerializeField]
	private float baseProductionRate;

	[SerializeField]
	private float productionMultiplier;

	#region Properties
	public float PurchaseCost => purchaseCost;

	public float UpgradeCostGrowthRate => upgradeCostGrowthRate;

	public float ProductionMultiplier => productionMultiplier;

	public float BaseProductionRate => baseProductionRate;
	#endregion

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref purchaseCost);
		Validator.ValidateNonNegative(ref upgradeCostGrowthRate);
		Validator.ValidateNonNegative(ref baseProductionRate);
		Validator.ValidateNonNegative(ref productionMultiplier);
	}
}
