using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBubbling : MonoBehaviour {

    /// <summary>
    /// 下から貫通できる床・上から下にすり抜けも可能
    /// 参考サイト：http://gomafrontier.com/unity/1387
    /// </summary>

    public bool _callEnter;
    public bool _callExit;

    private GameObject _parent;

    // Use this for initialization
    void Start () {
        _parent = transform.parent.gameObject; // 親 
      }

    private void OnTriggerEnter(Collider col)
    {
        if (_callEnter) _parent.SendMessage("OnChildTriggerEnter", col);
    }

    private void OnTriggerExit(Collider col)
    {
        if (_callExit) _parent.SendMessage("OnChildTriggerExit", col);
    }
}
