using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class PreviewObjectBaker:MonoBehaviour {
		[SerializeField] MoveSetInfo moveSet;
		[SerializeField] int targetIndex;
		[SerializeField] GameObject characterPrefab;
		[SerializeField] GameObject dotObject;
		[SerializeField] GameObject boxObject;
		[SerializeField] Material hitboxMaterial;
		MovePlayer movePlayer;
		Transform weaponObject;
		float timeSinceStart;
		int tickSinceStart;

		float moveDistance;
		float attackRange;

		void Start() {
			Time.timeScale=0.1f;
			timeSinceStart=0;
			tickSinceStart=0;
			GameObject target = Instantiate(characterPrefab,transform.position,Quaternion.identity);
			movePlayer=target.GetComponentInChildren<MovePlayer>();
			weaponObject=movePlayer.GetComponentInChildren<DamageDealer>().transform;
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
					moveDistance=dot.transform.localPosition.magnitude;
				}

				if(movePlayer.anim_damaging) {

					GameObject weaponParent = Instantiate(dotObject,weaponObject.position,weaponObject.rotation,transform);
					weaponParent.GetComponent<SpriteRenderer>().color=Color.clear;
					MeshRenderer mesh = Instantiate(boxObject).GetComponent<MeshRenderer>();
					mesh.transform.parent=weaponParent.transform;
					BoxCollider weaponCollider = weaponObject.GetComponent<BoxCollider>();

					attackRange=weaponCollider.ClosestPoint(Vector3.right*1000000000f).x-movePlayer.transform.position.x;

					mesh.transform.localScale=weaponCollider.size;
					mesh.transform.localPosition=weaponCollider.center;
					mesh.transform.localRotation=Quaternion.identity;

					mesh.material.color=newColor;
					mesh.GetComponent<PreviewObjectElementController>().thisTime=timeSinceStart;
				}
			} else {
				GetComponent<PreviewObjectAdditionalData>().attackRange=attackRange;
				GetComponent<PreviewObjectAdditionalData>().moveDistance=moveDistance;
				Destroy(this);
				Destroy(movePlayer.gameObject);
			}
		}
	}

}