using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class CharacterEngagement:MonoBehaviour {

		public float engageDistance;
		public float disengageDistance;
		Character character;

		private void Start() {
			character=GetComponent<Character>();
		}
		void Update() {
			
		}
	}

}
