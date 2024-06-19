using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Combat {

	public class MovePlayer:MonoBehaviour {

		Animator animator;
		DamageDealer damageDealer;
		new Rigidbody rigidbody;
		Character character;
		DamageTarget damageTarget;

		[field: SerializeField] public Transform weaponRoot { get; private set; }
		[SerializeField] float baseFrictionStrength;
		[SerializeField] float anim_frictionStrength;
		[HideInInspector] public bool isOnIce, isOnMud;
		private float originSpeedChange;
		[SerializeField] QuadSpriteSetter weaponRenderer;

		public bool anim_damaging;
		public float anim_acceleration;

		public bool anim_blocking;
		public float anim_blockStrength;
		public int anim_retaliateIndex=-1;

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
			damageTarget=GetComponent<DamageTarget>();
		}


		private void Update() {
			if(weaponRenderer&&character&&character.moveSet)
				weaponRenderer.targetSprite=character.moveSet.weaponSprite;
			//SetWeaponRootDirection(currentDirection);
		}

		private void FixedUpdate() {
			UpdateMove();
			UpdateFriction();
			UpdateBlock();
		}

		void UpdateBlock() {
			if(anim_blocking) damageTarget.damaging.Add(-1,OnBlock);
			else damageTarget.damaging.Remove(-1,OnBlock);
		}
		void OnBlock(DamageModel damage) {
			damage.knockback=Vector2.zero;
			damage.damageRange.x-=(int)(damage.damageRange.x*anim_blockStrength);
			damage.damageRange.y-=(int)(damage.damageRange.y*anim_blockStrength);
			Transform source = damage.source.transform;
			if(source.GetComponentInParent<Character>()) source=source.GetComponentInParent<Character>().transform;
			else if(source.GetComponentInParent<MovePlayer>()) source=source.GetComponentInParent<Character>().transform;
			Vector3 directon = source.position-transform.position;
			Debug.Log($"{source.position} , {transform.position} , {directon} , {new Angle(directon).degree}");
			//GameController.instance.isPlaying= false;	
			if(anim_retaliateIndex>=0) StartMove(character.moveSet,anim_retaliateIndex,new Angle(directon));
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
			originSpeedChange = speedChange;
			if(isOnMud){
				speedChange *= 1.5f;
			}else speedChange = originSpeedChange;
			if(isOnIce){
				speedChange *= 0.5f;
			}else speedChange = originSpeedChange;

			rigidbody.velocity=Vector3.MoveTowards(rigidbody.velocity,Vector3.zero,speedChange);
		}

	}

}
