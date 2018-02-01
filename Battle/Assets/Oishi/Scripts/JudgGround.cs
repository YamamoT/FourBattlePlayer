using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgGround : MonoBehaviour {

    public bool flag = false;
    private void OnTriggerStay(Collider other)
    {
        flag = true;
    }

    private void OnTriggerExit(Collider other)
    {
        flag = false;
    }
}
