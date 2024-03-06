using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class MovePlayer:MonoBehaviour {

		Animator animator;
		DamageDealer damageDealer;
		new Rigidbody2D rigidbody;

		[SerializeField] Transform weaponRoot;

		[SerializeField] bool anim_damaging;
		[SerializeField] float anim_acceleration;

		MoveSetInfo currentMoveSet;
		MoveInfo currentMove;
		public int tickAfterMove;
		Angle currentDirection;

		public bool isMoving => currentMove!=null&&tickAfterMove<=currentMove.moveDuration;

		void Start() {
			animator=GetComponent<Animator>();
			damageDealer=GetComponentInChildren<DamageDealer>();
			rigidbody=GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate() {
			UpdateMove();
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
			weaponRoot.rotation=currentDirection.quaternion;
			UpdateDamageDealer();
			UpdateRigidBody();
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
			rigidbody.AddForce(currentDirection.vector*anim_acceleration);
		}


	}

}
