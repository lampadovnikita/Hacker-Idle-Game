using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI currencyAmountUGUI = default;

	private CurrencyAmount currencyAmount;

	private void Start()
	{
		currencyAmount = CurrencyAmount.Instance;
		currencyAmount.OnAmountChanged += OnCurrencyAmountChanged;

		UpdateAmountUI();
	}

	private void OnCurrencyAmountChanged()
	{
		UpdateAmountUI();
	}

	private void UpdateAmountUI()
	{
		currencyAmountUGUI.text = currencyAmount.Amount.ToString();
	}
}
