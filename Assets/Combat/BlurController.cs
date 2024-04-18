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
			foreach(var i in blurs) {
				if(!i) continue;
				i.SetActive(true);
			}
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
				newGo=Instantiate(gameObject,new Vector3(0,100000,0),Quaternion.identity);
				newGo.SetActive(false);
				Component[] componentList = newGo.GetComponentsInChildren<Component>();
				foreach(var i in componentList) {
					if(i is MeshRenderer) {
						if(i.transform.parent.GetComponent<BillBoardController>()) i.transform.localPosition+=new Vector3(0,0,0.01f);

						MeshRenderer renderer = (MeshRenderer)i;
						Color c = renderer.material.color;
						c.a*=0.25f;
						renderer.material.color=c;
					} else if(i is Transform||i is BillBoardController||i is MeshFilter) {
					} else Destroy(i);
				}
				newGo.transform.position=transform.position;
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