using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player Instance { get; private set; }

	[SerializeField]
	private FloatBasedResource flopCoinPurse = default;

	[SerializeField]
	private FloatBasedResource informationPurse = default;

	#region Properties
	public FloatBasedResource FlopCoinPurse => flopCoinPurse;

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
	}
}
