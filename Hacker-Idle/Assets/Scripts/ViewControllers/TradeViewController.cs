using UnityEngine;

public class TradeViewController : MonoBehaviour
{
    [SerializeField]
    private TradeView tradeView = default;

	[SerializeField]
	private float sellAmount = default;

	[SerializeField]
	private float buyAmount = default;

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	private void Start()
	{
		sellAccumulator = Player.Instance.InformationAccumulator;

		buyAccumulator = Player.Instance.FlopcoinAccumulator;

		sellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();
		
		
		tradeView.SetBuyAmountText(buyAmount.ToString());
		tradeView.SetSellAmountText(sellAmount.ToString());
	}

	public void OnTradeButtonPressed()
	{
		bool isWrittedOff = sellAccumulator.AttemptWriteOff(sellAmount);

		if (isWrittedOff)
		{
			buyAccumulator.Deposit(buyAmount);
		}
	}

	private void UpdateTradeButtonInteractability()
	{
		if (sellAccumulator.Amount >= sellAmount)
		{
			tradeView.SetTradeButtonInteractability(true);
		}
		else 
		{
			tradeView.SetTradeButtonInteractability(false);
		}
	}
}
