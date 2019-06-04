using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Networking;

public class BeetleController : NetworkBehaviour
{
    private Rigidbody rb; //used to access the rigidbody of the beetle
    private Animation anim; //used to play animations on the beetle
   
    public Transform Bullet_Emitter; //Accesses the location of the bullet emitter object on the beetle to fire from

    public GameObject Bullet; //the prefab for the bullet to spawn


    public float Bullet_Forward_Force; //this number can be modified in the game

    private static bool pressed; //determines if the button was pushed to fire on the next update. Useful for limitations with GUI controls

    //initialize data on beetle
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) //prevents players from taking control of other player's beetle
        {
            return;
        }

        if (pressed) //if the fire button was pressed during the last update. This is necessary because a [Command]
            //function can only be called from a system called method (System calls Update so Update can call CmdFire
        {
            
            CmdFireAnim();
            pressed = false;
        }
        bool behindLine;
        float lineZ = GameObject.Find("MiddleLine").transform.position.z;
        if (Bullet_Emitter.position.z > lineZ) //checks to see if the current player is infront of the line or behind it. If behind, then all animations and actions must be in reverse
        {
            behindLine = true;
        }
        else
            behindLine = false;

      
        if ((anim.IsPlaying("Shoot") || anim.IsPlaying("ShootBack"))) //prevent players from moving while shooting
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
            Vector3 movement = new Vector3(x, 0, y); //Get movement vector from direction user is pushing in
            rb.velocity = movement * 1f;

            if (x != 0 && y != 0)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * transform.eulerAngles.z);
            }
            else if (x == 0 && y == 0)
            {
                if (behindLine)
                {
                    anim.Play("AssBack"); //I was really annoyed that I accidentally made a second animation that does nothing. It's quite easy to do in Blender, 
                                         //and really hard to undo, So i named it this in my frustration and used it as the default standing animation.
                }
                else
                    anim.Play("Ass");
            }

            if (x != 0 || y != 0)
            {

                if (behindLine)
                    anim.Play("WalkBack");
                }
                else
                anim.Play("Walk");
            }
        }

    public void FirePressed() //used to signal the fire button was pressed
    {
        pressed = true;
    }

    [Command]
    public void CmdFireAnim() //important method for firing the cannon
    {
        GameObject Temporary_Bullet_Handler; //instanciate the buller handler

        if (Bullet_Emitter.position.z > GameObject.Find("MiddleLine").transform.position.z) //check if player is behind the line or not
        {
            Temporary_Bullet_Handler = (GameObject)Instantiate(Bullet, Bullet_Emitter.position - new Vector3(0, 0, .7f), Bullet_Emitter.rotation); //offset where bullet spawns so it doesn't it player in the face
            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = Temporary_Bullet_Handler.transform.forward *  - Bullet_Forward_Force; // set velocity  
            anim.Play("Shoot"); //play animation
        }
        else
        {
            Temporary_Bullet_Handler = (GameObject)Instantiate(Bullet, Bullet_Emitter.position, Bullet_Emitter.rotation);
            Temporary_Bullet_Handler.GetComponent<Rigidbody>().velocity = Temporary_Bullet_Handler.transform.forward * 6.0f;
            anim.Play("Shoot");
        }


        Destroy(Temporary_Bullet_Handler, 2.0f); //remove the bullet from the game after 2 seconds
        NetworkServer.Spawn(Temporary_Bullet_Handler); //spawn the bullet on the network so both players can see    
        }
    }