using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[SerializeField]
	private FloatBasedResource flopcoinPurse = default;

	[SerializeField]
	private FloatBasedResource informationPurse = default;

	#region Properties
	public FloatBasedResource FlopcoinPurse => flopcoinPurse;

	public FloatBasedResource InformationPurse => informationPurse;
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

		flopcoinPurse.Deposit(1000000f);
	}
}
