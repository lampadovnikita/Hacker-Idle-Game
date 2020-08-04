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

	[SerializeField]
	private TextMeshProUGUI levelUGUI = default;

	private void Start()
	{
		generator.OnUpgraded += OnGeneratorUpgraded;

		UpdateGeneratorInfoUI();
	}

	public void OnUpgradeButtonPressed()
	{
		generator.Upgrade();
	}

	public void OnGeneratorUpgraded(Generator senderGenerator)
	{
		UpdateGeneratorInfoUI();
	}

	private void UpdateGeneratorInfoUI()
	{
		upgradeCostUGUI.text = generator.UpgradeCost.ToString();
		productionRateUGUI.text = generator.ProductionRate.ToString();
		levelUGUI.text = generator.Level.ToString();
	}
}
