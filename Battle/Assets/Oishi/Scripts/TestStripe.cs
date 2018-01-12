﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestStripe : MonoBehaviour {

    // スクロール速度
    [SerializeField]
    private float speed = 1.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ScrollUV();
	}

    void ScrollUV()
    {
        var material = GetComponent<Renderer>().material;
        Vector2 offset = material.mainTextureOffset;
        offset += new Vector2(speed/10,0) * speed * Time.deltaTime;
        material.mainTextureOffset = offset;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            var body = other.gameObject.GetComponent<CharacterController>();
            //Debug.Log("hit");
            //Vector3 add = transform.forward * speed * Time.deltaTime;
            body.Move(new Vector3(speed, 0, 0) * Time.deltaTime);
        }
        else
        {
            other.transform.position += new Vector3(speed, 0, 0) * Time.deltaTime;
        }
    }
}