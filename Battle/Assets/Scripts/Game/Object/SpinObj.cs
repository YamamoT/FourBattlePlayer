using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour {

    [SerializeField]
    private float _rotSpeed = 180;

	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0,_rotSpeed,0) * Time.deltaTime) ;
	}
}
