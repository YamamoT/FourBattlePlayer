using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {
    float speed = 500.0f;
    public Transform player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 velocity = (other.transform.position - player.transform.position).normalized * speed;
            rb.AddForce(new Vector3(velocity.x, velocity.y * 1.5f, 0));// * speed);
        }
    }
}
