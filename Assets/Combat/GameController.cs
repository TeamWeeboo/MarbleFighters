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
		[SerializeReference] bool noPause;
		public List<Character> playerCharacters;

		public void AddCharacter(Character character){
			if (nextCharacter == 1){
				playerCharacters.Add(character);
				playerCharacters.Sort((a, b) => b.currentAgile.CompareTo(a.currentAgile));
			}else if(nextCharacter > 1){
				playerCharacters.Add(character);
			}else if(nextCharacter == 0){
				playerCharacters.Add(character);
				nextCharacter = playerCharacters.Count - 1;
			}
		}
		public void RemoveCharacter(Character character){
			playerCharacters.Remove(character);
		}
	
		int tickPlayed = 9999;

		bool _isPlaying = true;
		public bool isPlaying {
			get => _isPlaying;
			set {
				bool original = _isPlaying;
				_isPlaying=value;
				if(_isPlaying) {
					Time.timeScale=1;
				} else {
					if(!noPause) Time.timeScale=0;
					//if(original) EnterCommandMode();
				}
			}
		}

		private void Start() {
			UnityEngine.Rendering.GraphicsSettings.transparencySortMode=TransparencySortMode.CustomAxis;
			UnityEngine.Rendering.GraphicsSettings.transparencySortAxis=new Vector3(0,0,1);
			instance=this;
			C = GetComponent<Character>();
		}

		private void FixedUpdate() {
			tickPlayed++;
			if(tickPlayed>tickPerRound) {
				EnterCommandMode();
				tickPlayed=0;
			}
			
		}
		public Character C	;
		void Update(){
			if (Input.GetKeyDown(KeyCode.K)){
				AddCharacter(C); //测试用
			}
		}

		public int nextCharacter;
		void EnterCommandMode() {
			if(traditionalTurnBased) {
				//UI.CommandPanelController.instance.EnterCommandMode(playerCharacters,nextCharacter);
				playerCharacters[nextCharacter].decision.PlayDecision();
				
				Debug.Log(playerCharacters.Count);
				for(int _ = 0;_<playerCharacters.Count;_++) {
					nextCharacter=(nextCharacter+1)%playerCharacters.Count;
					if(playerCharacters.Count > 0){
						if(nextCharacter == 1 ) playerCharacters.Sort((a, b) => b.currentAgile.CompareTo(a.currentAgile));//当当前角色为列表第一时，重新排序
					}
					if(playerCharacters[nextCharacter]) break;
				}
				
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



	}
}