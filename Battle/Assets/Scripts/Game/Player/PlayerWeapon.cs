﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField]
    private GameObject[] _weapons;

    [SerializeField]
    private bool _isWeapon = false; // 武器持ってるか持ってないか
    [SerializeField]
    private bool _isSword = false;
    [SerializeField]
    private int _attack = 0;

    Animator playerAnime;

    private EquipManager _charEquipManager;
    private PlayerStates pStates;

    private string[] _weaponPath = new string[] 
    {
        "t",
        "e",
        "s",
        "t"
    };


    GameObject hand;
	// Use this for initialization
	void Start() {
        playerAnime = GetComponent<Animator>();
        _charEquipManager = GetComponent<EquipManager>();

        for (int i = 0; _weapons.Length > i; i++)
        {
        //    if (_weapons[i].name.Contains("Hand")) _weapons[i].GetComponent<HandGun>();
        }

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; _weapons.Length > i; i++)
        {
            //if (_weapons[i].name.Contains("Hand")) _weapons[i].GetComponent<>();
        }


        if (_isWeapon)
        {

            // ここに武器持った時の攻撃
            

            if (Input.GetButtonDown("Throw"))
            {
                for (int i = 0; _weapons.Length > i; i++)
                {
                    _weapons[i].SetActive(false);
                }
                _isWeapon = false;
            }
        }
        else
        {
            //　武器持ってないときの攻撃
        }
        

        // アニメーション用
        if (_isWeapon)
        {
           
           if(_isSword)
            {
                //アニメーション(武器[剣]持っているとき)
                playerAnime.SetBool("sword", true);
            }
            else
            {
                // アニメーション(武器[銃]持ってる時)
                playerAnime.SetBool("gun", true);
            }
        }
        else
        {
            // アニメーション(武器持ってないとき)
            playerAnime.SetBool("gun", false);
            playerAnime.SetBool("sword", false);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Weapon")
        {
            if(Input.GetAxisRaw("Vertical") < 0) 
            {
                if (!_isWeapon)
                {
                    _isWeapon = true;

                    string wepName = col.gameObject.name;
                    for (int i = 0; _weapons.Length > i; i++)
                    {
                        if (wepName.Contains(_weapons[i].name)) _weapons[i].SetActive(true);
                    }

                    Destroy(col.gameObject);
                }
            }    
        }
    }

}
