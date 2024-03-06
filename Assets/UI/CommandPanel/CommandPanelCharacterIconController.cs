using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI {
	public class CommandPanelCharacterIconController:MonoBehaviour {
		[SerializeField] int targetIndex;

		TMPro.TextMeshProUGUI text;
		void Start() {
			text=GetComponent<TMPro.TextMeshProUGUI>();
			text.text=(targetIndex+1).ToString();
		}
		void Update() {
			if(targetIndex>=CommandPanelController.instance.playerCharacters.Count) {
				text.color=Color.clear;
				return;
			}
			if(targetIndex==CommandPanelController.instance.characterIndex) {
				text.color=Color.yellow;
				return;
			}
			if(CommandPanelController.instance.HasMove(targetIndex)) text.color=Color.white;
			else text.color=Color.gray;
		}
	}

}