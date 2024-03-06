using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Combat {
	[CreateAssetMenu(fileName = "新建技能组数据",menuName = "自定/技能组数据")]
	public class MoveSetInfo:ScriptableObject {
		[InspectorName("技能组")]
		public MoveInfo[] moves;
		[InspectorName("动画组")]
		public AnimatorOverrideController animations;

		public const int moveRunIndex = 4;
		public const int moveHaltIndex = 5;
	}



	[System.Serializable]
	public class MoveInfo {
		[InspectorName("伤害范围")]
		public Vector2Int damageRange;
		[InspectorName("伤害类型")]
		public DamageType damageType;
		[InspectorName("击退")]
		public Vector2 relativeKnockback;
		[InspectorName("持续时间")]
		public int moveDuration;
		[InspectorName("最高重置速率")]
		public float maxResetSpeed;
		[InspectorName("最高初始速率")]
		public float maxInitialSpeed;
		[InspectorName("预览")]
		public GameObject previewObject;
		[InspectorName("技能名称")]
		public string moveName;
		[InspectorName("技能图标")]
		public Sprite moveIcon;

	}
}