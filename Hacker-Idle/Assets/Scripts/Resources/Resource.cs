using System;
using UnityEngine;

public class Resource<T, C> : MonoBehaviour
	where T : struct, IComparable<T>
	where C : Accumulator<T>, new()
{

	[SerializeField]
	private ResourceBaseData baseData = default;

	[SerializeField]
	private T initialAmount = default;

	private C accumulator;

	#region Properties
	public C Accumulator => accumulator;

	public ResourceBaseData BaseData => baseData;
	#endregion

	private void Awake()
	{
		accumulator = new C();

		accumulator.Deposit(initialAmount);
	}
}
