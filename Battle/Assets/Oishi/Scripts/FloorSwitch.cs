using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwitch : MonoBehaviour {
    public Transform btn;
    public bool flag;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 movePos;
    public int count = 1;
    // Use this for initialization
    void Start () {
        flag = false;
        startPos = btn.position;
        endPos = transform.position;
        movePos = startPos;
    }
	
	// Update is called once per frame
	void Update () {
        if (flag)
        {
            movePos.y -= 0.1f;
            if(movePos.y <= endPos.y) { movePos.y = endPos.y; }
        }
        else
        {
            movePos.y += 0.1f;
            if (movePos.y >= startPos.y) { movePos.y = startPos.y; }
        }
        
        btn.position = movePos;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {       
            flag = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            flag = false;
        }
    }
}
