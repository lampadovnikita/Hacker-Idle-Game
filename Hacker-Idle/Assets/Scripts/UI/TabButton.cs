using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
	public UnityEvent OnTabSelected;
	public UnityEvent OnTabDeselected;
	
	public TabGroup tabGroup;

	private Image backgroundImage;


	public Image BackgroundImage => backgroundImage;

	void Start()
	{
		backgroundImage = GetComponent<Image>();

		tabGroup.Subscribe(this);
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		tabGroup.OnTabSelected(this);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tabGroup.OnTabEnter(this);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tabGroup.OnTabExit(this);
	}

	public void Select()
	{
		OnTabSelected?.Invoke();
	}

	public void Deselect()
	{
		OnTabDeselected?.Invoke();
	}
}
