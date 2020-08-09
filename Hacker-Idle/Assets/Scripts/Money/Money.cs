using System;
using UnityEngine;

public abstract class Money<T> : MonoBehaviour where T : IComparable<T>
{
	public delegate void AmountChanged();
	public event AmountChanged OnAmountChanged;

	[SerializeField]
	private MoneyBaseData baseData;

	[SerializeField]
	private T initialAmount;

	private T amount;

	public T Amount => amount;

	private void Awake()
	{
		amount = initialAmount;
	}

	public bool AttemptWriteOff(T writeOffAmount)
	{
		if (amount.CompareTo(writeOffAmount) >= 0)
		{
			amount = Subtract(amount, writeOffAmount);
			OnAmountChanged?.Invoke();

			return true;
		}
		else
		{
			return false;
		}
	}

	public void WriteOff(T writeOffAmount)
	{
		amount = Subtract(amount, writeOffAmount);
		OnAmountChanged?.Invoke();
	}

	public void Deposit(T depositAmount)
	{
		amount = Add(amount, depositAmount);
		OnAmountChanged?.Invoke();
	}

	protected abstract T Add(T lhs, T rhs);

	protected abstract T Subtract(T lhs, T rhs);
}
