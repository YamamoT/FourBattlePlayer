using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerNum : MonoBehaviour {
    
    private int playerId;
    public GameObject[] Number;
    public GameObject player;
    public Slider _rayBar;

    [SerializeField]
    private float _correctSize;

    // Use this for initialization
    void Start () {
        playerId = player.GetComponent<PlayerStates>().PlayerID;
    }
	

	// Update is called once per frame
	void Update () {
        Number[playerId - 1].SetActive(true);
        transform.rotation = Camera.main.transform.rotation;

        transform.position = new Vector3(transform.position.x, transform.position.y, -6f);

        

    }
}
