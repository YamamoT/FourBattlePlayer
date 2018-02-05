using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    public GameObject[] _stage;

    private int _stageNum;

	// Use this for initialization
	void Awake () {

        _stageNum = Random.Range(0, _stage.Length);

        Instantiate(_stage[_stageNum]);
        		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
