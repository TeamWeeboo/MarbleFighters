using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat {

	public class CommandModel {
		public int moveIndex;
		public Angle moveDirection;
	}

	public class GameController:MonoBehaviour {

		public static GameController instance;
		[SerializeField] int tickPerRound;
		[SerializeField] bool traditionalTurnBased;
		[SerializeReference] bool noPause;
		public List<Character> playerCharacters;

		public void AddCharacter(Character character) {
			if(nextCharacter==1) {
				playerCharacters.Add(character);
				playerCharacters.Sort((a,b) => b.currentAgile.CompareTo(a.currentAgile));
			} else if(nextCharacter>1) {
				playerCharacters.Add(character);
			} else if(nextCharacter==0) {
				playerCharacters.Add(character);
				nextCharacter=playerCharacters.Count-1;
			}
		}
		public void RemoveCharacter(Character character) {
			playerCharacters.Remove(character);
			if(nextCharacter>=playerCharacters.Count)
				nextCharacter=playerCharacters.Count-1;
		}
		public bool ContainCharacter(Character character) => playerCharacters.Contains(character);

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
				}
			}
		}

		private void Start() {
			UnityEngine.Rendering.GraphicsSettings.transparencySortMode=TransparencySortMode.CustomAxis;
			UnityEngine.Rendering.GraphicsSettings.transparencySortAxis=new Vector3(0,0,1);
			instance=this;
			C=GetComponent<Character>();
		}

		private void FixedUpdate() {
			tickPlayed++;
			if(tickPlayed>tickPerRound) {
				EnterCommandMode();
				tickPlayed=0;
			}

		}
		public Character C;
		void Update() {
			if(Input.GetKeyDown(KeyCode.K)) {
				AddCharacter(C); //测试用
			}
		}

		public int nextCharacter;
		void EnterCommandMode() {
			playerCharacters[nextCharacter].decision.PlayDecision();

			Debug.Log(playerCharacters.Count);
			for(int _ = 0;_<playerCharacters.Count;_++) {
				nextCharacter=(nextCharacter+1)%playerCharacters.Count;
				if(playerCharacters.Count>0) {
					if(nextCharacter==1) playerCharacters.Sort((a,b) => b.currentAgile.CompareTo(a.currentAgile));//当当前角色为列表第一时，重新排序
				}
				if(playerCharacters[nextCharacter]) break;
			}

		}

	}
}