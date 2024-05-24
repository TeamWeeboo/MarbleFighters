using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class IntentionGroup {

		public CharacterIntention owner;
		public HashSet<CharacterIntention> members;


		public virtual void Unbind() {

		}

	}
}