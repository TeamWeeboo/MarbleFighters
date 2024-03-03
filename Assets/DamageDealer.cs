using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public class DamageDealer:MonoBehaviour {
		Collider2D collider;
		void Start() {
			collider=GetComponent<Collider2D>();
		}
		private void OnTriggerEnter2D(Collider2D collision) {
			for(var t = transform;t!=null;t=t.parent)
				if(collision.transform==t) return;
			if(!collision.GetComponent<DamageTarget>()) return;
		}
	}

}