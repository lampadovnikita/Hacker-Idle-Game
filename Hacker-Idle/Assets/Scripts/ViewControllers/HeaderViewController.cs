using UnityEngine;

public class HeaderViewController : MonoBehaviour
{
	[SerializeField]
	private HeaderView headerView = default;

	[SerializeField]
	private FloatBasedMoney primaryMoneySource = default;

	private void Start()
	{
		primaryMoneySource.OnAmountChanged += OnPrimaryMoneyAmountChanged;

		UpdateHeaderViewAmount();
	}

	private void OnPrimaryMoneyAmountChanged()
	{
		UpdateHeaderViewAmount();
	}

	private void UpdateHeaderViewAmount()
	{
		headerView.SetPrimaryMoneyAmountText(FloatBasedMoney.ToString(primaryMoneySource.Amount));
	}
}
