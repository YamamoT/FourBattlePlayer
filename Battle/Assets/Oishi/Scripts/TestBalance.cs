using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBalance : MonoBehaviour {
    public TellJudg upObject;
    public TellJudg downObject;

    Vector3 upStartPos;
    Vector3 upEndPos;
    Vector3 downStartPos;
    Vector3 downEndPos;
    public float speed = 0.5f;
    private float time;
    private float upJourneyLength;
    private float downJourneyLength;

    // Use this for initialization
    void Start () {
        upStartPos = upObject.transform.position;
        downStartPos = downObject.transform.position;
        upEndPos = new Vector3(upObject.transform.position.x,   downObject.transform.position.y, upObject.transform.position.z);
        downEndPos = new Vector3(downObject.transform.position.x, upObject.transform.position.y,   downObject.transform.position.z);
        time = 0.0f;
        upJourneyLength = Vector3.Distance(upStartPos, upEndPos);
        downJourneyLength = Vector3.Distance(downStartPos, downEndPos);
    }
	
	// Update is called once per frame
	void Update () {

        /*Ray upRay = new Ray(upObject.transform.position, new Vector3(0, 1, 0));
        Debug.DrawRay(upRay.origin, upRay.direction, Color.red, 1.0f);
        RaycastHit upHit;
        Ray downRay = new Ray(downObject.transform.position, new Vector3(0, 1, 0));
        Debug.DrawRay(downRay.origin, downRay.direction, Color.red, 1.0f);
        RaycastHit downHit;
        
        if (Physics.Raycast(upRay, out upHit, 0.8f))
        {
            if (upHit.collider.tag == "Player")
            {
                time += speed;
                if(time >= 2.0f) { time = 2.0f; }
            }
        }
        else if (Physics.Raycast(downRay, out downHit, 0.8f))
        {
            if (downHit.collider.tag == "Player")
            {
                time -= speed;
                if (time <= 0.0f) { time = 0.0f; }
            }
        }*/
        if (upObject.flag)
        {
            time += speed;
            if (time >= 2.0f) { time = 2.0f; }
        }
        if (downObject.flag)
        {
            time -= speed;
            if (time <= 0.0f) { time = 0.0f; }
        }

        float upJracJourney = time / upJourneyLength;
        float downJracJourney = time / downJourneyLength;
        upObject.transform.position = Vector3.Lerp(upStartPos, upEndPos, upJracJourney);
        downObject.transform.position = Vector3.Lerp(downStartPos, downEndPos, downJracJourney);
    }
}
