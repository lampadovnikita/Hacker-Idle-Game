using System.Collections.Generic;
using UnityEngine;

public class GeneratorProductionMediator : MonoBehaviour
{
	[SerializeField]
	private FloatBasedResource resourceDestination = default;

	[SerializeField]
	private List<Generator> generators = default;

	private void Awake()
	{
		foreach (Generator g in generators)
		{
			g.OnProduced += OnGeneratorProduced;
		}
	}

	private void OnGeneratorProduced(float producedAmount)
	{
		resourceDestination.Deposit(producedAmount);
	}
}
