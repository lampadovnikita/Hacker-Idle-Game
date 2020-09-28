using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ResourceBaseData : ScriptableObject
{
	[SerializeField]
	private Sprite icon = default;

	public Sprite Icon => icon;
}
