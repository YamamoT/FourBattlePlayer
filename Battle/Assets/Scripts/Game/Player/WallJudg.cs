using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJudg : MonoBehaviour {

    public bool jg = false;

    private void OnTriggerStay(Collider col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) != "Stage") return;
        jg = true;
    }

    private void OnTriggerExit(Collider col)
    {
        if (LayerMask.LayerToName(col.gameObject.layer) != "Stage") return;
        jg = false;
    }
}
