using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour {
    public GameObject blast;
    public ParticleSystem exp;
    float speed = 30.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Instantiate(blast, transform.position, Quaternion.identity);
            ParticleSystem _exp = Instantiate(exp, transform.position, Quaternion.identity);
            _exp.transform.localScale = new Vector3(3, 3, 3);
            _exp.Play();
            Destroy(gameObject);
        }

        if (other.tag == "Fist")
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 velocity = (transform.position - other.transform.position).normalized * speed;
                rb.AddForce(velocity * speed);
            }
        }
    }
}
