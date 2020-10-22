using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour
{
	public delegate void TradeButtonClicked(object sender);
	public event TradeButtonClicked OnTradeButtonClicked;

	[SerializeField]
	private ResourceView sellResourceView = default;

	[SerializeField]
	private ResourceView buyResourceView = default;

	[SerializeField]
	private Button tradeButton = default;

	#region Properties
	public ResourceView SellResourceView => sellResourceView;

	public ResourceView BuyResourceView => buyResourceView;
	#endregion

	private void Awake()
	{
		tradeButton.onClick.AddListener
		(
			() => OnTradeButtonClicked?.Invoke(this)
		);
	}

	public void SetTradeButtonInteractability(bool isInteractable)
	{
		tradeButton.interactable = isInteractable;
	}
}
