using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class QuadSpriteSetter:MonoBehaviour {

	[SerializeField] Material baseMaterial;
	[SerializeField] int sortingOrder;

	public Sprite targetSprite;
	Material material;
	private void Start() {
		MeshRenderer renderer = GetComponent<MeshRenderer>();
		if(renderer.sharedMaterial!=baseMaterial) renderer.material=baseMaterial;
		material=renderer.material;
	}
	private void Update() {

		if(sortingOrder>0) transform.localPosition+=new Vector3(0,0.001f,-0.001f);
		if(sortingOrder<0) transform.localPosition-=new Vector3(0,0.001f,-0.001f);
		sortingOrder=0;

		if(!targetSprite) return;
		material.mainTexture=targetSprite.texture;
		material.SetVector("_min",targetSprite.textureRect.min);
		material.SetVector("_max",targetSprite.textureRect.max);
		material.SetVector("_fullSize",new Vector2(targetSprite.texture.width,targetSprite.texture.height));
		material.SetVector("_pivot",targetSprite.pivot);
		material.SetFloat("_ppu",targetSprite.pixelsPerUnit);
	}


}
