using System.Collections;
using System.Collections.Generic;
using TMPro;
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
		Image overlayArmor;
		Image overlayArmorWhite;
		TextMeshProUGUI nameText;

		[SerializeField]
		float whiteRetractDelay = 0.1f;
		[SerializeField]
		float whiteRetractSpeed = 3;

		float ratioWhite = 1;
		float ratioRed = 1;
		float ratioArmor = 1;
		float ratioArmorWhite = 1;

		void GetComponentReference() {
			source=GetComponentInParent<Character>();
			Image[] images = GetComponentsInChildren<Image>();
			foreach(Image i in images) {
				if(i.name=="HealthBarOverlayWhite") overlayWhite=i;
				if(i.name=="HealthBarOverlayRed") overlayRed=i;
				if(i.name=="HealthBarOverlayArmor") overlayArmor=i;
				if(i.name=="HealthBarOverlayArmorWhite") overlayArmorWhite=i;
			}
			nameText=GetComponentInChildren<TextMeshProUGUI>();
		}

		void UpdateValue() {
			if(!source) return;
			nameText.text=source.gameObject.name;
			ratioRed=(float)source.currentHp/source.maxHp;
			ratioArmor=(float)source.currentArmor/source.originArmor;
			if(ratioWhite>ratioRed&&source.timeAfterHit>whiteRetractDelay)
				ratioWhite=Mathf.Max(ratioRed,ratioWhite-whiteRetractSpeed*Time.deltaTime);
			if(ratioArmorWhite>ratioArmor&&source.timeAfterHit>whiteRetractDelay)
				ratioArmorWhite=Mathf.Max(ratioArmor,ratioArmorWhite-whiteRetractSpeed*Time.deltaTime);
		}
		void UpdateImage() {
			overlayRed.fillAmount=ratioRed;
			overlayWhite.fillAmount=ratioWhite;
			overlayArmor.fillAmount=ratioArmor;
			overlayArmorWhite.fillAmount=ratioArmorWhite;
		}
	}
}

