using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageTarget:MonoBehaviour {

		public int hpMax;
		public int hp;
		new Rigidbody2D rigidbody;

		private void Start() {
			hp=hpMax;
			rigidbody=GetComponentInParent<Rigidbody2D>();
		}

		public void Damage(DamageModel damage) {
			hp-=Random.Range(damage.damageRange.x,damage.damageRange.y+1);
			rigidbody.AddForce(damage.knockback,ForceMode2D.Impulse);
		}

	}
}