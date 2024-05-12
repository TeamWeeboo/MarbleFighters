using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController:MonoBehaviour {

	public static MainCameraController instance;
	Camera camera;
	void Start() {
		instance=this;
		camera=GetComponent<Camera>();
		camera.transparencySortMode=TransparencySortMode.CustomAxis;
		camera.transparencySortAxis=new Vector3(0,0,transform.forward.z);
	}
	public static Vector3 mouseWorldPosition { get; private set; }

	Vector3 lastMousePosition;
	Vector2 lastMouseScreenPosition;

	private void Update() {

		if(Input.GetKey(KeyCode.LeftArrow))
			transform.root.eulerAngles+=new Vector3(0,90*Time.unscaledDeltaTime,0);
		if(Input.GetKey(KeyCode.RightArrow))
			transform.root.eulerAngles+=new Vector3(0,-90*Time.unscaledDeltaTime,0);

		Vector3 mousePosition = ScreenToWorldPosition(Input.mousePosition);
		Vector3 mouseMovement = mousePosition-lastMousePosition;
		lastMousePosition=mousePosition;
		if(Input.GetMouseButton(2)) transform.position-=mouseMovement;
		mousePosition=ScreenToWorldPosition(Input.mousePosition);
		lastMousePosition=mousePosition;

		float turn = -(Input.mousePosition.x-lastMouseScreenPosition.x)/Screen.width*180;
		if(Input.GetMouseButton(1)) transform.root.eulerAngles+=new Vector3(0,turn,0);
		lastMouseScreenPosition=Input.mousePosition;

		camera.transparencySortAxis=new Vector3(transform.forward.x,0,transform.forward.z);
		UnityEngine.Rendering.GraphicsSettings.transparencySortAxis=new Vector3(transform.forward.x,0,transform.forward.z);
		mouseWorldPosition=ScreenToWorldPosition(Input.mousePosition);
	}

	Vector3 ScreenToWorldPosition(Vector2 screenPositon) {
		RaycastHit hit;
		Physics.Raycast(camera.ScreenPointToRay(screenPositon),out hit,9999999,LayerMask.GetMask(new string[] { "Terrain" }));
		return hit.point;
	}

}
