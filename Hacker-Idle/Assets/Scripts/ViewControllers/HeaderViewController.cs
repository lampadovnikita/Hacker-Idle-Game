using UnityEngine;

public class HeaderViewController : MonoBehaviour
{
	[SerializeField]
	private HeaderView headerView = default;

	private FloatAccumulator flopcoinSource;

	private FloatAccumulator informationSource;

	private void Start()
	{
		flopcoinSource = Player.Instance.FlopcoinAccumulator;
		informationSource = Player.Instance.InformationAccumulator;

		flopcoinSource.OnAmountChanged += (object sender) => UpdateFlopcoinAmountText();

		informationSource.OnAmountChanged += (object sender) => UpdateInformationAmountText();

		UpdateFlopcoinAmountText();
		UpdateInformationAmountText();
	}

	private void UpdateFlopcoinAmountText()
	{
		headerView.SetFlopcoinAmountText(FloatAccumulator.ToString(flopcoinSource.Amount));
	}

	private void UpdateInformationAmountText()
	{
		headerView.SetInformationAmountText(FloatAccumulator.ToString(informationSource.Amount));
	}
}
