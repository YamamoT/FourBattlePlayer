using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollObj : MonoBehaviour {

    [SerializeField]
    int landingCount = 10;
    [SerializeField]
    int timeCount = 120;
    float rot = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (landingCount > 0) return;
        if (landingCount <= 0)
        {
            rot-= 2;
            if (rot <= -90) { rot = -90; }
            transform.rotation = Quaternion.Euler(0, 0, rot);
        }
        if (timeCount-- <= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            timeCount = 7200;
            landingCount = 10;
            rot = 0;
        }       	
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            landingCount--;
        }
    }
}
