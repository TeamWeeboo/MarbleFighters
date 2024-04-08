using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteSetter:MonoBehaviour {

	[SerializeField] CharacterSpriteData data;

	[ContextMenuItem("±£¥Ê","SaveSprites")]
	[ContextMenuItem("∂¡»°","LoadSprites")]
	public bool _button_;

	void Start() {
		LoadSprites();
	}

	public void LoadSprites() {
		if(!data) return;

		Dictionary<int,Sprite> dataDictionary = new Dictionary<int,Sprite>();
		for(int i = 0;i<data.relations.Length;i++) {
			dataDictionary.Add(data.relations[i],data.sprites[i]);
		}
		QuadSpriteSetter[] sprites = GetComponentsInChildren<QuadSpriteSetter>();
		foreach(var sprite in sprites) {
			int relation = GetRelation(gameObject,sprite.gameObject);
			if(dataDictionary.ContainsKey(relation))
				sprite.targetSprite=dataDictionary[relation];
		}
	}
	public void SaveSprites() {
		if(!data) return;
		QuadSpriteSetter[] sprites = GetComponentsInChildren<QuadSpriteSetter>();
		data.relations=new int[sprites.Length];
		data.sprites=new Sprite[sprites.Length];
		int index = 0;
		foreach(var sprite in sprites) {
			data.relations[index]=GetRelation(gameObject,sprite.gameObject);
			data.sprites[index]=sprite.targetSprite;
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
}
