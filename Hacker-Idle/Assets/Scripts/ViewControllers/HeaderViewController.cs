using UnityEngine;

public class HeaderViewController : MonoBehaviour
{
	[SerializeField]
	private HeaderView headerUI = default;

	[SerializeField]
	private FloatBasedMoney primaryMoneySource = default;

	private void Start()
	{
		primaryMoneySource.OnAmountChanged += OnPrimaryMoneyAmountChanged;

		UpdateAmountUI();
	}

	private void OnPrimaryMoneyAmountChanged()
	{
		UpdateAmountUI();
	}

	private void UpdateAmountUI()
	{
		headerUI.SetPrimaryMoneyAmountText(FloatBasedMoney.ToString(primaryMoneySource.Amount));
	}
}
