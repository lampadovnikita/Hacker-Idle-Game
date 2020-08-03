using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public delegate void Produce(float producedNum);
	public event Produce OnProduce;

	[SerializeField]
	private GeneratorBaseData data = default;

	[SerializeField]
	private int level = 0;

	private float upgradeCost;

	private float productionRate;

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
	}

	private IEnumerator ProduceLoopCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);

			Debug.Log(gameObject.name + " produce " + productionRate + " CU");

			OnProduce?.Invoke(productionRate);
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
