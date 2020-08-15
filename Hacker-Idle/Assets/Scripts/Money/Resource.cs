﻿using System;
using UnityEngine;

public abstract class Resource<T> : MonoBehaviour where T : struct, IComparable<T>
{
	public delegate void AmountChanged();
	public event AmountChanged OnAmountChanged;

	[SerializeField]
	private ResourceBaseData baseData = default;

	[SerializeField]
	private T initialAmount = default;

	private T amount;

	public T Amount => amount;

	private void Awake()
	{
		amount = initialAmount;
	}

	public bool AttemptWriteOff(T writeOffAmount)
	{
		if (IsNegative(writeOffAmount) == true)
		{
			throw new ArgumentOutOfRangeException(nameof(writeOffAmount), writeOffAmount,
				"AttemptWriteOff must receive a non-negative input value");
		}

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
	public void Deposit(T depositAmount)
	{
		if (IsNegative(depositAmount) == true)
		{
			throw new ArgumentOutOfRangeException(nameof(depositAmount), depositAmount,
				"Deposit must receive a non-negative input value");
		}

		amount = Add(amount, depositAmount);
		OnAmountChanged?.Invoke();
	}

	protected abstract T Add(T lhs, T rhs);

	protected abstract T Subtract(T lhs, T rhs);

	protected abstract bool IsNegative(T val);
}
