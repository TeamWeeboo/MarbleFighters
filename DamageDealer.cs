using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageDealer:MonoBehaviour {
		Collider2D collider;

		[SerializeField] float damageInterval;

		Dictionary<DamageTarget,float> lastDamageTimes = new Dictionary<DamageTarget,float>();

		public Vector2Int damageRange;
		public DamageType damageType;
		public Vector2 relativeKnockback;
		public Angle direction;
		public Character character;



		void Start() {
			collider=GetComponent<Collider2D>();
			character = GetComponentInParent<Character>();
		}
		private void OnEnable() {
			lastDamageTimes.Clear();
		}


		private void OnTriggerStay2D(Collider2D collision) {
			if(!this.isActiveAndEnabled)return;
			for(var t = transform;t!=null;t=t.parent)
				if(collision.transform==t) return;
			DamageTarget target = collision.GetComponent<DamageTarget>();
			if(!target) return;
			if(lastDamageTimes.ContainsKey(target)&&Time.time-lastDamageTimes[target]<damageInterval) return;
			lastDamageTimes[target]=Time.time;

			Character targetCharacter = collision.GetComponent<Character>();
			targetCharacter.atkAd = character.currentAd;
			targetCharacter.atkAp = character.currentAp;
			targetCharacter.atkAgile = character.currentAgile;


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