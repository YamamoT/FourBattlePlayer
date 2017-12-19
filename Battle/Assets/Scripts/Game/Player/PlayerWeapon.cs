using System.Collections;
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

        // アニメーション用
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
        else
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

                for (int i = 0; _weapons.Length > i; i++)
                {
                    if (wepName.Contains(_weapons[i].name))
                    {
                        _weapons[i].SetActive(true);
                    }
                }
                Destroy(col.gameObject);
            }
        }
    }

}
