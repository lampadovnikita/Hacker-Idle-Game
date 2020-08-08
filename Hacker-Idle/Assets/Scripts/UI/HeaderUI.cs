using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI currencyAmountUGUI = default;

	[SerializeField]
	private FloatBasedMoney primaryMoney = default;

	private void Start()
	{
		primaryMoney.OnAmountChanged += OnPrimaryMoneyAmountChanged;

		UpdateAmountUI();
	}

	private void OnPrimaryMoneyAmountChanged()
	{
		UpdateAmountUI();
	}

	private void UpdateAmountUI()
	{
		currencyAmountUGUI.text = primaryMoney.Amount.ToString();
	}
}
