using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UI {
	public class CommandPanelMoveButtomController:MonoBehaviour {
		[SerializeField] int targetMoveIndex;
		[SerializeField] Image borderImage;
		[SerializeField] Image iconImage;
		TextMeshProUGUI text;

		CommandPanelController panel;
		void Start() {
			panel=GetComponentInParent<CommandPanelController>();
			text=GetComponentInChildren<TextMeshProUGUI>();
		}
		void Update() {
			if(panel.currentCharacter==null) {
				text.text="";
				iconImage.sprite=null;
				borderImage.gameObject.SetActive(false);
				return;
			}
			text.text=panel.currentCharacter.moveSet.moves[targetMoveIndex].moveName;
			iconImage.sprite=panel.currentCharacter.moveSet.moves[targetMoveIndex].moveIcon;
			borderImage.gameObject.SetActive(panel.currentCommand.moveIndex==targetMoveIndex);
		}

		public void OnPress() {
			panel.SelectMove(targetMoveIndex);
		}
	}
}