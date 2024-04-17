using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageDealer:MonoBehaviour {
		Collider collider;

		public Vector2Int damageRange;
		public DamageType damageType;
		public Vector2 relativeKnockback;
		public Angle direction;

		void Start() {
			collider=GetComponent<Collider>();
		}
		private void OnTriggerEnter(Collider collision) {
			for(var t = transform;t!=null;t=t.parent)
				if(collision.transform==t) return;
			if(!collision.GetComponent<DamageTarget>()) return;

			DamageModel damage = GetDamage();
			DamageTarget target = collision.GetComponent<DamageTarget>();
			target.Damage(damage);
		}

		public DamageModel GetDamage() {
			DamageModel result = new DamageModel();
			result.damageRange=damageRange;
			result.damageType=damageType;
			result.knockback=Utility.Product(relativeKnockback,direction.vector3);
			return result;
		}
	}

}