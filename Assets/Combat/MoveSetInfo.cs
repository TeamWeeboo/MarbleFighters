using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Combat {
	[CreateAssetMenu(fileName = "新建技能组数据",menuName = "自定/技能组数据")]
	public class MoveSetInfo:ScriptableObject {
		public MoveInfo[] moves;
		public AnimatorOverrideController animations;

		public const float moveRunIndex = 4;
		public const float moveHaltIndex = 5;
	}



	[System.Serializable]
	public class MoveInfo {
		[InspectorName("伤害范围")]
		public Vector2Int damageRange;
		[InspectorName("伤害类型")]
		public DamageType damageType;
		[InspectorName("击退")]
		public Vector2 relativeKnockback;
		[InspectorName("预览")]
		public GameObject previewObject;
		[InspectorName("持续时间")]
		public int moveDuration;
		[InspectorName("最高重置速率")]
		public float maxResetSpeed;
		[InspectorName("最高初始速率")]
		public float maxInitialSpeed;
	}
}