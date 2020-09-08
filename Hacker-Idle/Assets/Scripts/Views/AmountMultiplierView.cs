using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AmountMultiplierView : MonoBehaviour
{
	public delegate void AmountMultiplierButtonClicked(object sender);
	public event AmountMultiplierButtonClicked OnAmountMultiplierButtonClicked;

	[SerializeField]
	private Button amountMultiplierButton = default;

	[SerializeField]
	private TextMeshProUGUI amountMultiplierUGUI = default;

	private void Awake()
	{
		amountMultiplierButton.onClick.AddListener
		(
			() =>
			{
				OnAmountMultiplierButtonClicked?.Invoke(this);
			}
		);
	}

	public void SetAmountMultiplierText(string text)
	{
		amountMultiplierUGUI.text = text;
	}
}
