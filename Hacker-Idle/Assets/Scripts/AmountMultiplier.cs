using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountMultiplier : MonoBehaviour
{
	public delegate void AmountMultiplierChanged(object sender);
	public event AmountMultiplierChanged OnAmountMultiplierChanged;

	[SerializeField]
	private List<int> amountMultipliers = default;

	private int currentMultiplierIndex;

	public int CurrentMultiplier => amountMultipliers[currentMultiplierIndex];

	void Awake()
    {
		currentMultiplierIndex = 0;

		if (amountMultipliers.Count == 0)
		{
			Debug.LogError("Empty multipliers list!");
		}
	}

	public void ShiftAmountMultiplier()
	{
		if (currentMultiplierIndex >= (amountMultipliers.Count - 1))
		{
			currentMultiplierIndex = 0;
		}
		else
		{
			currentMultiplierIndex++;
		}

		OnAmountMultiplierChanged?.Invoke(this);
	}
}
