using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bowscript : MonoBehaviour {

    public Transform spine;
    public Transform neck;
    public bool bow;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (bow == true)
        {
            spine.rotation = Quaternion.Euler(new Vector3(0,70,-90));
            neck.rotation = Quaternion.Euler(new Vector3(0,20, -90));

        }
    }
}
