using UnityEngine;

public class TradeViewController : MonoBehaviour
{
	[SerializeField]
	private Trade trade = default;

	[SerializeField]
	private TradeView tradeView = default;

	[SerializeField]
	private AmountMultiplier tradeAmountMultiplier = default;

	private Resource<float, FloatAccumulator> sellResource;
	private Resource<float, FloatAccumulator> buyResource;

	private void Start()
	{
		sellResource = Player.Instance.GetResource(trade.SellResourceCoede);
		buyResource = Player.Instance.GetResource(trade.BuyResourceCoede);

		tradeView.SellResourceView.SetIconSprite(sellResource.BaseData.Icon);
		tradeView.BuyResourceView.SetIconSprite(buyResource.BaseData.Icon);

		sellResource.Accumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();

		tradeView.OnTradeButtonClicked += (object sender) => OnTradeButtonPressed();

		tradeAmountMultiplier.OnAmountMultiplierChanged += (object sender) => UpdateTradeViewInfo();

		UpdateTradeViewInfo();
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = sellResource.Accumulator.AttemptWriteOff(trade.SellAmount *
			tradeAmountMultiplier.CurrentMultiplier);

		if (isWrittedOff == true)
		{
			buyResource.Accumulator.Deposit(trade.BuyAmount * tradeAmountMultiplier.CurrentMultiplier);
		}
	}

	private void UpdateTradeViewInfo()
	{
		tradeView.BuyResourceView.
			SetAmountText((trade.BuyAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());
		
		tradeView.SellResourceView.
			SetAmountText((trade.SellAmount * tradeAmountMultiplier.CurrentMultiplier).ToString());

		UpdateTradeButtonInteractability();
	}

	private void UpdateTradeButtonInteractability()
	{
		if (sellResource.Accumulator.Amount >= trade.SellAmount * tradeAmountMultiplier.CurrentMultiplier)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
