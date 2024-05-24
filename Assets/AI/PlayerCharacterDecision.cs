using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

public class PlayerCharacterDecision:CharacterDecision {

	public override void PlayDecision() {
		UI.CommandPanelController.instance.EnterCommandMode(character);
	}

}
