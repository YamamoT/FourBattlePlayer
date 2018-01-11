using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSeesaw : MonoBehaviour {
    public TellJudg right;
    public TellJudg left;
    float rot = 0;
    float speed = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*Ray rightRay = new Ray(right.transform.position, new Vector3(0, 1, 0));
        Debug.DrawRay(rightRay.origin, rightRay.direction, Color.red, 1.0f);
        RaycastHit rightHit;

        Ray leftRay = new Ray(left.transform.position, new Vector3(0, 1, 0));
        Debug.DrawRay(leftRay.origin, leftRay.direction, Color.red, 1.0f);
        RaycastHit leftHit;

        if (Physics.Raycast(rightRay, out rightHit, 2.0f))
        {
            if (rightHit.collider.tag == "Player")
            {
                rot -= speed;
                if (rot <= -30.0f) { rot = -30.0f; }
            }
        }
        if (Physics.Raycast(leftRay, out leftHit, 2.0f))
        {
            if (leftHit.collider.tag == "Player")
            {
                rot += speed;
                if (rot >= 30.0f) { rot = 30.0f; }
            }
        }*/
        if (right.flag)
        {
            rot -= speed;
            if (rot <= -30.0f) { rot = -30.0f; }
        }
        if (left.flag)
        {
            rot += speed;
            if (rot >= 30.0f) { rot = 30.0f; }
        }
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
}
