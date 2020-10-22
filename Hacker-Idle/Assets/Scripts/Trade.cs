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

	#region Properties
	public float SellAmount => sellAmount;
	public float BuyAmount => buyAmount;

	public FloatResourceCode SellResourceCoede => sellResourceCode;
	public FloatResourceCode BuyResourceCoede => buyResourceCode;
	#endregion
}
