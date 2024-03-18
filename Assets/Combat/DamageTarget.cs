using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageTarget:MonoBehaviour {
		new Rigidbody2D rigidbody;

		public delegate void DamageMethod(DamageModel damage);
		public DamageMethod damaging;

		private void Start() {
			rigidbody=GetComponentInParent<Rigidbody2D>();
		}

		public bool damageSuccess;
		public bool Damage(DamageModel damage) {
			damageSuccess=false;
			damaging?.Invoke(damage);
			return damageSuccess;
		}

	}
}