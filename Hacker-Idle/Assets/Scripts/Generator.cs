using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public delegate void Produced(float producedNum);
	public event Produced OnProduced;

	public delegate void Upgraded(Generator sender);
	public event Upgraded OnUpgraded;

	[SerializeField]
	private GeneratorBaseData data = default;

	[SerializeField]
	private int level = 0;

	private float upgradeCost;

	private float productionRate;

	#region Properties
	public int Level => level;

	public float UpgradeCost => upgradeCost;

	public float ProductionRate => productionRate;
	#endregion

	private void Start()
	{
		InitializeProductionRate();
		InitializeUpgradeCost();

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

		upgradeCost *= data.UpgradeCostGrowthRate;

		if (level > 1)
		{
			productionRate += data.BaseProductionRate * data.ProductionMultiplier;
		}
		else if (level == 1)
		{
			productionRate = data.BaseProductionRate;
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
			productionRate = data.BaseProductionRate;
		}
		else
		{
			productionRate = data.BaseProductionRate + data.BaseProductionRate * data.ProductionMultiplier * (level - 1);
		}
	}

	private void InitializeUpgradeCost()
	{
		if (IsPurchased() == true)
		{
			upgradeCost = data.PurchaseCost * Mathf.Pow(data.UpgradeCostGrowthRate, level);
		}
		else
		{
			upgradeCost = data.PurchaseCost;
		}
	}
}
