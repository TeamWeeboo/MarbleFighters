using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {
	public enum DamageType {
		Slash,
		Pierce,
		Blunt,
		Burn,
		True
	}
	public class DamageModel:ICloneable {
		public DamageType damageType;
		public Vector2Int damageRange;
		public Vector2 knockback;

		public DamageDealer source;

		public object Clone() {
			DamageModel result = new DamageModel();
			result.damageType=damageType;
			result.damageRange=damageRange;
			result.knockback=knockback;
			return result;
		}
	}

}
