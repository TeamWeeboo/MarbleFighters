using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class CharacterDecision:MonoBehaviour {

		protected MovePlayer movePlayer;
		protected Character character;

		public virtual void PlayDecision() {

		}

		void Start() {
			movePlayer=GetComponent<MovePlayer>();
			character=GetComponent<Character>();
		}

	}
}