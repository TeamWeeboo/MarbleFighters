using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "自定/角色精灵集")]
public class CharacterSpriteData:ScriptableObject {

	public int[] relations;
	public Sprite[] sprites;

}
