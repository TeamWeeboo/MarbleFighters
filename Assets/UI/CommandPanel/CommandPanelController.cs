using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;



namespace UI {

	public class CommandPanelController:MonoBehaviour {
		public static CommandPanelController instance;
		void Start() {
			instance=this;
		}
		void Update() {

			if(Input.GetKeyDown(KeyCode.Q)) SelectMove(0);
			if(Input.GetKeyDown(KeyCode.W)) SelectMove(1);
			if(Input.GetKeyDown(KeyCode.E)) SelectMove(2);
			if(Input.GetKeyDown(KeyCode.R)) SelectMove(3);
			if(Input.GetKeyDown(KeyCode.A)) SelectMove(MoveSetInfo.moveRunIndex);
			if(Input.GetKeyDown(KeyCode.S)) SelectMove(MoveSetInfo.moveHaltIndex);

			if(Input.GetMouseButtonDown(0)) NextCharacter();
			if(Input.GetMouseButtonDown(1)) PreviousCharacter();

			if(currentCharacter!=null) {
				currentCommand.moveDirection=new Angle(MainCameraController.mouseWorldPosition-currentCharacter.transform.position);
				currentCharacter.GetComponent<MovePlayer>().weaponRoot.rotation=currentCommand.moveDirection.quaternion;
			}

		}

		public List<Character> playerCharacters;
		public CommandSetModel currentCommandSet;
		Dictionary<Character,int> lastMoves = new Dictionary<Character,int>();
		public int characterIndex { get; private set; }
		public Character currentCharacter => (characterIndex<0||characterIndex>=playerCharacters.Count) ? null : playerCharacters[characterIndex];
		public CommandModel currentCommand => (characterIndex<0||characterIndex>=playerCharacters.Count) ? null : currentCommandSet.moves[characterIndex];

		int characterToControl;
		public void EnterCommandMode(List<Character> playerCharacters,int characterToControl = -1) {
			this.characterToControl=characterToControl;
			this.playerCharacters=playerCharacters;
			currentCommandSet=new CommandSetModel(playerCharacters.Count);

			for(int i = 0;i<currentCommandSet.moves.Length;i++) {
				if(!playerCharacters[i]) continue;
				if(lastMoves.ContainsKey(playerCharacters[i])) {
					int lastMove = lastMoves[playerCharacters[i]];
					if(playerCharacters[i].CanPlayMove(lastMove))
						currentCommandSet.moves[i].moveIndex=lastMove;
				} else currentCommandSet.moves[i].moveIndex=-1;
			}
			characterIndex=0;
			while(true) {
				if(characterIndex>=playerCharacters.Count) break;
				if(HasMove(characterIndex)) break;
				characterIndex++;
			}

			gameObject.SetActive(true);
		}

		public bool HasMove(int characterIndex) {
			Character target = playerCharacters[characterIndex];
			if(characterIndex!=characterToControl&&characterToControl>=0) return false;
			if(!target) return false;
			return target.HasMove();
		}
		public bool CanSelectMove(int moveIndex) {
			if(!currentCharacter) return false;
			if(!currentCharacter.CanPlayMove(moveIndex)) return false;
			return true;
		}
		public bool SelectMove(int moveIndex) {
			if(!CanSelectMove(moveIndex)) return false;
			currentCommand.moveIndex=moveIndex;
			return true;
		}
		public bool NextCharacter() {
			if(currentCharacter!=null) {
				currentCommand.moveDirection=
				new Angle(MainCameraController.mouseWorldPosition-currentCharacter.transform.position);
			}

			while(true) {
				characterIndex++;
				if(characterIndex>=playerCharacters.Count) break;
				if(HasMove(characterIndex)) break;
			}

			if(characterIndex>playerCharacters.Count) {
				GameController.instance.SetCommand(currentCommandSet);
				gameObject.SetActive(false);
				return true;
			}
			return true;
		}
		public bool PreviousCharacter() {
			if(characterIndex<=0) return false;
			int original = characterIndex;
			while(true) {
				characterIndex--;
				if(characterIndex<=0) break;
				if(HasMove(characterIndex)) break;
			}
			if(characterIndex<=0&&!HasMove(characterIndex)) {
				characterIndex=original;
				return false;
			}
			return true;
		}

	}

}
