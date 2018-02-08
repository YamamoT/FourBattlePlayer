using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour {

    //サウンド
    public AudioSource Sound;
    public AudioClip BGM;

    // Use this for initialization
    void Start () {
        //サウンド再生
        Sound.Play();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
