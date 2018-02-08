using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour {
    [SerializeField]
    Vector3 comeback = new Vector3(0, 15, 0);
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
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            PlayerStates pStates = other.GetComponent<PlayerStates>();
            other.transform.position = comeback;
            pStates.Hp -= 20;
        }
    }
}
