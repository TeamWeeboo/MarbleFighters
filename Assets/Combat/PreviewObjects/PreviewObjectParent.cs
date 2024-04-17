using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;

namespace Combat {
	public class PreviewObjectParent:MonoBehaviour {
		Dictionary<int,(GameObject, GameObject)> previewObjects = new Dictionary<int,(GameObject, GameObject)>();
		void Update() {

			if(CommandPanelController.instance.isActiveAndEnabled) {
				for(int i = 0;i<CommandPanelController.instance.playerCharacters.Count;i++) {

					if(!CommandPanelController.instance.playerCharacters[i]){
						if(previewObjects.ContainsKey(i)&&previewObjects[i].Item2){
							Destroy(previewObjects[i].Item2);
							previewObjects.Remove(i);
						}
						continue;
					}

					CommandModel command = CommandPanelController.instance.currentCommandSet.moves[i];
					int moveIndex = command.moveIndex;
					Character character = CommandPanelController.instance.playerCharacters[i];

					if(moveIndex<0) {
						//��Ԥ��
						if(previewObjects.ContainsKey(i)) {
							Destroy(previewObjects[i].Item2);
							previewObjects.Remove(i);
						}
					} else {
						//��Ԥ��
						GameObject prefab = character.moveSet.moves[moveIndex].previewObject;
						if(!previewObjects.ContainsKey(i)||previewObjects[i].Item1!=prefab) {
							//��Ҫ����Ԥ��
							if(previewObjects.ContainsKey(i)) {
								Destroy(previewObjects[i].Item2);
							}

							GameObject newPreviewObject = Instantiate(prefab,character.transform.position,command.moveDirection.quaternion3,transform);
							foreach(var element in newPreviewObject.GetComponentsInChildren<PreviewObjectElementController>()) {
								element.velocityVector=character.rigidbody.velocity;
							}
							if(previewObjects.ContainsKey(i)) {
								previewObjects[i]=(prefab, newPreviewObject);
							} else {
								previewObjects.Add(i,(prefab, newPreviewObject));
							}
						}
						previewObjects[i].Item2.transform.rotation=command.moveDirection.quaternion3;
					}

				}
			} else {
				foreach(var i in previewObjects) {
					Destroy(i.Value.Item2);
				}
				previewObjects.Clear();
			}

		}
	}

}