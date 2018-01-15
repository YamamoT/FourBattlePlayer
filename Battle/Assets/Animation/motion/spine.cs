using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spine : MonoBehaviour
{

    public Transform spineval;
    public bool gun;
    public Vector3 val;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (gun == true)
        {
            spineval.rotation = Quaternion.Euler(val);
        }
    }
}
