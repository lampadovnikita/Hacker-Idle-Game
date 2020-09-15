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
	private AmountMultiplier tradeAmountMultiplier = default;

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	private void Start()
	{
		sellAccumulator = Player.Instance.GetFloatResourceAccumulator(sellResourceCode);
		buyAccumulator = Player.Instance.GetFloatResourceAccumulator(buyResourceCode);

		sellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();

		tradeView.OnTradeButtonClicked += (object sender) => OnTradeButtonPressed();

		tradeAmountMultiplier.OnAmountMultiplierChanged +=
			(object sender) => UpdateTradeViewInfo();

		UpdateTradeViewInfo();
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = sellAccumulator.AttemptWriteOff(sellAmount *
			tradeAmountMultiplier.CurrentMultiplier);

		if (isWrittedOff == true)
		{
			buyAccumulator.Deposit(buyAmount * tradeAmountMultiplier.CurrentMultiplier);
		}
	}

	private void UpdateTradeViewInfo()
	{
		tradeView.SetBuyAmountText((buyAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());
		tradeView.SetSellAmountText((sellAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());

		UpdateTradeButtonInteractability();
	}

	private void UpdateTradeButtonInteractability()
	{
		if (sellAccumulator.Amount >= sellAmount * tradeAmountMultiplier.CurrentMultiplier)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
