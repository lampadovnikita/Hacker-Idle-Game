using System.Collections.Generic;
using UnityEngine;

public class CurrencyAmount : MonoBehaviour
{
	public static CurrencyAmount Instance { get; private set; }

	public delegate void AmountChanged();
	public event AmountChanged OnAmountChanged;

	[SerializeField]
	private List<Generator> generators = default;

	private float amount;

	public float Amount => amount;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		foreach (Generator generator in generators)
		{
			generator.OnProduced += OnGeneratorProduced;
		}
	}

	public bool AttemptWriteOff(float amountToWriteOff)
	{
		if (CanWriteOff(amountToWriteOff) == true)
		{
			amount -= amountToWriteOff;
			OnAmountChanged?.Invoke();

			return true;
		}
		else
		{
			return false;
		}
	}

	public bool CanWriteOff(float amountToWriteOff)
	{
		if (amount - amountToWriteOff > 0f)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	private void OnGeneratorProduced(float producedAmount)
	{
		amount += producedAmount;

		OnAmountChanged?.Invoke();
	}
}
