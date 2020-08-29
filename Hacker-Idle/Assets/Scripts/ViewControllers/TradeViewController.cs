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

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	private void Start()
	{
		sellAccumulator = Player.Instance.GetFloatResourceAccumulator(sellResourceCode);
		buyAccumulator = Player.Instance.GetFloatResourceAccumulator(buyResourceCode);

		sellAccumulator.OnAmountChanged += (object sender) => UpdateTradeButtonInteractability();
		
		tradeView.SetBuyAmountText(buyAmount.ToString());
		tradeView.SetSellAmountText(sellAmount.ToString());

		UpdateTradeButtonInteractability();
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
