using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class MovePlayer:MonoBehaviour {

		Animator animator;
		DamageDealer damageDealer;
		new Rigidbody rigidbody;
		Character character;

		[field: SerializeField] public Transform weaponRoot { get; private set; }
		[SerializeField] float baseFrictionStrength;
		[SerializeField] float anim_frictionStrength;
		[SerializeField] QuadSpriteSetter weaponRenderer;

		public bool anim_damaging;
		public float anim_acceleration;

		MoveSetInfo currentMoveSet;
		MoveInfo currentMove;
		public int tickAfterMove;
		public Angle currentDirection;

		public bool isMoving => currentMove!=null&&tickAfterMove<=currentMove.moveDuration;

		void Start() {
			GetComponentReferences();
		}
		public void GetComponentReferences() {
			animator=GetComponent<Animator>();
			damageDealer=GetComponentInChildren<DamageDealer>();
			rigidbody=GetComponent<Rigidbody>();
			character=GetComponent<Character>();
		}


		private void Update() {
			if(weaponRenderer&&character&&character.moveSet)
				weaponRenderer.targetSprite=character.moveSet.weaponSprite;
			//SetWeaponRootDirection(currentDirection);
		}

		private void FixedUpdate() {
			UpdateMove();
			UpdateFriction();
		}

		public void StartMove(MoveSetInfo moveset,int moveIndex,Angle direction) {

			currentMoveSet=moveset;
			currentMove=currentMoveSet.moves[moveIndex];
			currentDirection=direction;
			if(animator.runtimeAnimatorController!=moveset.animations)
				animator.runtimeAnimatorController=moveset.animations;
			animator.SetFloat("moveIndex",moveIndex);
			animator.SetTrigger("startMove");
			tickAfterMove=0;

			if(rigidbody.velocity.magnitude<=currentMove.maxResetSpeed)
				rigidbody.velocity=Vector2.zero;
		}

		void UpdateMove() {
			if(currentMove==null) return;
			tickAfterMove++;
			if(tickAfterMove>currentMove.moveDuration) currentMove=null;
			SetWeaponRootDirection(currentDirection);
			UpdateDamageDealer();
			UpdateRigidBody();
		}



		public void SetWeaponRootDirection(Angle angle) {

			Angle selfAngle = new Angle();
			selfAngle.quaternion3=transform.rotation;
			weaponRoot.localRotation=(angle+selfAngle).quaternion;
		}

		private void UpdateDamageDealer() {
			if(!damageDealer) return;

			if(currentMove==null) {
				damageDealer.enabled=false;
				return;
			}

			damageDealer.enabled=anim_damaging;
			damageDealer.damageRange=currentMove.damageRange;
			damageDealer.damageType=currentMove.damageType;
			damageDealer.relativeKnockback=currentMove.relativeKnockback;
			damageDealer.direction=currentDirection;
		}
		void UpdateRigidBody() {
			if(currentMove==null) return;
			rigidbody.AddForce(currentDirection.vector3*anim_acceleration);
		}

		void UpdateFriction() {
			float speedChange = rigidbody.velocity.magnitude*0.1f+0.1f;
			speedChange*=baseFrictionStrength*(isMoving ? anim_frictionStrength : 1);
			rigidbody.velocity=Vector3.MoveTowards(rigidbody.velocity,Vector3.zero,speedChange);
		}

	}

}
