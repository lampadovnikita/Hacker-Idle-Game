using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorUI : MonoBehaviour
{
	[SerializeField]
	private Generator generator = default;

	public void OnUpgradeButtonPressed()
	{
		generator.Upgrade();
	}
}
