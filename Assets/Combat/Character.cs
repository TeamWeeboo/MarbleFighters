using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat {
	public class Character:MonoBehaviour {

		public int hpMax;
		[HideInInspector] public int hp;

		public float timeAfterHit{ get; private set; }


		public MoveSetInfo moveSet;
		[HideInInspector] new public Rigidbody rigidbody;
		public MovePlayer movePlayer { get; private set; }
		public DamageTarget damageTarget { get; private set; }
		void Start() {
			rigidbody=GetComponent<Rigidbody>();
			movePlayer=GetComponent<MovePlayer>();
			damageTarget=GetComponent<DamageTarget>();
			GetComponent<DamageTarget>().damaging+=Damage;
			hp=hpMax;
		}
		private void FixedUpdate() {
			if(hp<=0) Die();
			timeAfterHit+=Time.deltaTime;
		}

		public virtual void Die() {
			Destroy(gameObject);
		}

		public bool CanPlayMove(int index) {
			if(movePlayer.isMoving) return false;
			if(rigidbody.velocity.magnitude>moveSet.moves[index].maxInitialSpeed) return false;
			return true;
		}
		public bool HasMove() {
			bool hasMove = false;
			for(int i = 0;i<moveSet.moves.Length;i++) {
				if(CanPlayMove(i)) {
					hasMove=true;
					break;
				}
			}
			return hasMove;
		}

		public void Damage(DamageModel damage) {
			if(hp<=0) {
				damageTarget.damageSuccess=false;
				return;
			}
			timeAfterHit=0;
			damageTarget.damageSuccess=true;
			hp-=Random.Range(damage.damageRange.x,damage.damageRange.y+1);
			rigidbody.AddForce(damage.knockback,ForceMode2D.Impulse);
			Debug.Log(hp);

		}
	}
}
