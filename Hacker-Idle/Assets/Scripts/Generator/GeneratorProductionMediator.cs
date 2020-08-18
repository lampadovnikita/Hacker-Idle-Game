using System.Collections.Generic;
using UnityEngine;

public class GeneratorProductionMediator : MonoBehaviour
{
	[SerializeField]
	private List<Generator> generators = default;

	private FloatAccumulator productionDestination;
	
	private void Start()
	{
		productionDestination = Player.Instance.InformationAccumulator;

		foreach (Generator g in generators)
		{
			g.OnProduced += OnGeneratorProduced;
		}
	}

	private void OnGeneratorProduced(float producedAmount)
	{
		productionDestination.Deposit(producedAmount);
	}
}
