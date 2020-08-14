using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class MoneyBaseData : ScriptableObject
{
	[SerializeField]
	private Currency currency = default;

	[SerializeField]
	private Image icon = default;
}
