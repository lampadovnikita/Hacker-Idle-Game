using UnityEditor.UIElements;
using UnityEngine;

public class Generator : MonoBehaviour
{
	// To see the formulas for calculating the cost of an upgrade
	// and the amount of resources produced see the comments to the methods:
	// InitializeProductionAmount and InitializeUpgradeCost

	public delegate void BeginProduce(object sender);
	public event BeginProduce OnBeginProduce;

	public delegate void FinishProduce(object sender, float producedAmount);
	public event FinishProduce OnFinishProduce;

	public delegate void Upgraded(object sender);
	public event Upgraded OnUpgraded;

	[SerializeField]
	private GeneratorBaseData baseData = default;

	[SerializeField]
	private int level = 0;

	private float upgradeCost;

	// Time of one production cycle duration
	private float productionCycleTime;

	// Amount of produced units per 1 production cycle
	private float productionAmount;

	private float productionProgressTime;

	private bool isProductionInProgress;

	#region Properties
	public GeneratorBaseData BaseData => baseData;

	public int Level => level;

	public float UpgradeCost => upgradeCost;

	public float ProductionCycleTime => productionCycleTime;

	public float ProductionAmount => productionAmount;

	public float ProductionProgressTime => productionProgressTime;
	#endregion

	private void Awake()
	{
		InitializeProductionAmount();
		InitializeUpgradeCost();
		InitializeProductionTime();

		isProductionInProgress = false;
	}

	private void FixedUpdate()
	{
		if (IsPurchased() == true)
		{
			if (isProductionInProgress == true)
			{
				productionProgressTime += Time.deltaTime;
				if (productionProgressTime > productionCycleTime)
				{
					FinishProductionCycle();
					Debug.Log(gameObject.name + " produce " + productionAmount + " CU");
				}
			}
			else
			{
				BeginProductionCycle();
			}
		}
	}

	private void OnValidate()
	{
		Validator.ValidateNonNegative(ref level);
	}

	public void Upgrade(int levelsAmount)
	{
		level += levelsAmount;

		upgradeCost *= Mathf.Pow(baseData.UpgradeCostGrowthRate, levelsAmount);

		productionAmount += baseData.BaseProductionAmount * levelsAmount;

		OnUpgraded?.Invoke(this);
	}

	public void Upgrade()
	{
		level++;

		upgradeCost *= baseData.UpgradeCostGrowthRate;

		if (level > 1)
		{
			productionAmount += baseData.BaseProductionAmount * baseData.ProductionMultiplier;
		}
		else if (level == 1)
		{
			productionAmount = baseData.BaseProductionAmount;
		}

		OnUpgraded?.Invoke(this);
	}

	public float GetUpgradeCost(int levelsAmount)
	{
		return upgradeCost * (Mathf.Pow(baseData.UpgradeCostGrowthRate, levelsAmount) - 1) /
			(baseData.UpgradeCostGrowthRate - 1);
	}

	private void BeginProductionCycle()
	{
		productionProgressTime = 0f;
		isProductionInProgress = true;

		OnBeginProduce?.Invoke(this);
	}

	private void FinishProductionCycle()
	{
		productionProgressTime = productionCycleTime;
		isProductionInProgress = false;

		OnFinishProduce?.Invoke(this, productionAmount);
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

	private void InitializeProductionAmount()
	{
		// productionAmount formula:
		// productionAmount = baseProductionAmount * level * someMultipliers
		// someMultipliers isn't actual variable name, but any multipliers that may occur

		if (level == 1)
		{
			productionAmount = baseData.BaseProductionAmount;
		}
		else
		{
			productionAmount = baseData.BaseProductionAmount +
				baseData.BaseProductionAmount * baseData.ProductionMultiplier * (level - 1);
		}
	}

	private void InitializeUpgradeCost()
	{
		// upgradeCost formula:
		// upgradeCost = purchaseCost * upgradeCostGrowthRate^level

		if (IsPurchased() == true)
		{
			upgradeCost = baseData.PurchaseCost * Mathf.Pow(baseData.UpgradeCostGrowthRate, level);
		}
		else
		{
			upgradeCost = baseData.PurchaseCost;
		}
	}

	private void InitializeProductionTime()
	{
		productionCycleTime = baseData.ProductionTime;
	}
}
