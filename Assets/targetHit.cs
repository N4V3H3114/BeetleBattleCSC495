using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class targetHit : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
            GameObject hit = c.gameObject;
            Health health = hit.GetComponent<Health>();

            if(health != null)
            {
                health.TakeDamage(10);
                if(health.getHealth() == 0) //if the hit target's health is 0 they should be removed
            {
                Destroy(hit);
            }
            }

            Destroy(gameObject);
        }
    }
