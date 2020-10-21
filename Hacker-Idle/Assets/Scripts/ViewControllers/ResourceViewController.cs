using UnityEngine;

public class ResourceViewController : MonoBehaviour
{
    [SerializeField]
    private ResourceView resourceView = default;

    [SerializeField]
    private FloatResourceCode resourceCode = default;

    private Resource<float, FloatAccumulator> resource;

	void Start()
    {
        resource = Player.Instance.GetResource(resourceCode);

        resource.Accumulator.OnAmountChanged +=
            (object sender) => UpdateResourceAmountText();

        resourceView.SetIconSprite(resource.BaseData.Icon);

        // Initial setup
        UpdateResourceAmountText();
    }

    private void UpdateResourceAmountText()
    {
        resourceView.SetAmountText(FloatAccumulator.ToString(resource.Accumulator.Amount));
    }
}
