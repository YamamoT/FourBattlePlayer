using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLift : MonoBehaviour {
    public Transform startPos;
    public Transform endPos;
    public Vector3 speed = new Vector3(0,0.5f,0);
    private float startTime;
    //private float journeyLength;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        //journeyLength = Vector3.Distance(startPos.position, endPos.position);
	}
	
	// Update is called once per frame
	void Update () {
        float elapsedTime = (Time.time - startTime) * speed.y;
        //float fracJourney = elapsedTime / journeyLength;

        //transform.position = Vector3.Lerp(startPos.position, endPos.position, fracJourney);
        transform.position = new Vector3(transform.position.x,Mathf.Lerp(startPos.position.y, endPos.position.y, Mathf.PingPong(elapsedTime, 1.0f)),transform.position.z);
        //transform.position = new Vector3( Mathf.Lerp(startPos.position.y, endPos.position.y, Mathf.PingPong(elapsedTime, 1.0f)), transform.position.y, transform.position.z);
    }
}
