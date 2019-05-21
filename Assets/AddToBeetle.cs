using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddToBeetle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject beetle = GameObject.FindGameObjectWithTag("Player");
        transform.SetParent(beetle.transform, false);
    }

}
