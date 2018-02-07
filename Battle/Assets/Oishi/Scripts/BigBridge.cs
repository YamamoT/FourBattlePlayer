using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBridge : MonoBehaviour {
    [SerializeField]
    GameObject right;
    [SerializeField]
    GameObject left;

    [SerializeField]
    float speed = 1;
    [SerializeField]
    int rideNum = 0;
    [SerializeField]
    bool open = false;
    float rot = 0;
    int count = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(rideNum >= 3 && !open)
        {
            open = true;
        }

        if (open && count < 300)
        {
            if(rot >= 90) { rot = 90; count++; }
            else { rot += speed * Time.deltaTime; }
        }

        if (count >= 300)
        {
            
            if(rot <= 0) { rot = 0; open = false; count = 0; rideNum = 0; }
            else { rot -= speed * Time.deltaTime; }
        }

        right.transform.rotation = Quaternion.Euler(0, 0, rot);
        left.transform.rotation  = Quaternion.Euler(0, 0, -rot);
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "UnderCol")
        {
            rideNum++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            rideNum--;
        }
    }
}
