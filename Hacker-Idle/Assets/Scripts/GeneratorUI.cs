using TMPro;
using UnityEngine;

public class GeneratorUI : MonoBehaviour
{
	[SerializeField]
	private Generator generator = default;

	[SerializeField]
	private TextMeshProUGUI upgradeCostUGUI = default;

	[SerializeField]
	private TextMeshProUGUI productionRateUGUI = default;

	private void Start()
	{
		generator.OnUpgraded += OnGeneratorUpgraded;	
	}

	public void OnUpgradeButtonPressed()
	{
		generator.Upgrade();
	}

	public void OnGeneratorUpgraded(Generator senderGenerator)
	{
		upgradeCostUGUI.text = senderGenerator.UpgradeCost.ToString();
		productionRateUGUI.text = senderGenerator.ProductionRate.ToString();
	}
}
