using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat {
	public class PreviewObjectElementController:MonoBehaviour {
		public float thisTime;
		public Vector2 velocityVector;
		Vector2 initPosition;
		public Color color;
		public Material material;
		void Start() {
			if(transform.childCount>0&&transform.GetChild(0).TryGetComponent(out MeshRenderer mesh)) {
				mesh.material=material;
				mesh.material.color=color;
			}
			initPosition=transform.localPosition;
		}
		void Update() {
			transform.localPosition=initPosition;
			transform.position+=(Vector3)velocityVector*thisTime;
		}
	}

}
