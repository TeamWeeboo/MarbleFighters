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

		private void FixedUpdate() {

			Character target = IntentionGroup.GetTarget(character,hpWeight: 0);

			if(GameController.instance.playerCharacters.Contains(character)) {
				if((target.transform.position-transform.position).magnitude>disengageDistance) {
					GameController.instance.RemoveCharacter(character);
				}
			} else {
				if((target.transform.position-transform.position).magnitude<engageDistance) {
					GameController.instance.AddCharacter(character);
				}
			}

		}

	}

}
