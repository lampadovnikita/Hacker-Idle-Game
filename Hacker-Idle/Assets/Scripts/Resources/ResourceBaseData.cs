using UnityEngine;

[CreateAssetMenu]
public class ResourceBaseData : ScriptableObject
{
	[SerializeField]
	private Sprite icon = default;

	[SerializeField]
	private Color iconColor = Color.clear;

	#region Properties
	public Sprite Icon => icon;

	public Color IconColor => iconColor;
	#endregion
}
