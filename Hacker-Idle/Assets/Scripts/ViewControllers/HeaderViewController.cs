using UnityEngine;

public class HeaderViewController : MonoBehaviour
{
	[SerializeField]
	private HeaderView headerView = default;

	private FloatBasedResource flopcoinSource;

	private FloatBasedResource informationSource;

	private void Start()
	{
		flopcoinSource = Player.Instance.FlopCoinPurse;
		informationSource = Player.Instance.InformationPurse;

		flopcoinSource.OnAmountChanged += UpdateFlopcoinAmountText;

		informationSource.OnAmountChanged += UpdateInformationAmountText;

		UpdateFlopcoinAmountText();
		UpdateInformationAmountText();
	}

	private void UpdateFlopcoinAmountText()
	{
		headerView.SetFlopcoinAmountText(FloatBasedResource.ToString(flopcoinSource.Amount));
	}

	private void UpdateInformationAmountText()
	{
		headerView.SetInformationAmountText(FloatBasedResource.ToString(informationSource.Amount));
	}
}
