using UnityEngine;

public class HeaderViewController : MonoBehaviour
{
	[SerializeField]
	private HeaderView headerView = default;

	private Resource<float, FloatAccumulator> flopcoin;

	private Resource<float, FloatAccumulator> information;

	private void Start()
	{
		flopcoin = Player.Instance.GetResource(FloatResourceCode.Flopcoin);
		information = Player.Instance.GetResource(FloatResourceCode.Information);

		headerView.FlopcoinView.SetIconSprite(flopcoin.BaseData.Icon);
		headerView.InformationView.SetIconSprite(information.BaseData.Icon);

		flopcoin.Accumulator.OnAmountChanged +=
			(object sender) => UpdateFlopcoinAmountText();

		information.Accumulator.OnAmountChanged +=
			(object sender) => UpdateInformationAmountText();

		UpdateFlopcoinAmountText();
		UpdateInformationAmountText();
	}

	private void UpdateFlopcoinAmountText()
	{
		headerView.FlopcoinView.SetAmountText(FloatAccumulator.ToString(flopcoin.Accumulator.Amount));
	}

	private void UpdateInformationAmountText()
	{
		headerView.InformationView.SetAmountText(FloatAccumulator.ToString(information.Accumulator.Amount));
	}
}
