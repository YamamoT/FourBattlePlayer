using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlast : MonoBehaviour {

    public float speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().AddForce(new Vector3(Random.onUnitSphere.x, Random.onUnitSphere.y,0) * 10000.0f);
        }
    }
}
