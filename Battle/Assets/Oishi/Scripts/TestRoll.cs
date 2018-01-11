using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoll : MonoBehaviour {
    float rot = 0;
    public float way = 0;
    public float count = 300.0f;

	// Use this for initialization
	void Start () {
        transform.rotation = Quaternion.Euler(0, 0, rot);
    }
	
	// Update is called once per frame
	void Update () {
        rot += 360 / count;
        transform.rotation = Quaternion.Euler(0, 0, rot * way);
    }
}
