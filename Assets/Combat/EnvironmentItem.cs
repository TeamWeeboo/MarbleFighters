using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Combat{
    public class EnvironmentItem : MonoBehaviour
    {
        public float bounceForce = 10f; // 弹开的力度

        public void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.GetComponent<Character>() != null)
            {
                collision.collider.GetComponent<Rigidbody>().AddExplosionForce(bounceForce, collision.contacts[0].point, 1f);
            }
        }           
    }
}
