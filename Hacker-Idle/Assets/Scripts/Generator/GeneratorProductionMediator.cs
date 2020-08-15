using System.Collections.Generic;
using UnityEngine;

public class GeneratorProductionMediator : MonoBehaviour
{
	[SerializeField]
	private List<Generator> generators = default;

	private FloatBasedResource resourceDestination;
	
	private void Start()
	{
		resourceDestination = Player.Instance.InformationPurse;

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
