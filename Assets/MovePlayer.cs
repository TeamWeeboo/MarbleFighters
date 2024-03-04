using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class MovePlayer:MonoBehaviour {

		Animator animator;
		DamageDealer damageDealer;

		[SerializeField] bool damaging;
		[SerializeField] float acceleration;
		MoveSetInfo currentMoveSet;
		MoveInfo currentMove;

		void Start() {
			animator=GetComponent<Animator>();
			damageDealer=GetComponentInChildren<DamageDealer>();
		}

		private void FixedUpdate() {
			UpdateDamageDealer();

		}

		public void StartMove(MoveSetInfo moveset,int moveIndex) {
			currentMoveSet=moveset;
			if(animator.runtimeAnimatorController!=moveset.animations)
				animator.runtimeAnimatorController=moveset.animations;
		}

		private void UpdateDamageDealer() {
			if(!damageDealer) return;
			damageDealer.enabled=damaging;
			damageDealer.damageRange=currentMove.damageRange;
			damageDealer.damageType=currentMove.damageType;

		}

	}

}
