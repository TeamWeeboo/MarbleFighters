using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript:MonoBehaviour {


	[SerializeField] Combat.MovePlayer toMove;
	[SerializeField] Combat.MoveSetInfo moveset;
	void Start() {
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.P))
			toMove.StartMove(moveset,0,new Angle(20));
		if(Input.GetMouseButtonDown(0)) {
			toMove.StartMove(moveset,0,new Angle(MainCameraController.mouseWorldPosition-toMove.transform.position));
		}
	}
}
