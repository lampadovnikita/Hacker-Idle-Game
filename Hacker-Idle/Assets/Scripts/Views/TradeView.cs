using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour
{
	public delegate void TradeButtonClicked(object sender);
	public event TradeButtonClicked OnTradeButtonClicked;

	[SerializeField]
	private Image sellResourceIconImage = default;

	[SerializeField]
	private TextMeshProUGUI sellAmountUGUI = default;

	[SerializeField]
	private Image buyResourceIconImage = default;

	[SerializeField]
	private TextMeshProUGUI buyAmountUGUI = default;

	[SerializeField]
	private Button tradeButton = default;

	private void Awake()
	{
		tradeButton.onClick.AddListener
		(
			() => OnTradeButtonClicked?.Invoke(this)
		);
	}

	public void SetSellAmountText(string text)
	{
		sellAmountUGUI.text = text;
	}

	public void SetBuyAmountText(string text)
	{
		buyAmountUGUI.text = text;
	}

	public void SetTradeButtonInteractability(bool isInteractable)
	{
		tradeButton.interactable = isInteractable;
	}
}
