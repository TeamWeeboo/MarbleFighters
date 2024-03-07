using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class PreviewObjectBaker:MonoBehaviour {
		[SerializeField] MoveSetInfo moveSet;
		[SerializeField] int targetIndex;
		[SerializeField] GameObject characterPrefab;
		[SerializeField] GameObject dotObject;
		MovePlayer movePlayer;
		Transform weaponObject;
		void Start() {
			GameObject target = Instantiate(characterPrefab,transform.position,Quaternion.identity);
			movePlayer=target.GetComponentInChildren<MovePlayer>();
			weaponObject=movePlayer.GetComponentInChildren<DamageDealer>().transform.parent;
			movePlayer.GetComponentReferences();
			movePlayer.StartMove(moveSet,targetIndex,Angle.right);
		}
		void FixedUpdate() {
			if(movePlayer.isMoving) {
				Instantiate(dotObject,movePlayer.transform.position,Quaternion.identity,transform);
				if(movePlayer.anim_damaging) {
					SpriteRenderer spr = Instantiate(dotObject,weaponObject.position,weaponObject.rotation,transform).GetComponent<SpriteRenderer>();
					spr.transform.localScale=weaponObject.localScale;
					spr.sprite=weaponObject.GetComponent<SpriteRenderer>().sprite;
				}
			} else {
				Destroy(this);
				Destroy(movePlayer.gameObject);
			}
		}
	}

}