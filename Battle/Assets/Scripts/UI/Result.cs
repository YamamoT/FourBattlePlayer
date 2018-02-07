using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour {

    public Text text;
    public float time;
    private float count;
    private bool EndFlag = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (EndFlag == false)
        {
            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            if (Players.Length == 1)
            {
                text.text = Players[0].GetComponent<PlayerStates>().PlayerID + "P WON";
                EndFlag = true;
            }
            else if (Players.Length <= 0)
            {
                text.text = "DRAW";
                EndFlag = true;
            }
        }
        else
        {
            count+= Time.deltaTime;
            if (count > time)
            {
                gManager.instance.SetCurrentState(SceneState.Title);
            }

        }

    }
}
