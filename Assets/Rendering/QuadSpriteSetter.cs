using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadSpriteSetter:MonoBehaviour {

	public Sprite targetSprite;
	Material material;
	private void Start() {
		material=GetComponent<MeshRenderer>().material;
	}
	private void Update() {
		Debug.Log(material.mainTexture);
		material.mainTexture=targetSprite.texture;
		material.SetVector("min",targetSprite.textureRect.min);
		material.SetVector("max",targetSprite.textureRect.max);
		material.SetVector("pivot",targetSprite.pivot);
		material.SetFloat("ppu",targetSprite.pixelsPerUnit);
	}


}
