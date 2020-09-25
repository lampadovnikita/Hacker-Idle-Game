using UnityEngine;

public class Trade : MonoBehaviour
{
	[SerializeField]
	private FloatResourceCode sellResourceCode = default;

	[SerializeField]
	private float sellAmount = default;

	[SerializeField]
	private FloatResourceCode buyResourceCode = default;

	[SerializeField]
	private float buyAmount = default;

	private FloatAccumulator sellAccumulator;
	private FloatAccumulator buyAccumulator;

	#region Properties
	public float SellAmount => sellAmount;

	public float BuyAmount => buyAmount;

	public FloatAccumulator SellAccumulator => sellAccumulator;

	public FloatAccumulator BuyAccumulator => buyAccumulator;
	#endregion

	private void Start()
	{
		sellAccumulator = Player.Instance.GetFloatResourceAccumulator(sellResourceCode);
		buyAccumulator = Player.Instance.GetFloatResourceAccumulator(buyResourceCode);
	}
}
