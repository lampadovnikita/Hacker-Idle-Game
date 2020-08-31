using UnityEngine;

[CreateAssetMenu]
public class GeneratorBaseData : ScriptableObject
{
	[SerializeField]
	private string activityName = "";

	[SerializeField]
	private float purchaseCost = 1f;

	[SerializeField]
	private float upgradeCostGrowthRate = 1f;

	[SerializeField]
	private float baseProductionAmount = 1f; // Units per production cycle

	[SerializeField]
	private float productionTime = 1f; // In seconds

	[SerializeField]
	private float productionMultiplier = 1f;

	#region Properties
	public string ActivityName => activityName;

	public float PurchaseCost => purchaseCost;

	public float UpgradeCostGrowthRate => upgradeCostGrowthRate;

	public float BaseProductionAmount => baseProductionAmount;

	public float ProductionMultiplier => productionMultiplier;

	public float ProductionTime => productionTime;
	#endregion

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref purchaseCost);
		Validator.ValidateNonNegative(ref upgradeCostGrowthRate);
		Validator.ValidateNonNegative(ref baseProductionAmount);
		Validator.ValidateNonNegative(ref productionTime);
		Validator.ValidateNonNegative(ref productionMultiplier);
	}
}
