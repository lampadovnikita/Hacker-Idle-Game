using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmountMultiplierViewController : MonoBehaviour
{
	private static string MULTIPLIER_TEXT_PREFIX = "x";

	public delegate void AmountMultiplierChnaged(object sender, int newMultiplier);
	public event AmountMultiplierChnaged OnAmountMultiplierChanged;

	[SerializeField]
	private AmountMultiplierView amountMultiplierView = default;

	[SerializeField]
	private List<int> amountMultipliers = default;

	private int currentMultiplierIndex;

	public int CurrentMultiplier => amountMultipliers[currentMultiplierIndex];

	private void Awake()
	{
		currentMultiplierIndex = 0;

		if (amountMultipliers.Count == 0)
		{
			Debug.LogError("Empty multipliers list!");
		}
	}

	private void Start()
	{
		amountMultiplierView.OnAmountMultiplierButtonClicked += (object sender) => ChangeAmountMultiplier();
	}

	private void ChangeAmountMultiplier()
	{
		if (currentMultiplierIndex >= (amountMultipliers.Count - 1))
		{
			currentMultiplierIndex = 0;
		}
		else
		{
			currentMultiplierIndex++;
		}

		string amountMultiplierText = MULTIPLIER_TEXT_PREFIX + CurrentMultiplier;
		amountMultiplierView.SetAmountMultiplierText(amountMultiplierText);

		OnAmountMultiplierChanged?.Invoke(this, CurrentMultiplier);
	}
}
