using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgGround : MonoBehaviour {

    public bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        flag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }
}
