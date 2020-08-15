using TMPro;
using UnityEngine;

public class HeaderView : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI flopcoinAmountUGUI = default;

	[SerializeField]
	private TextMeshProUGUI informationAmountUGUI = default;

	public void SetFlopcoinAmountText(string text)
	{
		flopcoinAmountUGUI.text = text;
	}

	public void SetInformationAmountText(string text)
	{
		informationAmountUGUI.text = text;
	}
}
