using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Combat {
	[CreateAssetMenu(fileName = "新建技能组数据",menuName = "自定/技能组数据")]
	public class MoveSetInfo:ScriptableObject {
		public MoveInfo[] moves;
		public AnimatorController animations;
	}



	[System.Serializable]
	public class MoveInfo {
		public Vector2Int damageRange;
		public DamageType damageType;
		public Vector2 relativeKnockback;
		public GameObject previewobject;
		public int moveDuration;
		public float maxInitialSpeed;
	}
}