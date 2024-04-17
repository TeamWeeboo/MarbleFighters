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

	private void Update() {
		camera.transparencySortAxis=new Vector3(transform.forward.x,0,transform.forward.z);
		UnityEngine.Rendering.GraphicsSettings.transparencySortAxis=new Vector3(transform.forward.x,0,transform.forward.z);
		RaycastHit hit;
		Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition),out hit,9999999,LayerMask.GetMask(new string[] { "Terrain" }));
		mouseWorldPosition=hit.point;
	}

}
