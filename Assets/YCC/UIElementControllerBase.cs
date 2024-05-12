using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIElementControllerBase:MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler {

	protected bool isMouseOver { get; private set; }

	protected virtual void Update() {
		if(isMouseOver&&Input.GetMouseButtonUp(0)) OnPointerRelease();
	}

	protected virtual void OnDisable() {
		isMouseOver=false;
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
		isMouseOver=true;
	}
	public virtual void OnPointerExit(PointerEventData eventData) {
		isMouseOver=false;
	}

	protected virtual void OnPointerRelease() { }

	public virtual void OnPointerDown(PointerEventData eventData) { }
}
