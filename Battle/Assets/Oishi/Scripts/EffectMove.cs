using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour {

    private Bezier bez;
    public float count;
    public float moveCount;
    public Vector3 target;

    float t = 0;
    // Use this for initialization
    void Start () {
        Vector3 pos = transform.position;

        bez = new Bezier(transform.position, new Vector3(0,0,0), 
            new Vector3(0, 0, 0), target);
    }

    // Update is called once per frame
    void Update()
    {
       if ((count < moveCount*1.5f)&&(count >= moveCount/2))
        {
            
            Vector3 vec = bez.GetPointAtTime(t);
            transform.position = vec;
            t += ((60.0f / moveCount)*Time.deltaTime);
        }
       if(count <= moveCount / 3)
        {
            Vector3 scale = transform.localScale;
            scale.x -= (60.0f / 20.0f) * Time.deltaTime;
            scale.z -= (60.0f / 20.0f) * Time.deltaTime;
            transform.localScale = scale;
        }
        if (count <= 0)
        {
            Destroy(gameObject);
        }
        count = count - (60.0f * Time.deltaTime);
    }
}
