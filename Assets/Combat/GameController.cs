using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class CommandModel {
		public int moveIndex;
		public Angle moveDirection;
	}
	public class CommandSetModel {
		public CommandModel[] moves;

		public CommandSetModel(int count) {
			moves=new CommandModel[count];
			for(int i = 0;i<count;i++) moves[i]=new CommandModel();
		}
	}

	public class GameController:MonoBehaviour {

		public static GameController instance;
		[SerializeField] int tickPerRound;
		[SerializeField] bool traditionalTurnBased;
		public List<Character> playerCharacters;

		int tickPlayed = 9999;

		bool _isPlaying = true;
		public bool isPlaying {
			get => _isPlaying; private set {
				bool original = _isPlaying;
				_isPlaying=value;
				if(_isPlaying) {
					Time.timeScale=1;
				} else {
					Time.timeScale=0;
					if(original) EnterCommandMode();
				}
			}
		}

		private void Start() {
			UnityEngine.Rendering.GraphicsSettings.transparencySortMode=TransparencySortMode.CustomAxis;
			UnityEngine.Rendering.GraphicsSettings.transparencySortAxis=new Vector3(0,0,1);
			instance =this;
		}

		private void FixedUpdate() {
			tickPlayed++;
			if(tickPlayed>tickPerRound) {
				isPlaying=false;
				tickPlayed=0;
			}
		}

		int nextCharacter;
		void EnterCommandMode() {
			if(traditionalTurnBased) {
				UI.CommandPanelController.instance.EnterCommandMode(playerCharacters,nextCharacter);
				nextCharacter=(nextCharacter+1)%playerCharacters.Count;
			} else UI.CommandPanelController.instance.EnterCommandMode(playerCharacters);
		}
		public void SetCommand(CommandSetModel commands) {
			for(int i = 0;i<playerCharacters.Count&&i<commands.moves.Length;i++) {
				Character currentCharacter = playerCharacters[i];
				if(!currentCharacter) continue;
				CommandModel command = commands.moves[i];
				if(command.moveIndex<0) continue;
				if(!currentCharacter.CanPlayMove(command.moveIndex)) continue;
				currentCharacter.movePlayer.StartMove(currentCharacter.moveSet,command.moveIndex,command.moveDirection);
			}
			isPlaying=true;
		}

		private void Update() {

		}


	}
}