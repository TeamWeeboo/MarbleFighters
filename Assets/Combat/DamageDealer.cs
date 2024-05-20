using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageDealer:MonoBehaviour {
		Collider collider;

		[SerializeField] float damageInterval;

		Dictionary<DamageTarget,float> lastDamageTimes = new Dictionary<DamageTarget,float>();

		public Vector2Int damageRange;
		public DamageType damageType;
		public Vector2 relativeKnockback;
		public Angle direction;

		void Start() {
			collider=GetComponent<Collider>();
		}
		private void OnEnable() {
			lastDamageTimes.Clear();
		}

		private void OnTriggerStay(Collider collision) {
			if(!this.isActiveAndEnabled)return;
			for(var t = transform;t!=null;t=t.parent)
				if(collision.transform==t) return;
			DamageTarget target = collision.GetComponent<DamageTarget>();
			if(!target) return;
			if(lastDamageTimes.ContainsKey(target)&&Time.time-lastDamageTimes[target]<damageInterval) return;
			lastDamageTimes[target]=Time.time;

			DamageModel damage = GetDamage();
			target.Damage(damage);
		}

		public DamageModel GetDamage() {
			DamageModel result = new DamageModel();
			result.damageRange=damageRange;
			result.damageType=damageType;
			result.knockback=Utility.Product(relativeKnockback,direction.vector);
			return result;
		}
	}

}