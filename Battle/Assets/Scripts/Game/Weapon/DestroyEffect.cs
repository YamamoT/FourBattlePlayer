using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour {

    //消滅までの時間
    public float count;

    //サウンド
    public AudioSource Sound;
    public AudioClip SE;

    // Use this for initialization
    void Start () {
        //サウンド再生
        Sound.PlayOneShot(SE);
    }
	
	// Update is called once per frame
	void Update () {
        Destroy(this.gameObject,count);
    }
}
