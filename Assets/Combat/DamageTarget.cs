using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageTarget:MonoBehaviour {
		new Rigidbody rigidbody;

		public delegate void DamageMethod(DamageModel damage);


		private void Start() {
			rigidbody=GetComponentInParent<Rigidbody>();
		}

		public delegate void DamageDelegate(DamageModel a);
		public readonly SortedEvent<DamageDelegate> damaging=new SortedEvent<DamageDelegate>();

		public bool damageSuccess;
		public bool Damage(DamageModel damage) {
			damageSuccess=false;

			damaging.Invoke(damage);
			return damageSuccess;
		}

	}
}