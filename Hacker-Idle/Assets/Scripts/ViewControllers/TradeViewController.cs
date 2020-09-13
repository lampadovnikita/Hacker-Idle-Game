using UnityEngine;

public class TradeViewController : MonoBehaviour
{
	[SerializeField]
	private TradeView tradeView = default;

	[SerializeField]
	private FloatResourceCode sellResourceCode = default;

	[SerializeField]
	private float sellAmount = default;

	[SerializeField]
	private FloatResourceCode buyResourceCode = default;

	[SerializeField]
	private float buyAmount = default;

	[SerializeField]
	private AmountMultiplierViewController tradeAmountMultiplierViewController = default;

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	private void Start()
	{
		sellAccumulator = Player.Instance.GetFloatResourceAccumulator(sellResourceCode);
		buyAccumulator = Player.Instance.GetFloatResourceAccumulator(buyResourceCode);

		sellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();

		tradeView.OnTradeButtonClicked += (object sender) => OnTradeButtonPressed();

		tradeAmountMultiplierViewController.OnAmountMultiplierChanged +=
			(object sender, int newAmount) => UpdateTradeViewInfo();
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = sellAccumulator.AttemptWriteOff(sellAmount *
			tradeAmountMultiplierViewController.CurrentMultiplier);

		if (isWrittedOff == true)
		{
			buyAccumulator.Deposit(buyAmount * tradeAmountMultiplierViewController.CurrentMultiplier);
		}
	}

	private void UpdateTradeViewInfo()
	{
		tradeView.SetBuyAmountText((buyAmount * tradeAmountMultiplierViewController.CurrentMultiplier).ToString());
		tradeView.SetSellAmountText((sellAmount * tradeAmountMultiplierViewController.CurrentMultiplier).ToString());

		UpdateTradeButtonInteractability();
	}

	private void UpdateTradeButtonInteractability()
	{
		if (sellAccumulator.Amount >= sellAmount * tradeAmountMultiplierViewController.CurrentMultiplier)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
