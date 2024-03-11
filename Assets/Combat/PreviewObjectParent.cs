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
					CommandModel command = CommandPanelController.instance.currentCommandSet.moves[i];
					int moveIndex = command.moveIndex;
					Character character = CommandPanelController.instance.playerCharacters[i];

					if(moveIndex<0) {
						//无预览
						if(previewObjects.ContainsKey(i)) {
							Destroy(previewObjects[i].Item2);
							previewObjects.Remove(i);
						}
					} else {
						//有预览
						GameObject prefab = character.moveSet.moves[moveIndex].previewObject;
						if(!previewObjects.ContainsKey(i)||previewObjects[i].Item1!=prefab) {
							//需要更新预览
							if(previewObjects.ContainsKey(i)) {
								Destroy(previewObjects[i].Item2);
							}

							GameObject newPreviewObject = Instantiate(prefab,character.transform.position,command.moveDirection.quaternion,transform);
							if(previewObjects.ContainsKey(i)) {
								previewObjects[i]=(prefab, newPreviewObject);
							} else {
								previewObjects.Add(i,(prefab, newPreviewObject));
							}
						}
						previewObjects[i].Item2.transform.rotation=command.moveDirection.quaternion;
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