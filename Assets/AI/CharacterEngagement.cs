using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class CharacterEngagement:MonoBehaviour {

		public float engageDistance;
		public float disengageDistance;
		public bool donotEngage;
		Character character;

		private void Start() {
			character=GetComponent<Character>();
		}

		private void FixedUpdate() {
			if(donotEngage) return;
			Character target = IntentionGroup.GetTarget(character,hpWeight: 0);

			if(GameController.instance.ContainCharacter(character)) {
				if(!target||(target.transform.position-transform.position).magnitude>disengageDistance) {
					GameController.instance.RemoveCharacter(character);
				}
			} else {
				if(target&&(target.transform.position-transform.position).magnitude<engageDistance) {
					GameController.instance.AddCharacter(character);
				}
			}

		}

		private void OnDestroy() {
			if(GameController.instance.ContainCharacter(character))
				GameController.instance.RemoveCharacter(character);
		}

	}

}
