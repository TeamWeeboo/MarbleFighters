using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteSetter:MonoBehaviour {

	public CharacterSpriteData data;

	[ContextMenuItem("±£¥Ê","SaveSprites")]
	[ContextMenuItem("∂¡»°","LoadSprites")]
	public bool _button_;

	void Start() {
		LoadSprites();
	}

	public void LoadSprites() {
		if(!data) return;

		Dictionary<int,Sprite> dataDictionary = new Dictionary<int,Sprite>();
		Dictionary<int,int> depthDictionary = new Dictionary<int,int>();
		Dictionary<int,Vector3> positionDictionary = new Dictionary<int,Vector3>();
		for(int i = 0;i<data.relations.Length;i++) {
			dataDictionary.Add(data.relations[i],data.sprites[i]);
			positionDictionary.Add(data.relations[i],data.positions[i]);
			depthDictionary.Add(data.relations[i],data.depths[i]);
		}
		QuadSpriteSetter[] sprites = GetComponentsInChildren<QuadSpriteSetter>();
		foreach(var sprite in sprites) {
			int relation = GetRelation(gameObject,sprite.gameObject);
			if(dataDictionary.ContainsKey(relation)) {
				sprite.targetSprite=dataDictionary[relation];
				sprite.transform.localPosition=positionDictionary[relation];
				sprite.sortingOrder=depthDictionary[relation];
			}
		}
		foreach(var sprite in sprites) sprite.UpdateSortingOrder();
	}
	public void SaveSprites() {
		if(!data) return;
		QuadSpriteSetter[] sprites = GetComponentsInChildren<QuadSpriteSetter>();
		data.relations=new int[sprites.Length];
		data.sprites=new Sprite[sprites.Length];
		data.positions=new Vector3[sprites.Length];
		data.depths=new int[sprites.Length];
		int index = 0;
		foreach(var sprite in sprites) {
			data.relations[index]=GetRelation(gameObject,sprite.gameObject);
			data.sprites[index]=sprite.targetSprite;
			data.positions[index]=sprite.transform.localPosition;
			data.depths[index]=sprite.sortingOrder;
			index++;
		}
	}
	public int GetRelation(GameObject root,GameObject leaf) {
		int result = 0;
		for(Transform t = leaf.transform;t!=null;t=t.parent) {
			result++;
			result*=1009;
			result+=Animator.StringToHash(t.name);
			if(t==root.transform) return result;
		}
		return 0;
	}
	public void LoadSpritesFromData(CharacterSpriteData data) {
		this.data=data;
		LoadSprites();
	}
}
