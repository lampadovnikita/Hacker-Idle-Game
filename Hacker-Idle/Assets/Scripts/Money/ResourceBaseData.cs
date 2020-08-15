using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class ResourceBaseData : ScriptableObject
{
	[SerializeField]
	private ResourceType resourceType = default;

	[SerializeField]
	private Image icon = default;
}
