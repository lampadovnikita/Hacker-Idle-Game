using UnityEngine;

// This controller set 
public class ResourceViewController : MonoBehaviour
{
	[SerializeField]
	private ResourceView resourceView = default;

	[SerializeField]
	private FloatResourceCode resourceCode = default;

	private Resource<float, FloatAccumulator> resource;

	private void Start()
	{
		resource = Player.Instance.GetResource(resourceCode);

		resourceView.SetIconSprite(resource.BaseData.Icon);

		resource.Accumulator.OnAmountChanged +=
			(object sender) => resourceView.SetAmountText(resource.Accumulator.Amount.ToString());
	}
}
