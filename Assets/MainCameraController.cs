using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController:MonoBehaviour {

	public static MainCameraController instance;
	Camera camera;
	void Start() {
		instance=this;
		camera=GetComponent<Camera>();
		camera.transparencySortMode=TransparencySortMode.Orthographic;
		//camera.transparencySortMode=TransparencySortMode.CustomAxis;
		//camera.transparencySortAxis=new Vector3(0,1,-1);
	}

	public Vector2 mouseWorldPosition => camera.ScreenToWorldPoint(Input.mousePosition);
}
