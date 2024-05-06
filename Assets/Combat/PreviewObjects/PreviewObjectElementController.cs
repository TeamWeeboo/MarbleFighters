using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat {
	public class PreviewObjectElementController:MonoBehaviour {
		public float thisTime;
		public Vector2 velocityVector;
		Vector2 initPosition;
		void Start() {
			initPosition=transform.localPosition;
		}
		void Update() {
			transform.localPosition=initPosition;
			transform.position+=(Vector3)velocityVector*thisTime;
		}
	}

}
