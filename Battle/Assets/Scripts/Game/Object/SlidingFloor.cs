using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingFloor : MonoBehaviour {

    private int _playerLayer;
    private int _slidingFloorLayer;

	// Use this for initialization
	void Start () {
        _playerLayer = LayerMask.NameToLayer("Player");
        _slidingFloorLayer = LayerMask.NameToLayer("Sliding");
	}

    private void OnChildTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponentInChildren<BoxCollider>().tag == "TriggerCollider")
            Physics.IgnoreLayerCollision(_playerLayer, _slidingFloorLayer, true);   
    }

    private void OnChildTriggerExit(Collider col)
    {
        if (col.gameObject.GetComponentInChildren<BoxCollider>().tag == "TriggerCollider")
            Physics.IgnoreLayerCollision(_playerLayer, _slidingFloorLayer,false);
    }
}
