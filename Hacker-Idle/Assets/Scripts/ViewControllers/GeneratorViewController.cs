using System.Collections;
using UnityEngine;

public class GeneratorViewController : MonoBehaviour
{
	[SerializeField]
	private GeneratorView generatorView = default;

	[SerializeField]
	private Generator generator = default;

	private FloatAccumulator moneySource;

	private float productionProgressTime;

	private void Awake()
	{
		productionProgressTime = 0f;
	}

	private void Start()
	{
		moneySource = Player.Instance.FlopcoinAccumulator;

		generator.OnUpgraded += (object sender) => UpdateUpgradeButtonInteractability();
		generator.OnBeginProduce += (object sender) => RestartProductionProgressVisualization();

		generatorView.SetProductionProgressMaxValue(generator.ProductionTime);

		UpdateGeneratorViewInfo();
		UpdateUpgradeButtonInteractability();
	}

	public void OnUpgradeButtonPressed()
	{
		bool isWritedOff = moneySource.AttemptWriteOff(generator.UpgradeCost);

		if (isWritedOff == true)
		{
			generator.Upgrade();
			UpdateGeneratorViewInfo();
		}
	}

	private void UpdateGeneratorViewInfo()
	{
		generatorView.SetUpgradeCostText(FloatAccumulator.ToString(generator.UpgradeCost));
		generatorView.SetProductionRateText(FloatAccumulator.ToString(generator.ProductionAmount));
		generatorView.SetLevelText(generator.Level.ToString());
	}

	private void OnGeneratorUpgraded(Generator sender)
	{
		UpdateUpgradeButtonInteractability();
	}

	private void UpdateUpgradeButtonInteractability()
	{
		if (HasEnoughMoney() == true)
		{
			generatorView.SetUpgradeButtonInteractability(true);
		}
		else
		{
			generatorView.SetUpgradeButtonInteractability(false);
		}
	}

	private bool HasEnoughMoney()
	{
		if (moneySource.Amount > generator.UpgradeCost)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void RestartProductionProgressVisualization()
	{
		// To prevent errors with multiple adjustment of the progress value
		// if the generator starts a new production cycle earlier than the
		// previous cycle is fully visualized
		StopCoroutine(VisualizeProductionProgressCycle());

		StartCoroutine(VisualizeProductionProgressCycle());
	}

	// Coroutine to visualize 1 cycle of generators production
	private IEnumerator VisualizeProductionProgressCycle()
	{
		productionProgressTime = 0f;
		while (productionProgressTime <= generator.ProductionTime)
		{
			generatorView.SetProductionProgressValue(productionProgressTime);

			yield return null;

			productionProgressTime += Time.deltaTime;
		}

		// At the end reset progress value to 0
		generatorView.SetProductionProgressValue(0f);
	}
}
