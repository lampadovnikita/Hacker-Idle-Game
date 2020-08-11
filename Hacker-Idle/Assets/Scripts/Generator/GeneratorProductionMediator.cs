using System.Collections.Generic;
using UnityEngine;

public class GeneratorProductionMediator : MonoBehaviour
{
	[SerializeField]
	private FloatBasedMoney moneyPurse = default;

	[SerializeField]
	private List<Generator> generators = default;

	private void Awake()
	{
		foreach (Generator g in generators)
		{
			g.OnProduced += OnGeneratorProduced;
		}
	}

	private void OnGeneratorProduced(float prodecedAmount)
	{
		moneyPurse.Deposit(prodecedAmount);
	}
}
