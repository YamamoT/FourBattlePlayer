using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {
    float start = 0;
    float end = -90;
    public bool open = false;
    public FloorSwitch f_switch;
    public float count;
    public float speed = 0.2f;
    // Use this for initialization
    void Start () {
        open = f_switch.flag;
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        open = f_switch.flag;
        if (open)
        {
            count -= speed;
            if (count <= end) { count = end; }
        }
        else
        {
            count += speed/2;
            if (count >= 0) { count = 0; }
        }
        
        transform.rotation = Quaternion.Euler(0,0,count);
    }
}
