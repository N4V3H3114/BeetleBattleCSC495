using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class BeetleController : NetworkBehaviour
{
    private Rigidbody rb;
    private Animation anim;
    //Drag in the Bullet Emitter from the Component Inspector.
    public Transform Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;
    // Start is called before the first frame update
    public float Bullet_Forward_Force;
    private static bool pressed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (pressed)
        {
            CmdFireAnim();
            pressed = false;
        }
        bool behindLine;
        float lineZ = GameObject.Find("MiddleLine").transform.position.z;
        if (Bullet_Emitter.position.z > lineZ)
        {
            behindLine = true;
        }
        else
            behindLine = false;

        if (anim.IsPlaying("Shoot") || anim.IsPlaying("ShootBack"))
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
            else if (x == 0 && y == 0)
            {
                if(behindLine)
                {
                    anim.Play("AssBack");
                }
                else
                    anim.Play("Ass");
            }

            if (x != 0 || y != 0)
            {
                if(behindLine)
                {
                    anim.Play("WalkBack");
                }
                else
                anim.Play("Walk");
            }
        }

    }

    public void FirePressed()
    {
        pressed = true;
    }

    [Command]
    public void CmdFireAnim()
    {
        //The Bullet instantiation happens here.
        Debug.Log("Should fire");
        GameObject Temporary_Bullet_Handler;
        
        if (Bullet_Emitter.position.z > GameObject.Find("MiddleLine").transform.position.z)
        {
            Temporary_Bullet_Handler = (GameObject)Instantiate(Bullet, Bullet_Emitter.position - new Vector3(0, 0, .7f), Bullet_Emitter.rotation);
            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = Temporary_Bullet_Handler.transform.forward * -6.0f;
            anim.Play("ShootBack");
            Debug.Log("I am firing backwards");
        }
        else
        {
            Temporary_Bullet_Handler = (GameObject)Instantiate(Bullet, Bullet_Emitter.position, Bullet_Emitter.rotation);
            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = Temporary_Bullet_Handler.transform.forward * 6.0f;
            anim.Play("Shoot");
            Debug.Log("I am firing forwards");
        }

        Debug.Log(Bullet_Emitter.position.z + " line is " + GameObject.Find("MiddleLine").transform.position.z);

        Destroy(Temporary_Bullet_Handler, 2.0f);
        NetworkServer.Spawn(Temporary_Bullet_Handler);
    }
}