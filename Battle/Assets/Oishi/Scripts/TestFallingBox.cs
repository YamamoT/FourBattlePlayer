using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFallingBox : MonoBehaviour {
    [SerializeField]
    int count = 0;
    [SerializeField]
    bool fall = false;
    Vector3 startPos;

	// Use this for initialization
	void Start () {
        startPos = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (count > 30)
        {
            fall = true;
        }

        if (fall)
        {
            transform.position = transform.position + new Vector3(0,-0.1f,0);
        }

        if(transform.position.y <= -10.0f)
        {
            transform.position = startPos;
            count = 0;
            fall = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            count = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            count++;
        }
    }
}
