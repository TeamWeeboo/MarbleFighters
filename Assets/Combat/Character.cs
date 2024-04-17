using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Combat {
	public class Character:MonoBehaviour {

		public MoveSetInfo moveSet;
		[HideInInspector] new public Rigidbody rigidbody;
		public MovePlayer movePlayer { get; private set; }
		void Start() {
			rigidbody=GetComponent<Rigidbody>();
			movePlayer=GetComponent<MovePlayer>();
		}
		void Update() {

		}

		public bool CanPlayMove(int index) {
			if(movePlayer.isMoving) return false;
			if(rigidbody.velocity.magnitude>moveSet.moves[index].maxInitialSpeed) return false;
			return true;
		}
		public bool HasMove(){
			bool hasMove = false;
			for(int i = 0;i<moveSet.moves.Length;i++) {
				if(CanPlayMove(i)) {
					hasMove=true;
					break;
				}
			}
			return hasMove;
		}
	}
}
