/*using Combat;
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

}*/
using Combat;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionListDisplayController : MonoBehaviour
{

    Image[] images;

    private void Start()
    {
        images = GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        List<Character> charList = GameController.instance.playerCharacters;
        int nextCharacter = GameController.instance.nextCharacter;

        int imageIndex = 0;

        if (charList.Count > 0 && nextCharacter < charList.Count && nextCharacter >= 0)
        {
            nextCharacter = (nextCharacter + 1) % charList.Count;
            int characterIndex = nextCharacter;
            for (; imageIndex < images.Length;)
            {
                Sprite characterSprite = charList[characterIndex].GetSprite(); // 获取Sprite的方法
                if (characterSprite != null)
                {
                    images[imageIndex].sprite = characterSprite;
                    images[imageIndex].enabled = true; 
                }
                else
                {
                    images[imageIndex].enabled = false; 
                }
                imageIndex++;
                characterIndex = (characterIndex + 1) % charList.Count;
                if (characterIndex == nextCharacter) break;
            }
        }

        
        for (; imageIndex < images.Length; imageIndex++)
        {
            images[imageIndex].enabled = false;
        }
    }
}