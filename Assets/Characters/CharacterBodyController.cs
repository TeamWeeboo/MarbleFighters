using Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBodyController:MonoBehaviour {

	[SerializeField] CharacterSpriteData spriteFront;
	[SerializeField] CharacterSpriteData spriteBack;
	[SerializeField] Transform handParent;
	[SerializeField] Transform weaponParent;

	MovePlayer movePlayer;
	CharacterSpriteSetter spriteSetter;

	void Start() {
		movePlayer=GetComponent<MovePlayer>();
		spriteSetter=GetComponent<CharacterSpriteSetter>();
	}
	void Update() {
		Angle direction = movePlayer.currentDirection;

		Angle selfAngle = new Angle();
		selfAngle.quaternion3=transform.rotation;
		direction+=selfAngle;
		Debug.Log(direction.degree);
		if(direction.IfBetween(0,180)) spriteSetter.LoadSpritesFromData(spriteBack);
		else spriteSetter.LoadSpritesFromData(spriteFront);

		if(direction.IfBetween(-90,90)!=isFlipped) {
			isFlipped=direction.IfBetween(-90,90);
			transform.localScale=Flip(transform.localScale);
			handParent.localScale=Flip(handParent.localScale);
			weaponParent.localScale=Flip(weaponParent.localScale);
		}

	}
	bool isFlipped=true;
	Vector3 Flip(Vector3 origin) => new Vector3(-origin.x,origin.y,origin.z);
}
