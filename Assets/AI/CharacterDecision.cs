using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class CharacterDecision:MonoBehaviour {

		protected MovePlayer movePlayer;
		protected Character character;
		protected CharacterIntention intention;
		protected Rigidbody rigidbody;

		public virtual void PlayDecision() {
			IntentionModel currentIntention = intention.GetIntention();

			switch(currentIntention.intentionType) {

			//进攻
			case IntentionType.Attack: {
					if(!character.moveSet) break;
					int targetMoveIndex = -1;
					Angle targetAngle = new Angle();

					Vector3 targetPosition = currentIntention.targetPosition;
					if(currentIntention.targetCharacter) targetPosition=currentIntention.targetCharacter.transform.position;
					float targetMoveScore = (targetPosition-transform.position).magnitude;//攻击->-1000 移动->剩余位置

					//选择技能
					for(int i = 0;i<character.moveSet.moves.Length;i++) {
						MoveInfo thisMove = character.moveSet.moves[i];
						Vector3 moveVector = rigidbody.velocity*thisMove.moveDuration*Time.fixedDeltaTime;
						Angle thisAngle = new Angle();
						moveVector.y=0;

						targetPosition=currentIntention.targetPosition;
						if(currentIntention.targetCharacter) targetPosition=currentIntention.targetCharacter.transform.position;
						targetPosition-=moveVector;

						float currentDistance = (targetPosition-transform.position).magnitude;
						float newScore = Mathf.Abs(currentDistance-thisMove.moveDistance);
						thisAngle.vector3=(targetPosition-transform.position);

						if(currentDistance<thisMove.attackRange&&currentDistance>thisMove.attackMinDistance)
							newScore=-1000;

						if(newScore<targetMoveScore) {
							targetMoveScore=newScore;
							targetMoveIndex=i;
							targetAngle=thisAngle;
						}

					}

					if(targetMoveIndex!=-1)
						movePlayer.StartMove(character.moveSet,targetMoveIndex,targetAngle);
				}
				break;

			//不动
			case IntentionType.Hold: {
					if(!character.moveSet) break;
					movePlayer.StartMove(character.moveSet,MoveSetInfo.moveHaltIndex,new Angle());
				}
				break;

			case IntentionType.Move: {
					if(!character.moveSet) break;
					MoveInfo moveToChoose = character.moveSet.moves[MoveSetInfo.moveRunIndex];
					Vector3 moveVector = rigidbody.velocity*moveToChoose.moveDuration*Time.fixedDeltaTime;

					Vector3 targetPosition = currentIntention.targetPosition;
					if(currentIntention.targetCharacter) targetPosition=currentIntention.targetCharacter.transform.position;
					targetPosition-=moveVector;
					movePlayer.StartMove(character.moveSet,MoveSetInfo.moveRunIndex,new Angle(targetPosition-transform.position));
				}
				break;

			}



		}

		void Start() {
			movePlayer=GetComponent<MovePlayer>();
			character=GetComponent<Character>();
			intention=GetComponent<CharacterIntention>();
			rigidbody=GetComponent<Rigidbody>();
		}

	}
}