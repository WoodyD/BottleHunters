using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

	public bool Pressed { get; private set; }
	
	public void OnPointerDown(PointerEventData eventData) {
		Pressed = true;
	}
	
	public void OnPointerUp(PointerEventData eventData) {
		Pressed = false;
	}
}
