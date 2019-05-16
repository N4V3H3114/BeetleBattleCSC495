using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetHit : MonoBehaviour
{
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name == "beetle")
        {
            Destroy(c.gameObject);
        }
    }
}
