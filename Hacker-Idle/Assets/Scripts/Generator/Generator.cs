﻿using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour
{
	public delegate void BeginProduce(Generator sender);
	public event BeginProduce OnBeginProduce;

	public delegate void Produced(float producedAmount);
	public event Produced OnProduced;

	public delegate void Upgraded(Generator sender);
	public event Upgraded OnUpgraded;

	[SerializeField]
	private GeneratorBaseData baseData = default;

	[SerializeField]
	private int level = 0;

	private float upgradeCost;

	// Time of production duration
	private float productionTime;

	// Production per second * duration
	private float productionAmount;

	#region Properties
	public int Level => level;

	public float UpgradeCost => upgradeCost;

	public float ProductionTime => productionTime;

	public float ProductionAmount => productionAmount;
	#endregion

	private void Awake()
	{
		InitializeProductionAmount();
		InitializeUpgradeCost();
		InitializeProductionTime();
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
			productionAmount += baseData.BaseProductionAmount * baseData.ProductionMultiplier;
		}
		else if (level == 1)
		{
			productionAmount = baseData.BaseProductionAmount;

			StartCoroutine(ProduceLoopCoroutine());
		}

		OnUpgraded?.Invoke(this);
	}

	private IEnumerator ProduceLoopCoroutine()
	{
		while (true)
		{
			OnBeginProduce?.Invoke(this);

			yield return new WaitForSeconds(productionTime);

			Debug.Log(gameObject.name + " produce " + productionAmount + " CU");

			OnProduced?.Invoke(productionAmount);
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

	private void InitializeProductionAmount()
	{
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
		productionTime = baseData.ProductionTime;
	}
}
