using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    public GameObject blast;
    public ParticleSystem exp;
    float speed = 30.0f;
    int count = 1;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(count <= 0)
        {
            Instantiate(blast, transform.position, Quaternion.identity);
            ParticleSystem _exp = Instantiate(exp, transform.position, Quaternion.identity);
            _exp.transform.localScale = new Vector3(5, 5, 5);
            _exp.Play();
            Destroy(gameObject);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Stage")
        {
            count--;
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
