using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Combat {
	[CreateAssetMenu(fileName = "�½�����������",menuName = "�Զ�/����������")]
	public class MoveSetInfo:ScriptableObject {
		[InspectorName("������")]
		public MoveInfo[] moves;
		[InspectorName("������")]
		public AnimatorOverrideController animations;

		public const int moveRunIndex = 4;
		public const int moveHaltIndex = 5;
	}



	[System.Serializable]
	public class MoveInfo {
		[InspectorName("�˺���Χ")]
		public Vector2Int damageRange;
		[InspectorName("�˺�����")]
		public DamageType damageType;
		[InspectorName("����")]
		public Vector2 relativeKnockback;
		[InspectorName("����ʱ��")]
		public int moveDuration;
		[InspectorName("�����������")]
		public float maxResetSpeed;
		[InspectorName("��߳�ʼ����")]
		public float maxInitialSpeed;
		[InspectorName("Ԥ��")]
		public GameObject previewObject;
		[InspectorName("��������")]
		public string moveName;
		[InspectorName("����ͼ��")]
		public Sprite moveIcon;

		float _attackRange = float.PositiveInfinity;
		public float attackRange {
			get {
				if(!float.IsPositiveInfinity(_attackRange)) return _attackRange;
				if(!previewObject) return 0;
				if(!previewObject.TryGetComponent<PreviewObjectAdditionalData>(out var data)) return 0;
				_attackRange=data.attackRange;
				return data.attackRange;
			}
		}

		float _moveDistance = float.PositiveInfinity;
		public float moveDistance {
			get {
				if(!float.IsPositiveInfinity(_moveDistance)) return _moveDistance;
				if(!previewObject) return 0;
				if(!previewObject.TryGetComponent<PreviewObjectAdditionalData>(out var data)) return 0;
				_moveDistance=data.moveDistance;
				return data.moveDistance;
			}
		}
		float _attackMinDistance = float.PositiveInfinity;
		public float attackMinDistance {
			get {
				if(!float.IsPositiveInfinity(_attackMinDistance)) return _attackMinDistance;
				if(!previewObject) return 0;
				if(!previewObject.TryGetComponent<PreviewObjectAdditionalData>(out var data)) return 0;
				_attackMinDistance=data.attackMinDistance;
				return data.attackMinDistance;
			}
		}
		float _attackTime = float.PositiveInfinity;
		public float attackTime {
			get {
				if(!float.IsPositiveInfinity(_attackTime)) return _attackTime;
				if(!previewObject) return 0;
				if(!previewObject.TryGetComponent<PreviewObjectAdditionalData>(out var data)) return 0;
				_moveDistance=data.attackTime;
				return data.attackTime;
			}
		}

	}
}