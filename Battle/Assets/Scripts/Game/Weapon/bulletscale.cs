using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletscale : MonoBehaviour {

    // Use this for initialization
    void Start () {

        this.transform.localScale = new Vector3(this.transform.parent.localScale.x * this.transform.localScale.x,
                                        this.transform.parent.localScale.y * this.transform.localScale.y,
                                        this.transform.parent.localScale.z * this.transform.localScale.z);

    }

    // Update is called once per frame
    void Update () {

    }
}
