using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour {
    float sca = 0.5f;
    [SerializeField]
    float speed = 10;

    // Use this for initialization
    void Start () {
        transform.localScale += new Vector3(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update () {
        transform.localScale += new Vector3(1.5f, 1.5f, 1.5f);
        if(transform.localScale.x >= 15)
        {
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Animator ani = other.GetComponent<Animator>();
        if (rb != null)
        {
            Vector3 velocity = (other.transform.position - this.transform.position).normalized * speed;
            rb.AddForce(new Vector3(velocity.x*3, velocity.y, 0) * speed);
            if(ani != null) { ani.SetTrigger("damage"); }
            if (!rb.useGravity) { rb.useGravity = true; }
            if (rb.isKinematic) { rb.isKinematic = false; }
        }
    }
}
