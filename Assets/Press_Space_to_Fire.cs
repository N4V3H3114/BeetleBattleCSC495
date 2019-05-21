using UnityEngine;
using System.Collections;

public class Press_Space_to_Fire : MonoBehaviour
{

    private bool pressed = false;
    private float[] times;
    private int i = 0; //i stands for instance of time
    private BeetleController beetle;
    // Use this for initialization
    void Start()
    {
        beetle = FindObjectOfType<BeetleController>();
        times = new float[3];
        times[0] = -3;
        times[1] = -3;
        times[2] = -3;
    }

    public void on_click()
    {
        pressed = true;
        Debug.Log("about to call fire");
        beetle.CmdFireAnim();
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressed && Time.fixedTime - times[i] > 3)
        {
            times[i] = Time.fixedTime;
            i++;
            if(i >=3 )
            {
                i = 0;
            }
            
            
        }
        pressed = false;
    }
}