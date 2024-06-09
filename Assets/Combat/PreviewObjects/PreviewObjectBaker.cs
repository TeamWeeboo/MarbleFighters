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
		float attackMinDistance = 1000;
		float attackTimeTotal;
		int attackFrameTotal;

		void Start() {
			//Time.timeScale=0.1f;
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
					dot.transform.localScale=Vector3.one*0.25f;
					dot.transform.rotation=Quaternion.Euler(90,0,0);
					dot.GetComponent<PreviewObjectElementController>().thisTime=timeSinceStart;
					dot.GetComponent<SpriteRenderer>().color=newColor;
					moveDistance=dot.transform.localPosition.magnitude;
				}

				if(movePlayer.anim_damaging) {

					GameObject weaponParent = Instantiate(boxObject,weaponObject.position,weaponObject.rotation);
					weaponParent.transform.localScale=weaponObject.lossyScale;
					weaponParent.transform.parent=transform;

					MeshRenderer mesh =weaponParent.GetComponentInChildren<MeshRenderer>();
					BoxCollider weaponCollider = weaponObject.GetComponent<BoxCollider>();

					attackRange=Mathf.Max(attackRange,weaponCollider.ClosestPoint(Vector3.right*1000000000f).x-movePlayer.transform.position.x);
					attackMinDistance=Mathf.Min(attackMinDistance,weaponCollider.ClosestPoint(Vector3.left*1000000000f).x-movePlayer.transform.position.x);
					attackTimeTotal+=timeSinceStart;
					attackFrameTotal++;

					mesh.transform.localScale=weaponCollider.size;
					mesh.transform.localPosition=weaponCollider.center;
					mesh.transform.localRotation=Quaternion.identity;

					mesh.sharedMaterial=hitboxMaterial;
					mesh.material.color=newColor;
					PreviewObjectElementController elementController = weaponParent.GetComponent<PreviewObjectElementController>();
					elementController.material=hitboxMaterial;
					elementController.thisTime=timeSinceStart;
					elementController.color=newColor;

				}
			} else {
				PreviewObjectAdditionalData data = GetComponent<PreviewObjectAdditionalData>();
				data.attackRange=attackRange;
				data.moveDistance=moveDistance;
				data.attackMinDistance=attackMinDistance;
				data.attackTime=attackTimeTotal/attackFrameTotal;
				if(attackFrameTotal==0) data.attackTime=0;
				foreach(var i in GetComponentsInChildren<PreviewObjectElementController>()) {
					i.enabled=true;
				}

				Destroy(this);
				Destroy(movePlayer.gameObject);
			}
		}
	}

}