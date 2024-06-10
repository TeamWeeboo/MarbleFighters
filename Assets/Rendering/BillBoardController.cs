using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoardController:MonoBehaviour {
	void Update() {
		transform.rotation=(new Angle(90)-new Angle(MainCameraController.instance.transform.forward)).quaternion3;
	}
	private void LateUpdate() {
		transform.rotation=(new Angle(90)-new Angle(MainCameraController.instance.transform.forward)).quaternion3;
	}
}
