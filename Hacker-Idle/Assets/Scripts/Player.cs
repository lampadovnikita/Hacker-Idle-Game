using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[SerializeField]
	private Flopcoin flopcoin = default;

	[SerializeField]
	private Information information = default;

	#region Properties
	public FloatAccumulator FlopcoinAccumulator => flopcoin.Accumulator;

	public FloatAccumulator InformationAccumulator => information.Accumulator;
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
	}

	public Resource<float, FloatAccumulator> GetResource(FloatResourceCode code)
	{
		switch (code)
		{
			case FloatResourceCode.Flopcoin:
				return flopcoin;

			case FloatResourceCode.Information:
				return information;

			default:
				Debug.LogError("Unprocessed resource code!");
				return null;
		}
	}
}
