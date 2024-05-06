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
		float timeSinceStart;
		int tickSinceStart;
		void Start() {
			timeSinceStart=0;
			tickSinceStart=0;
			GameObject target = Instantiate(characterPrefab,transform.position,Quaternion.identity);
			movePlayer=target.GetComponentInChildren<MovePlayer>();
			weaponObject=movePlayer.GetComponentInChildren<DamageDealer>().transform.parent;
			movePlayer.GetComponentReferences();
			movePlayer.StartMove(moveSet,targetIndex,Angle.right);
		}
		void FixedUpdate() {
			Color newColor = Color.HSVToRGB(timeSinceStart-Mathf.Floor(timeSinceStart),1,1);
			newColor.a=0.25f;

			if(movePlayer.isMoving) {
				timeSinceStart+=Time.deltaTime;
				tickSinceStart++;
				if(tickSinceStart%4==0) {
					GameObject dot = Instantiate(dotObject,movePlayer.transform.position,Quaternion.identity,transform);
					dot.GetComponent<PreviewObjectElementController>().thisTime=timeSinceStart;
					dot.GetComponent<SpriteRenderer>().color=newColor;
				}

				if(movePlayer.anim_damaging) {
					SpriteRenderer spr = Instantiate(dotObject,weaponObject.position,weaponObject.rotation,transform).GetComponent<SpriteRenderer>();
					spr.transform.localScale=weaponObject.localScale;
					spr.sprite=weaponObject.GetComponent<SpriteRenderer>().sprite;
					spr.color=newColor;
					spr.GetComponent<PreviewObjectElementController>().thisTime=timeSinceStart;
				}
			} else {
				Destroy(this);
				Destroy(movePlayer.gameObject);
			}
		}
	}

}