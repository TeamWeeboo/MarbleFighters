using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController:MonoBehaviour {

	[SerializeField] Transform targetTransform;
	[SerializeField] Transform targetRootTransform;

	void Start() {
	}
	void Update() {
		Vector3 relativePosition = targetTransform.transform.position-targetRootTransform.position;
		relativePosition.z=relativePosition.y;
		relativePosition.y=0;
		transform.localPosition=relativePosition;
		Angle weaponAngle = new Angle(targetTransform.rotation);
		transform.rotation=weaponAngle.quaternion3;
	}
}
