using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseClickBlockDetecter:UIElementControllerBase {

	public static bool isBlocked {
		get {
			if(!instance||instance.gameObject.activeInHierarchy==false) return false;
			return !isOver||clickedOutside;
		}
	}
	static bool isOver;
	static bool clickedOutside;

	static MouseClickBlockDetecter instance;
	void Start() {
		if(instance) throw new System.Exception("Duplicate Instance");
		instance=this;
	}

	protected override void Update() {
		base.Update();
		isOver=isMouseOver;
		if(!isOver&&Input.GetMouseButtonDown(0)) clickedOutside=true;
		if(Input.GetMouseButtonUp(0)) clickedOutside=false;
	}

}
