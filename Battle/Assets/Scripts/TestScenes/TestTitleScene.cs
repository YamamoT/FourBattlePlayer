using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTitleScene : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            gManager.instance.SetCurrentState(SceneState.Play);
        }
        else if(Input.GetKeyDown(KeyCode.R))
        {
            gManager.instance.SetCurrentState(SceneState.Result);
        }
        else if(Input.GetKeyDown(KeyCode.T))
        {
            gManager.instance.SetCurrentState(SceneState.Title);
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {
            gManager.instance.SetCurrentState(SceneState.Config);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            gManager.instance.SetCurrentState(SceneState.Select);
        }
            
	}
}
