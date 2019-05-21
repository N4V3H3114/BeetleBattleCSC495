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
    public GameObject Bullet_Emitter;

    //Drag in the Bullet Prefab from the Component Inspector.
    public GameObject Bullet;
    // Start is called before the first frame update
    public float Bullet_Forward_Force;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer)
        {
            return;
        }

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

    public void FirePressed()
    {
        Debug.Log("Button was pressed");
        CmdFireAnim();
    }

    [Command]
    public void CmdFireAnim()
    {

       

        //The Bullet instantiation happens here.
        Debug.Log("Should fire");
        GameObject Temporary_Bullet_Handler;
        Temporary_Bullet_Handler = Instantiate(Bullet, Bullet_Emitter.transform.position, Bullet_Emitter.transform.rotation) as GameObject;

        GameObject beetle = GameObject.FindGameObjectWithTag("Player");
        transform.SetParent(beetle.transform, false);

        //Sometimes bullets may appear rotated incorrectly due to the way its pivot was set from the original modeling package.
        //This is EASILY corrected here, you might have to rotate it from a different axis and or angle based on your particular mesh.
        Temporary_Bullet_Handler.transform.Rotate(Vector3.left * 90);

        //Retrieve the Rigidbody component from the instantiated Bullet and control it.
        Rigidbody Temporary_RigidBody;
        Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody>();
        //Tell the bullet to be "pushed" forward by an amount set by Bullet_Forward_Force.
        Temporary_RigidBody.velocity = new Vector3(0f, 0f, 6f);

        Debug.Log(Temporary_RigidBody.velocity.magnitude);
        //NetworkServer.Spawn(Temporary_Bullet_Handler);

        Debug.Log(Temporary_RigidBody.velocity.magnitude);
        //Basic Clean Up, set the Bullets to self destruct after 10 Seconds, I am being VERY generous here, normally 3 seconds is plenty.
        Destroy(Temporary_Bullet_Handler, 4.0f);
    }
}
