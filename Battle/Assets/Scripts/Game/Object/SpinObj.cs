using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinObj : MonoBehaviour {

    [SerializeField]
    private float _rotSpeed = 5;

	// Update is called once per frame
	void Update () {
        this.transform.Rotate(0, _rotSpeed * Time.deltaTime, 0);
	}
}
