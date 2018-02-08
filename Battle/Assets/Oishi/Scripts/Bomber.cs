using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MonoBehaviour {
    public Transform startPos;
    public Transform endPos;
    public float speed = 0.5f;
    private float startTime;
    float count = 0;
    public GameObject bomb;
    // Use this for initialization
    void Start () {
        startTime = Time.time;
        count = Random.Range(10, 15);
    }
	
	// Update is called once per frame
	void Update () {
        float elapsedTime = (Time.time - startTime) * speed;
        transform.position = new Vector3(Mathf.Lerp(startPos.position.x, endPos.position.x, Mathf.PingPong(elapsedTime, 1.0f)), transform.position.y, transform.position.z);

        count -= Time.deltaTime;
        if (count <= 0)
        {
            Instantiate(bomb, transform.position, Quaternion.identity);
            count = Random.Range(10, 15);
        }
    }
}
