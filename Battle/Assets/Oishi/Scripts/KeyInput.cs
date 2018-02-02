using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour {
    bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale = transform.localScale;
        if (Input.GetKeyDown(KeyCode.A))
        {
            flag = !flag;
        }
        if (flag)
        {
            scale += new Vector3(0.5f, 0.5f, 0.5f);
            if (scale.x >= 25) { flag = false; }
        }
        else
        {
            scale = new Vector3(0,0,0);
        }

        transform.localScale = scale;
	}
}
