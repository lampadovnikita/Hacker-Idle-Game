using TMPro;
using UnityEngine;

public class HeaderUI : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI primaryMoneyAmountUGUI = default;

	public void SetPrimaryMoneyAmountText(string text)
	{
		primaryMoneyAmountUGUI.text = text;
	}
}
