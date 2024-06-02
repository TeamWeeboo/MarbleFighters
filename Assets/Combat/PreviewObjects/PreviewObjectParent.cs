using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Combat {
	public class PreviewObjectParent:MonoBehaviour {
		//Dictionary<int,(GameObject, GameObject)> previewObjects = new Dictionary<int,(GameObject, GameObject)>();
		GameObject previewObject;
		GameObject previewObjectPrefab;

		void Update() {

			if(CommandPanelController.instance.isActiveAndEnabled) {
				Character character = CommandPanelController.instance.currentCharacter;

				CommandModel command = CommandPanelController.instance.currentCommand;
				int moveIndex = command.moveIndex;

				if(moveIndex<0) {
					//Œﬁ‘§¿¿
					if(previewObject) {
						Destroy(previewObject);
						previewObject=null;
						previewObjectPrefab=null;
					}
				} else {
					//”–‘§¿¿
					GameObject prefab = character.moveSet.moves[moveIndex].previewObject;
					if(!previewObject||previewObjectPrefab!=prefab) {
						Destroy(previewObject);
						GameObject newPreviewObject = Instantiate(prefab,character.transform.position,command.moveDirection.quaternion3,transform);
						foreach(var element in newPreviewObject.GetComponentsInChildren<PreviewObjectElementController>()) {
							element.velocityVector=character.rigidbody.velocity;
						}
						previewObject=newPreviewObject;
						previewObjectPrefab=prefab;
					}
					previewObject.transform.rotation=(-command.moveDirection).quaternion3;

				}
			} else {
				//œ˙ªŸ‘§¿¿
				if(previewObject) {
					Destroy(previewObject);
					previewObject=null;
					previewObjectPrefab=null;
				}
			}

		}
	}

}