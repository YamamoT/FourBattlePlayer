using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour {
    float start = 0;
    float end = -90;
    public bool open = false;
    bool flag;
    public FloorSwitch f_switch;
    public float count;
    float speed = 20.0f;
    // Use this for initialization
    void Start () {
        flag = f_switch.flag;
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (flag != f_switch.flag)
        {
            flag = f_switch.flag;

            if (flag) { open = !open; }
        }
        if (open)
        {
            count -= speed * Time.deltaTime;
            if (count <= end) { count = end; }
        }
        else
        {
            count += speed/ 2 * Time.deltaTime;
            if (count >= start) { count = start; }
        }
        
        transform.rotation = Quaternion.Euler(0,0,count);
    }
}
