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
        public delegate void EnterNextRound();
        public event EnterNextRound OnEnterNextRound;
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
		public int Round;
		void EnterCommandMode() {
			playerCharacters[nextCharacter].decision.PlayDecision();
            Debug.Log(playerCharacters.Count);
			for(int _ = 0;_<playerCharacters.Count;_++) {
				nextCharacter=(nextCharacter+1)%playerCharacters.Count;
				
				if(playerCharacters[nextCharacter] != null) {
					if(nextCharacter==1) playerCharacters.Sort((a,b) => b.currentAgile.CompareTo(a.currentAgile));//进入下个回合时当nextcharacter为列表第二时，重新排序
				}
				Round++;    //不知道怎么判断战斗结束，所以Round归1并没有做
                OnEnterNextRound?.Invoke();
                if (playerCharacters[nextCharacter]) break;
			}

		}

	}
}