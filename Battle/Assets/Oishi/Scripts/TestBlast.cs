using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBlast : MonoBehaviour {

    [SerializeField]
    float speed = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if(rb != null)
        {
            //other.GetComponent<Rigidbody>().AddForce(new Vector3(Random.onUnitSphere.x, Random.onUnitSphere.y,0) * 10000.0f);
            Vector3 velocity = (other.transform.position - this.transform.position).normalized * speed;
            rb.AddForce(velocity * speed);
            if (!rb.useGravity) { rb.useGravity = true; }
        }
        
    }
    private void OnParticleCollision(GameObject other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Animator ani = other.GetComponent<Animator>();
        if (rb != null)
        {
            //other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.onUnitSphere.x, Random.onUnitSphere.y, 0) * 10000.0f);
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("damage"))
            {
                Vector3 velocity = (other.transform.position - this.transform.position).normalized * speed;
                rb.AddForce(new Vector3(velocity.x * 2, velocity.y, 0) * speed);
            }
            if (ani != null) { ani.SetTrigger("damage"); }
        }
    }
}
