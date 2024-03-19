using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Combat {
	public class HpBarController:MonoBehaviour {
		void Start() {
			GetComponentReference();
		}
		void Update() {
			UpdateValue();
			UpdateImage();
		}

		//unity reference
		public Character source;
		Image overlayRed;
		Image overlayWhite;

		[SerializeField]
		float whiteRetractDelay = 0.1f;
		[SerializeField]
		float whiteRetractSpeed = 3;

		float ratioWhite = 1;
		float ratioRed = 1;

		void GetComponentReference() {
			source=GetComponentInParent<Character>();
			Image[] images = GetComponentsInChildren<Image>();
			foreach(Image i in images) {
				if(i.name=="HealthBarOverlayWhite") overlayWhite=i;
				if(i.name=="HealthBarOverlayRed") overlayRed=i;
			}
		}

		void UpdateValue() {
			if(!source) return;
			ratioRed=(float)source.hp/source.hpMax;
			if(ratioWhite>ratioRed&&source.timeAfterHit>whiteRetractDelay) ratioWhite=Mathf.Max(ratioRed,ratioWhite-whiteRetractSpeed*Time.deltaTime);
		}
		void UpdateImage() {
			overlayRed.fillAmount=ratioRed;
			overlayWhite.fillAmount=ratioWhite;
		}
	}
}

