using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public delegate void Produced(float producedNum);
	public event Produced OnProduced;

	public delegate void Upgraded(Generator sender);
	public event Upgraded OnUpgraded;

	[SerializeField]
	private GeneratorBaseData baseData = default;

	[SerializeField]
	private int level = 0;

	private float upgradeCost;

	private float productionRate;

	#region Properties
	public int Level => level;

	public float UpgradeCost => upgradeCost;

	public float ProductionRate => productionRate;
	#endregion

	private void Awake()
	{
		InitializeProductionRate();
		InitializeUpgradeCost();
	}

	private void Start()
	{
		if (IsPurchased() == true)
		{
			StartCoroutine(ProduceLoopCoroutine());
		}
	}

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref level);
	}

	public void Upgrade()
	{
		level++;

		upgradeCost *= baseData.UpgradeCostGrowthRate;

		if (level > 1)
		{
			productionRate += baseData.BaseProductionRate * baseData.ProductionMultiplier;
		}
		else if (level == 1)
		{
			productionRate = baseData.BaseProductionRate;
			StartCoroutine(ProduceLoopCoroutine());
		}

		OnUpgraded?.Invoke(this);
	}

	private IEnumerator ProduceLoopCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);

			Debug.Log(gameObject.name + " produce " + productionRate + " CU");

			OnProduced?.Invoke(productionRate);
		}
	}

	public bool IsPurchased()
	{
		if (level == 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private void InitializeProductionRate()
	{
		if (level == 1)
		{
			productionRate = baseData.BaseProductionRate;
		}
		else
		{
			productionRate = baseData.BaseProductionRate + baseData.BaseProductionRate * baseData.ProductionMultiplier * (level - 1);
		}
	}

	private void InitializeUpgradeCost()
	{
		if (IsPurchased() == true)
		{
			upgradeCost = baseData.PurchaseCost * Mathf.Pow(baseData.UpgradeCostGrowthRate, level);
		}
		else
		{
			upgradeCost = baseData.PurchaseCost;
		}
	}
}
