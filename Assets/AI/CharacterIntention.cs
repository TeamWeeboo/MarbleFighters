using System.Collections;
using System.Collections.Generic;
using Type = System.Type;
using UnityEngine;
using Combat;

namespace AI {
	public class CharacterIntention:MonoBehaviour {

		[HideInInspector] public Character character;
		[HideInInspector] public MovePlayer movePlayer;
		[HideInInspector] public CharacterDecision decision;
		public IntentionModel lastIntention { get; protected set; }

		[SerializeField] string intentionGroupTypeName;
		public IntentionGroup originalIntentionGroup { get; protected set; }
		public IntentionGroup currentIntentionGroup { get; protected set; }

		void Start() {
			Type intentionGroupType = Type.GetType(intentionGroupTypeName);
			if(intentionGroupType==null) intentionGroupType=Type.GetType("AI."+intentionGroupTypeName);
			if(intentionGroupType==null) intentionGroupType=Type.GetType("Combat."+intentionGroupTypeName);
			if(intentionGroupType==null) intentionGroupType=typeof(CharacterIntention);
			originalIntentionGroup=intentionGroupType.GetConstructor(new Type[0]).Invoke(new object[0]) as IntentionGroup;
			if(originalIntentionGroup==null) originalIntentionGroup=new IntentionGroup();
			currentIntentionGroup=originalIntentionGroup;
			character=GetComponent<Character>();
			movePlayer=GetComponent<MovePlayer>();
			decision=GetComponent<CharacterDecision>();
		}

		public IntentionModel GetIntention() {
			lastIntention=currentIntentionGroup.GetIntention(this);
			return lastIntention;
		}
		public void JoinGroup(IntentionGroup newGroup) {
			if(newGroup==currentIntentionGroup) return;
			if(newGroup==null||newGroup==originalIntentionGroup) {
				//退出现有组
				if(currentIntentionGroup==newGroup) return;
				currentIntentionGroup.RemoveCharacter(this);
				originalIntentionGroup.AddCharacter(this);

			} else {


			}
		}

	}
}