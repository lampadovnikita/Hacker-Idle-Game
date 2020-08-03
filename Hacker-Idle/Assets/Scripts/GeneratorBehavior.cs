using System.Collections;
using UnityEngine;

public class GeneratorBehavior : MonoBehaviour
{
	public delegate void Produce(float producedNum);
	public event Produce OnProduce;

	[SerializeField]
	private GeneratorData data = default;

	private void Start()
	{
		if (data.IsPurchased() == true)
		{
			StartCoroutine(ProduceLoopCoroutine());
		}
	}

	public void Upgrade()
	{
		data.Level++;

		data.UpgradeCost *= data.UpgradeCostGrowthRate;

		if (data.Level > 1)
		{
			data.ProductionRate += data.BaseProductionRate * data.ProductionMultiplier;
		}
		else if (data.Level == 1)
		{
			StartCoroutine(ProduceLoopCoroutine());
		}
	}

	private IEnumerator ProduceLoopCoroutine()
	{
		while (true)
		{
			yield return new WaitForSeconds(1f);

			Debug.Log(gameObject.name + " produce " + data.ProductionRate + " CU");

			OnProduce?.Invoke(data.ProductionRate);
		}
	}
}
