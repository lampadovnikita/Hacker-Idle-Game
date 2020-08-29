using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI sellAmountUGUI = default;

    [SerializeField]
    private TextMeshProUGUI buyAmountUGUI = default;

    [SerializeField]
    private Button tradeButton = default;

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
