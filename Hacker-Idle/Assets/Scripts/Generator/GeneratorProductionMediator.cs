using System.Collections.Generic;
using UnityEngine;

public class GeneratorProductionMediator : MonoBehaviour
{

	[SerializeField]
	private List<Generator> generators = default;

	private Player player;
	
	private void Start()
	{
		player = Player.Instance;

		foreach (Generator g in generators)
		{
			g.OnProduced += OnGeneratorProduced;
		}
	}

	private void OnGeneratorProduced(float producedAmount)
	{
		player.FlopCoinPurse.Deposit(producedAmount);
	}
}
