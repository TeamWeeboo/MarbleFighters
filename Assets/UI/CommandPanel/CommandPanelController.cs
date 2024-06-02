using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;



namespace UI {

	public class CommandPanelController:MonoBehaviour {
		public static CommandPanelController instance;
		void Start() {
			instance=this;
			gameObject.SetActive(false);
		}
		void Update() {

			if(Input.GetKeyDown(KeyCode.Q)) SelectMove(0);
			if(Input.GetKeyDown(KeyCode.W)) SelectMove(1);
			if(Input.GetKeyDown(KeyCode.E)) SelectMove(2);
			if(Input.GetKeyDown(KeyCode.R)) SelectMove(3);
			if(Input.GetKeyDown(KeyCode.A)) SelectMove(MoveSetInfo.moveRunIndex);
			if(Input.GetKeyDown(KeyCode.S)) SelectMove(MoveSetInfo.moveHaltIndex);

			if(Input.GetMouseButtonDown(0)) ConfirmMove();
			//if(Input.GetMouseButtonDown(1)) PreviousCharacter();

			if(currentCharacter!=null) {
				currentCommand.moveDirection=new Angle(MainCameraController.mouseWorldPosition-currentCharacter.transform.position);
				currentCharacter.GetComponent<MovePlayer>().currentDirection=currentCommand.moveDirection;
			}

		}

		public Character currentCharacter;
		public CommandModel currentCommand;
		Dictionary<Character,int> lastMoves = new Dictionary<Character,int>();

		public void EnterCommandMode(Character character) {
			if(!character.HasMove()) return;

			GameController.instance.isPlaying=false;
			currentCommand=new CommandModel();
			currentCharacter=character;

			if(lastMoves.ContainsKey(character)) {
				int lastMove = lastMoves[character];
				if(character.CanPlayMove(lastMove))
					currentCommand.moveIndex=lastMove;
			} else currentCommand.moveIndex=-1;

			gameObject.SetActive(true);
		}


		public bool HasMove() => currentCharacter.HasMove();
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
		public bool ConfirmMove() {
			if(currentCharacter!=null) {
				currentCommand.moveDirection=
				new Angle(MainCameraController.mouseWorldPosition-currentCharacter.transform.position);
			}

			if(currentCommand.moveIndex<0) return false;
			if(!currentCharacter.CanPlayMove(currentCommand.moveIndex)) return false;
			currentCharacter.movePlayer.StartMove(currentCharacter.moveSet,currentCommand.moveIndex,currentCommand.moveDirection);
			GameController.instance.isPlaying=true;

			gameObject.SetActive(false);

			return true;
		}
		/*
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
		*/

	}

}
