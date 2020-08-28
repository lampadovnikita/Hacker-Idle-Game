using UnityEngine;

public class TradeViewController : MonoBehaviour
{
    [SerializeField]
    private TradeView tradeView = default;

	[SerializeField]
	private float sellPrice = default;

	[SerializeField]
	private float buyPrice = default;

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	private void Start()
	{
		sellAccumulator = Player.Instance.InformationAccumulator;

		buyAccumulator = Player.Instance.FlopcoinAccumulator;

		sellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();
		
		
		tradeView.SetBuyPriceText(buyPrice.ToString());
		tradeView.SetSellPriceText(sellPrice.ToString());
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = sellAccumulator.AttemptWriteOff(sellPrice);

		if (isWrittedOff)
		{
			buyAccumulator.Deposit(buyPrice);
		}
	}

	private void UpdateTradeButtonInteractability()
	{
		if (sellAccumulator.Amount >= sellPrice)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else 
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
