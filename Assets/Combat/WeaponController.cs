using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController:MonoBehaviour {

	[SerializeField] Transform targetTransform;
	[SerializeField] Transform targetRootTransform;
	CharacterBodyController body;

	void Start() {
		body=GetComponentInParent<CharacterBodyController>();
	}
	void Update() {

		Vector3 relativePosition = targetRootTransform.InverseTransformPoint(targetTransform.position);// targetTransform.position;
		Vector3 eulers = targetTransform.eulerAngles;
		if(body&&body.isFlipped){
			relativePosition.x=-relativePosition.x;
		}
		//relativePosition.z=relativePosition.y;
		//relativePosition.y=0;
		transform.localPosition=relativePosition;
		Angle weaponAngle = new Angle(targetTransform.rotation);
		//transform.rotation=weaponAngle.quaternion3;
		transform.localRotation=targetTransform.rotation;
	}
}
