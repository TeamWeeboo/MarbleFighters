using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardController:MonoBehaviour {

	BillBoardController[] children;
	BillBoardController parent;
	[SerializeField] bool noScale;
	private void Start() {
		children=GetComponentsInChildren<BillBoardController>();
		for(var t = transform.parent;t;t=t.parent) {
			if(t.GetComponent<BillBoardController>()!=null) {
				parent=t.GetComponent<BillBoardController>();
				break;
			}
		}
	}

	void Update() {
		transform.rotation=(new Angle(90)-new Angle(MainCameraController.instance.transform.forward)).quaternion3;
		foreach(var i in children) {
			if(i.parent==this)
				i.Update();
		}
		if(noScale) {
			if(transform.lossyScale.x<0) {

				Vector3 newScale = transform.localScale;
				newScale.x=-newScale.x;
				transform.localScale=newScale;
			}
		}
	}
}
