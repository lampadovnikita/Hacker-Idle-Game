using TMPro;
using UnityEngine;

public class HeaderView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI primaryMoneyAmountUGUI = default;

	public void SetPrimaryMoneyAmountText(string text)
	{
		primaryMoneyAmountUGUI.text = text;
	}
}
