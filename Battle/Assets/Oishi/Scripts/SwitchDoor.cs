using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour {
    [SerializeField]
    Vector3 start;
    [SerializeField]
    Vector3 end;
    public bool open = false;
    bool flag;
    public FloorSwitch f_switch;
    public float count;
    public float journeyLength;
    private float speed = 1.0f;
    [SerializeField]
    float fracJourney;

    // Use this for initialization
    void Start () {
        flag = f_switch.flag;
        start = transform.position;
        end = start + new Vector3(0, 4, 0);
        count = 0;
        journeyLength = Vector3.Distance(start, end);
    }
	
	// Update is called once per frame
	void Update () {
        if(flag != f_switch.flag)
        {
            flag = f_switch.flag;

            if (flag) { open = !open; }
        }

        if (open)
        {
            count += 0.2f;
            if (count >= end.y) { count = end.y; }
        }
        else
        {
            count -= 0.2f;
            if(count <= 0) { count = 0; }
        }
        //float elapsedTime = count * speed;
        //if(elapsedTime >= end.y * speed) { elapsedTime = end.y * speed; }
        fracJourney = count / journeyLength;

        transform.position = Vector3.Lerp(start, end, fracJourney/4);

    }
}
