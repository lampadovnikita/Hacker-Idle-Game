using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[SerializeField]
	private FloatAccumulator flopcoinAccumulator = default;

	[SerializeField]
	private FloatAccumulator informationAccumulator = default;

	#region Properties
	public FloatAccumulator FlopcoinAccumulator => flopcoinAccumulator;

	public FloatAccumulator InformationAccumulator => informationAccumulator;
	#endregion

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);

		flopcoinAccumulator.Deposit(1000000f);
	}
}
