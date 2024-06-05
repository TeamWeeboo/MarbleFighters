using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat{
    public class EnvironmentItem : MonoBehaviour
    {   

        [SerializeField]
        [Tooltip("弹开的力度")]
        private float damageBounceForce = 10f;
        private float septicBucketBounceForce = 20f;
        private float oilTankBounceForce = 20f;
        [SerializeField]
        [Tooltip("伤害地形的伤害")]
        private int damage = 5;
        [SerializeField]
        [Tooltip("粪叉的伤害")]
        private int damageOfFork = 6;
        [SerializeField]
        [Tooltip("粪桶的伤害")]
        private int damageOfSepticBucket = 7;
        [SerializeField]
        [Tooltip("油罐的伤害")]
        private int damageOfOilTank = 10;
        [SerializeField]
        [Header("带粪叉的干草堆")]
        [Tooltip("带粪叉的干草堆")]
        private bool isRickWithFork = false;
        [SerializeField]
        [Header("隐匿干草堆")]
        [Tooltip("隐匿干草堆")]
        private bool isRick = false;
        [SerializeField]
        [Header("伤害物品")]
        [Tooltip("伤害物品")]
        private bool isDamage = false;
        [SerializeField]
        [Header("油脂/冰面")]
        [Tooltip("油脂/冰面")]
        private bool isIce = false;
        [SerializeField]
        [Header("金汁/泥水")]
        [Tooltip("金汁/泥水")]
        private bool isMuddyWater = false;
        [SerializeField]
        [Header("粪桶")]
        [Tooltip("粪桶")]
        private bool isSepticBucket = false;
        public GameObject OilSurface;
        [SerializeField]
        [Header("油罐")]
        [Tooltip("油罐")]
        private bool isOilTank = false;
        public GameObject IceSurface;
        private int choice = 0;
        private int armor;
        [HideInInspector]public Character character;
        [HideInInspector]public Rigidbody rb;
        [HideInInspector]public MovePlayer moveplayer;


        
        void Awake(){
            if(isDamage) choice++;
            if(isIce) choice++;
            if(isMuddyWater) choice++;
            if(isOilTank) choice++;
            if(isRick) choice++;
            if(isRickWithFork) choice++;
            if(isSepticBucket) choice++;
            if(choice >1){
                Debug.Log("环境物品选择过多");
            }
            if(choice == 0){
                Debug.Log("环境物品未选择");
            }

        }



        void OnCollisionEnter(Collision collision)
        {   
            if (collision.collider.GetComponent<Character>() != null)
            {
                character = collision.collider.GetComponent<Character>();
                rb = collision.collider.GetComponent<Rigidbody>();
                moveplayer = collision.collider.GetComponent<MovePlayer>();
                armor = character.currentArmor;
                if(isRickWithFork){
                    rb.AddExplosionForce(damageBounceForce, collision.contacts[0].point, 1f);
                    if(armor > 0){
                        character.currentArmor -= damageOfFork;
                        if(character.currentArmor < 0){
                            character.currentHp += character.currentArmor;
                            character.currentArmor =0;
                        } 
                    }
                    if(armor == 0){
                        character.currentHp -= damageOfFork;
                    }
                }
                if(isDamage){
                    if(armor > 0){
                        character.currentArmor -= damage;
                        if(character.currentArmor < 0) {
                            character.currentHp += character.currentArmor;
                            character.currentArmor = 0;
                        }
                    }
                    if(armor == 0){
                        character.currentHp -= damage;

                    }
                }
                if(isRick){
                    rb.velocity = new Vector3(0f, 0f, 0f);
                    collision.collider.transform.position = transform.position;

                }
            }
            if (collision.collider.GetComponent<DamageDealer>() != null){
                character = collision.collider.GetComponentInParent<Character>();
                armor = character.currentArmor;
                rb = collision.collider.GetComponentInParent<Rigidbody>();
                if(isOilTank){
                    rb.AddExplosionForce(oilTankBounceForce, collision.contacts[0].point, 5f);
                    if(armor > 0){
                        character.currentArmor -= damageOfOilTank;
                        if(character.currentArmor < 0){
                            character.currentHp += character.currentArmor;
                            character.currentArmor =0;
                        } 
                    }
                    if(armor == 0){
                        character.currentHp -= damageOfOilTank;
                    }
                    GameObject Ice = Instantiate(IceSurface , transform.position, Quaternion.identity);
                }
                if(isSepticBucket){
                    rb.AddExplosionForce(septicBucketBounceForce, collision.contacts[0].point, 5f);
                    if(armor > 0){
                        character.currentArmor -= damageOfSepticBucket;
                        if(character.currentArmor < 0){
                            character.currentHp += character.currentArmor;
                            character.currentArmor =0;
                        } 
                    }
                    if(armor == 0){
                        character.currentHp -= damageOfSepticBucket;
                    }
                    GameObject Oil = Instantiate(OilSurface, transform.position, Quaternion.identity);
                }

            }
        }
        private void OnCollisionStay(Collision collision){
            moveplayer = collision.collider.GetComponent<MovePlayer>();
            if(isIce){
                moveplayer.isOnIce = true;
            }
            if(isMuddyWater){
                moveplayer.isOnMud = true;
            }
        }
        private void OnCollisionExit(Collision collision){
            moveplayer = collision.collider.GetComponent<MovePlayer>();
            if(isIce){
                moveplayer.isOnIce = false;
            }
            if(isMuddyWater){
                moveplayer.isOnMud = false;
            }

        }

    }
}
