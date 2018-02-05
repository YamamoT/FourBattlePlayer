using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerStates pStates = other.GetComponent<PlayerStates>();
            other.transform.position = new Vector3(0, 15, 0);
            pStates.Hp -= 20;
        }
    }
}
