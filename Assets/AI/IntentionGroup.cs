using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Combat;

namespace AI {
	public class IntentionGroup {

		public CharacterIntention owner;
		public HashSet<CharacterIntention> members=new HashSet<CharacterIntention>();

		public IntentionModel generalIntention = new IntentionModel();


		public virtual void Unbind() {
			foreach(var i in members) i.JoinGroup(i.originalIntentionGroup);
		}
		public virtual void AddCharacter(CharacterIntention sender) {
			if(members.Contains(sender)) return;
			members.Add(sender);
			if(members.Count==1) generalIntention=sender.lastIntention.Clone();
		}
		public virtual void RemoveCharacter(CharacterIntention sender) {
			if(!members.Contains(sender)) return;
			members.Remove(sender);
		}

		public virtual IntentionModel GetIntention(CharacterIntention sender) {
			IntentionModel result = new IntentionModel();
			if(members.Count>1) {
				//多人
				Character target = GetTarget(owner.character,distanceWeight: 0);
				if(target) {
					float distance = (sender.transform.position-target.transform.position).magnitude;
					result.targetCharacter=target;
					result.intentionType=IntentionType.Attack;
				} else {
					result.intentionType=IntentionType.Hold;
					result.targetPosition=sender.transform.position;
				}
			} else {
				//单人
				Character target = GetTarget(owner.character,distanceWeight: 2);
				if(target) {
					float distance = (sender.transform.position-target.transform.position).magnitude;
					result.targetCharacter=target;
					result.intentionType=IntentionType.Attack;
				} else {
					result.intentionType=IntentionType.Hold;
					result.targetPosition=sender.transform.position;
				}
			}
			return result;
		}

		public static Character GetTarget(Character sender,float maxScore = 0,float relationWeight = 100000,float distanceWeight = 1,float hpWeight = 1) {

			float currentScore = maxScore;
			Character target = null;
			foreach(var i in GameController.instance.playerCharacters) {
				if(i==sender) continue;
				float newScore = 0;
				newScore+=relationWeight*FactionUtils.GetRelation(sender.faction,i.faction);
				newScore+=distanceWeight*(sender.transform.position-i.transform.position).magnitude;
				newScore+=hpWeight*i.currentHp;

				if(newScore<currentScore) {
					currentScore=newScore;
					target=i;
				}
			}

			return target;
		}

	}

	public enum IntentionType {
		Nothing = 0,
		Attack = 1,
		Move = 2,
		Recover = 3,
		Hold = 4,
	}

	public class IntentionModel {

		public Vector3 targetPosition;
		public Character targetCharacter;
		public IntentionType intentionType;

		public IntentionModel Clone() {
			IntentionModel result = new IntentionModel();
			result.targetPosition=targetPosition;
			result.targetCharacter=targetCharacter;
			result.intentionType=intentionType;
			return result;
		}

	}

}