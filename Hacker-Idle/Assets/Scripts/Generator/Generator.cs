using UnityEditor.UIElements;
using UnityEngine;

public class Generator : MonoBehaviour, ISaveable
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

	private float nextUpgradeCost;

	// Time of one production cycle duration
	private float productionCycleTime;

	// Amount of produced units per 1 production cycle
	private float productionAmount;

	private float productionProgressTime;

	private bool isProductionInProgress;

	#region Properties
	public GeneratorBaseData BaseData => baseData;

	public int Level => level;

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

	public object CaptureState()
	{
		return new GeneratorSaveData
		{
			Level = level
		};
	}

	public void RestoreState(object state)
	{
		GeneratorSaveData saveData = (GeneratorSaveData)state;

		level = 0;
		Upgrade(saveData.Level);
	}

	public void Upgrade(int levelsAmount)
	{
		level += levelsAmount;

		nextUpgradeCost *= Mathf.Pow(baseData.UpgradeCostGrowthRate, levelsAmount);

		productionAmount += baseData.BaseProductionAmount * levelsAmount;

		OnUpgraded?.Invoke(this);
	}

	public float GetUpgradeCost(int levelsAmount)
	{
		// This expression follows from the formula for the sum of n elements of the geometric progression.
		// And the sequence of upgrade costs is just a geometric progression
		return nextUpgradeCost * (Mathf.Pow(baseData.UpgradeCostGrowthRate, levelsAmount) - 1) /
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
			nextUpgradeCost = baseData.PurchaseCost * Mathf.Pow(baseData.UpgradeCostGrowthRate, level);
		}
		else
		{
			nextUpgradeCost = baseData.PurchaseCost;
		}
	}

	private void InitializeProductionTime()
	{
		productionCycleTime = baseData.ProductionTime;
	}
}
