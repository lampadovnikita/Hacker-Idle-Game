﻿using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MoneyBaseData : ScriptableObject
{
	[SerializeField]
	private Currency currency;

	[SerializeField]
	private Image icon;
}
