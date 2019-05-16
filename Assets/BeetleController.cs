using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class BeetleController : MonoBehaviour
{
    private Rigidbody rb;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.IsPlaying("Shoot"))
        {
            float x = 0;
            float y = 0;

            Vector3 movement = new Vector3(x, 0, y);

            rb.velocity = movement * 0f;
        }
        else
        {
            float x = CrossPlatformInputManager.GetAxis("Horizontal");
            float y = CrossPlatformInputManager.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0, y);
            rb.velocity = movement * 1f;

            if (x != 0 && y != 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * transform.eulerAngles.z);
            }
            else if(x == 0 && y == 0)
            {
                anim.Play("Ass");
            }

            if (x != 0 || y != 0)
            {
                anim.Play("Walk");
            }
        }
      
    }

    public void FireAnim()
    {

        anim.Play("Shoot");
    }
}
