using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI sellPriceUGUI = default;

    [SerializeField]
    private TextMeshProUGUI buyPriceUGUI = default;

    [SerializeField]
    private Button tradeButton = default;

    public void SetSellPriceText(string text)
    {
        sellPriceUGUI.text = text;
    }

    public void SetBuyPriceText(string text)
    {
        buyPriceUGUI.text = text;
    }

    public void SetTradeButtonInteractability(bool isInteractable)
    {
        tradeButton.interactable = isInteractable;
    }
}
