using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI;

namespace Combat {
	public class Character:MonoBehaviour {

		public Factions faction;

		public string playerName;
		public bool isNPC;
		public string description;
		public int playerLevel;
		public int maxLevel;
		public int currentLevel;
		[HideInInspector] public int[] nextLevelExp;
		public int currentExp;
		public int currentHp, maxHp;
		[HideInInspector] public int currentAd, originAd, currentAp, originAp, currentAgile, originAgile;
		public int currentArmor, originArmor;
		[HideInInspector] public int weight, agileOffset;
		[HideInInspector] public int levelPoint;
		[HideInInspector] public int mainProperty; //所持武器代表属性
		public int baseAd, baseAp, baseAgile; //用于调整基础属性
		[HideInInspector] public int atkAd, atkAgile, atkAp;

		public CharacterDecision decision;
		public float timeAfterHit { get; private set; }


		public MoveSetInfo moveSet;
		[HideInInspector] new public Rigidbody rigidbody;
		public MovePlayer movePlayer { get; private set; }
		public DamageTarget damageTarget { get; private set; }
		void Start() {
			decision=GetComponent<CharacterDecision>();
			rigidbody=GetComponent<Rigidbody>();
			movePlayer=GetComponent<MovePlayer>();
			damageTarget=GetComponent<DamageTarget>();
			GetComponent<DamageTarget>().damaging.Add(0,Damage);

			originAd=baseAd;
			originAgile=baseAgile;
			originAp=baseAp;
			RefreshProperties();
			nextLevelExp=new int[maxLevel];
			nextLevelExp[0]=1000;
			for(int i = 1;i<maxLevel;i++) {
				nextLevelExp[i]=Mathf.RoundToInt(nextLevelExp[i-1]*1.1f);  //升级经验的算法 
			}
			Debug.Log("Start");
		}
		private void FixedUpdate() {
			if(currentHp<=0) Die();
			timeAfterHit+=Time.deltaTime;
		}

		public void AddExp() {
			currentExp+=9999;
			levelPoint++;
			if(currentExp>nextLevelExp[playerLevel-1]&&playerLevel<maxLevel) {
				LevelUp();
			}
		}
		public void LevelUp() {   //升级加的属性
			currentExp-=nextLevelExp[playerLevel-1];
			playerLevel++;
			maxHp=originAd*5;
			if(currentExp>nextLevelExp[playerLevel-1]&&playerLevel<maxLevel) {
				LevelUp();
			}

		}
		public void AdUp() {
			originAd++;
			RefreshProperties();
		}
		public void ApUp() {
			originAp++;
			RefreshProperties();
		}
		public void AgileUp() {
			originAgile++;
			RefreshProperties();
		}
		void RefreshProperties() {
			currentAd=originAd;
			currentAp=originAp;
			maxHp=originAd*5;
			currentHp=maxHp;
			if(weight-maxHp>0) {
				agileOffset=Mathf.RoundToInt((weight-maxHp)*0.5f);
			} else agileOffset=0;
			currentAgile=originAgile-agileOffset-1; //1代表护甲减值
			originArmor=Mathf.RoundToInt(originAd*0.5f+originAgile*0.5f)+10; //50代表装备护甲
			currentArmor=originArmor;
		}

		public virtual void Die() {
			Destroy(gameObject);
		}

		public bool CanPlayMove(int index) {
			if(movePlayer.isMoving) return false;
			if(rigidbody.velocity.magnitude>moveSet.moves[index].maxInitialSpeed) return false;
			return true;
		}
		public bool HasMove() {
			bool hasMove = false;
			for(int i = 0;i<moveSet.moves.Length;i++) {

				if(CanPlayMove(i)) {
					hasMove=true;
					break;
				}
			}
			return hasMove;
		}
		private bool AttackHit(DamageModel damage) {
			if(Random.Range(damage.damageRange.x,damage.damageRange.y+1)+Mathf.RoundToInt(mainProperty*0.5f)>=currentArmor) {        //装备加值未添加
				return true;
			} else return false;
		}

		public void Damage(DamageModel damage) {
			//Debug.Log("!!!");
			if(currentHp<=0) {
				damageTarget.damageSuccess=false;
				return;
			}
			timeAfterHit=0;
			if(AttackHit(damage)) {
				damageTarget.damageSuccess=true;
				currentHp-=Random.Range(damage.damageRange.x,damage.damageRange.y+1);
				Vector3 knockback3 = new Vector3(damage.knockback.x,0,damage.knockback.y);
				rigidbody.AddForce(knockback3,ForceMode.Impulse);
				Debug.Log("Hp"+currentHp);
				Debug.Log("Armor"+currentArmor);


			} else {
				damageTarget.damageSuccess=true;
				if(currentArmor>0) {
					currentArmor-=Random.Range(damage.damageRange.x,damage.damageRange.y+1);
					Debug.Log("Hp"+currentHp);
					Debug.Log("Armor"+currentArmor);
				}
				if(currentArmor<=0) {
					currentArmor-=Random.Range(damage.damageRange.x,damage.damageRange.y+1);
					Debug.Log("Hp"+currentHp);
					Debug.Log("Armor"+currentArmor);
				}

			}


		}
	}
}