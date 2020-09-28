using UnityEngine;

public class HeaderView : MonoBehaviour
{
	[SerializeField]
	private ResourceView flopcoinView = default;

	[SerializeField]
	private ResourceView informationView = default;

	#region Properties
	public ResourceView FlopcoinView => flopcoinView;

	public ResourceView InformationView => informationView;
	#endregion
}
