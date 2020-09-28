using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceView : MonoBehaviour
{
	[SerializeField]
	private Image icon = default;

	[SerializeField]
	private TextMeshProUGUI amountUGUI = default;

	public void SetIconSprite(Sprite sprite)
	{
		icon.sprite = sprite;
	}

	public void SetAmountText(string text)
	{
		amountUGUI.text = text;
	}
}
