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
            }

            Destroy(gameObject);
    }
}
