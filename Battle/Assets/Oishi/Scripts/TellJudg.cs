using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TellJudg : MonoBehaviour {
    public bool flag { get; set; }

	// Use this for initialization
	void Start () {
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            flag = false;
        }
    }
}
