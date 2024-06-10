using Combat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ActionListDisplayController:MonoBehaviour {

	TextMeshProUGUI[] texts;

	private void Start() {
		texts=GetComponentsInChildren<TextMeshProUGUI>();
	}

	private void Update() {
		List<Character> charList = GameController.instance.playerCharacters;
		int nextCharacter = GameController.instance.nextCharacter;

		int textIndex = 0;

		if(charList.Count>0&&nextCharacter<charList.Count&&nextCharacter>=0) {
			nextCharacter--;
			if(nextCharacter<0) nextCharacter=charList.Count-1;
			int characterIndex = nextCharacter;
			for(;textIndex<texts.Length;) {
				texts[textIndex].text=charList[characterIndex].gameObject.name;
				characterIndex++;
				textIndex++;
				if(characterIndex>=charList.Count) characterIndex=0;
				if(characterIndex==nextCharacter) break;
			}
		}
		for(;textIndex<texts.Length;textIndex++) {
			texts[textIndex].text="";
		}

	}

}
