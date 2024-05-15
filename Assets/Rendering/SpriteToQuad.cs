using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteToQuad:MonoBehaviour {

	[SerializeField] Mesh quadMesh;
	[SerializeField] Material quadMaterial;
	[SerializeField] bool done;

	[ContextMenuItem("นคื๗","Work")]
	public bool _button_;
	private void Start() { if(!done) Work(); }
	public void Work() {
		gameObject.transform.localScale=new Vector3(1,Mathf.Sqrt(2),1);
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
		foreach(var i in sprites) {
			GameObject go = i.gameObject;
			Sprite sprite = i.sprite;
			int order = i.sortingOrder;
			DestroyImmediate(go.GetComponent<SpriteSkin>());
			DestroyImmediate(go.GetComponent<SpriteRenderer>());
			go.AddComponent<MeshFilter>().mesh=quadMesh;
			go.AddComponent<MeshRenderer>().material=quadMaterial;
			go.GetComponent<MeshRenderer>().sortingOrder=0;
			go.AddComponent<QuadSpriteSetter>().targetSprite=sprite;
			go.GetComponent<QuadSpriteSetter>().sortingOrder=order;
		}
		done=true;
	}

}
