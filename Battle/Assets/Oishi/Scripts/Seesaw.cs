using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : MonoBehaviour {

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
            this.GetComponent<Rigidbody>().AddForceAtPosition(Vector3.down * 1, other.transform.position);
        }
        if (transform.rotation.z > 45)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45.0f);
        }
        if (transform.rotation.z < -45)
        {
            transform.rotation = Quaternion.Euler(0, 0, -45.0f);
        }
    }
}
