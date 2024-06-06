using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat{
    public class EnvironmentItem : MonoBehaviour
    {   

        [SerializeField]
        [Tooltip("����������")]
        private float damageBounceForce = 10f;
        private float septicBucketBounceForce = 20f;
        private float oilTankBounceForce = 20f;
        [SerializeField]
        [Tooltip("�˺����ε��˺�")]
        private int damage = 5;
        [SerializeField]
        [Tooltip("�����˺�")]
        private int damageOfFork = 6;
        [SerializeField]
        [Tooltip("��Ͱ���˺�")]
        private int damageOfSepticBucket = 7;
        [SerializeField]
        [Tooltip("�͹޵��˺�")]
        private int damageOfOilTank = 10;
        [SerializeField]
        [Header("�����ĸɲݶ�")]
        [Tooltip("�����ĸɲݶ�")]
        private bool isRickWithFork = false;
        [SerializeField]
        [Header("����ɲݶ�")]
        [Tooltip("����ɲݶ�")]
        private bool isRick = false;
        [SerializeField]
        [Header("�˺���Ʒ")]
        [Tooltip("�˺���Ʒ")]
        private bool isDamage = false;
        [SerializeField]
        [Header("��֬/����")]
        [Tooltip("��֬/����")]
        private bool isIce = false;
        [SerializeField]
        [Header("��֭/��ˮ")]
        [Tooltip("��֭/��ˮ")]
        private bool isMuddyWater = false;
        [SerializeField]
        [Header("��Ͱ")]
        [Tooltip("��Ͱ")]
        private bool isSepticBucket = false;
        public GameObject OilSurface;
        [SerializeField]
        [Header("�͹�")]
        [Tooltip("�͹�")]
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
                Debug.Log("������Ʒѡ�����");
            }
            if(choice == 0){
                Debug.Log("������Ʒδѡ��");
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
