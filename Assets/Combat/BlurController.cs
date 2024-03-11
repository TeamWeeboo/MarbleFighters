using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Combat {
	public class BlurController:MonoBehaviour {

		const int blurInterval = 4;
		const int blurCount = 6;

		Vector3 lastPosition;

		//lastIndex -> 上一个残影 lastIndex+1 -> 上上个残影
		GameObject[] blurs = new GameObject[blurCount];
		int lastIndex;
		int tickPassed;

		void FixedUpdate() {
			tickPassed++;
			if(tickPassed>=blurInterval) {
				tickPassed=0;
				NewBlur();
			}
		}
		void NewBlur() {

			GameObject newGo;
			if(transform.position==lastPosition) {
				newGo=blurs[lastIndex];
				blurs[lastIndex]=null;
			} else {
				newGo=Instantiate(gameObject);
				Component[] componentList = newGo.GetComponentsInChildren<Component>();
				foreach(var i in componentList) {
					if(i is SpriteRenderer) {
						SpriteRenderer spriteRenderer = (SpriteRenderer)i;
						Color c = spriteRenderer.color;
						c.a*=0.25f;
						spriteRenderer.color=c;
					} else if(i is SortingGroup) {
						(i as SortingGroup).sortingOrder-=10;
					} else if(i is Transform) {
					} else Destroy(i);
				}
			}


			lastIndex=(lastIndex+blurCount-1)%blurCount;
			if(blurs[lastIndex]) Destroy(blurs[lastIndex]);
			blurs[lastIndex]=newGo;
			lastPosition=transform.position;

		}

		private void OnDestroy() {
			foreach(var i in blurs) {
				if(!i) continue;
				Destroy(i);
			}
		}

	}

}