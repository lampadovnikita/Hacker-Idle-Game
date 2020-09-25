using UnityEngine;

public class TradeViewController : MonoBehaviour
{
	[SerializeField]
	private Trade trade = default;

	[SerializeField]
	private TradeView tradeView = default;

	[SerializeField]
	private AmountMultiplier tradeAmountMultiplier = default;

	private void Start()
	{
		trade.SellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();

		tradeView.OnTradeButtonClicked += (object sender) => OnTradeButtonPressed();

		tradeAmountMultiplier.OnAmountMultiplierChanged +=
			(object sender) => UpdateTradeViewInfo();

		UpdateTradeViewInfo();
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = trade.SellAccumulator.AttemptWriteOff(trade.SellAmount *
			tradeAmountMultiplier.CurrentMultiplier);

		if (isWrittedOff == true)
		{
			trade.BuyAccumulator.Deposit(trade.BuyAmount * tradeAmountMultiplier.CurrentMultiplier);
		}
	}

	private void UpdateTradeViewInfo()
	{
		tradeView.SetBuyAmountText((trade.BuyAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());
		tradeView.SetSellAmountText((trade.SellAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());

		UpdateTradeButtonInteractability();
	}

	private void UpdateTradeButtonInteractability()
	{
		if (trade.SellAccumulator.Amount >= trade.SellAmount * tradeAmountMultiplier.CurrentMultiplier)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
