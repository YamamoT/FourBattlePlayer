using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    [SerializeField]
    private GameObject _handGun;
    [SerializeField]
    private GameObject _shotGun;
    [SerializeField]
    private GameObject _machineGun;
    [SerializeField]
    private GameObject _sword;

    [SerializeField]
    private bool _isWeapon = false; // 武器持ってるか持ってないか
    [SerializeField]
    private bool _isSword = false;
    [SerializeField]
    private int _attack = 0;

    Animator playerAnime;

    private EquipManager _charEquipManager;

    private string[] _weaponPath = new string[] 
    {
        "t",
        "e",
        "s",
        "t"
    };

	// Use this for initialization
	void Start() {
        playerAnime = GetComponent<Animator>();
        _charEquipManager = GetComponent<EquipManager>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("g")) _shotGun.SetActive(true);
        if (Input.GetKeyDown("f")) _shotGun.SetActive(false);


        if (_isWeapon)
        {
           
           if(_isSword)
            {
                //アニメーション(武器[剣]持っているとき)
            }
            else
            {
                // アニメーション(武器[銃]持ってる時)
            }
        }
        {
           // アニメーション(武器持ってないとき)
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "Weapon")
        {
            if (!_isWeapon)
            {
                _isWeapon = true;

                string wepName = col.gameObject.name;

                if (wepName.Contains("Hand")) _handGun.SetActive(true);
                else if (wepName.Contains("Machine")) _machineGun.SetActive(true);
                else if (wepName.Contains("Shot")) _shotGun.SetActive(true);
                else if (wepName.Contains("Sword")) _sword.SetActive(true);

                Destroy(col.gameObject);
            }
        }
    }

}
