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
	private AmountMultiplier amountMultiplier = default;

	private void Start()
	{
		amountMultiplierView.OnAmountMultiplierButtonClicked +=
			(object sender) => amountMultiplier.ShiftAmountMultiplier();

		amountMultiplier.OnAmountMultiplierChanged += (object sender) => UpdateAmountMultiplierView();
	}

	private void UpdateAmountMultiplierView()
	{
		string amountMultiplierText = MULTIPLIER_TEXT_PREFIX + amountMultiplier.CurrentMultiplier;
		amountMultiplierView.SetAmountMultiplierText(amountMultiplierText);

		OnAmountMultiplierChanged?.Invoke(this, amountMultiplier.CurrentMultiplier);
	}
}
