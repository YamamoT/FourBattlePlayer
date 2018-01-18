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
    private PlayerStates pStates;
    private GameObject activeWeapon = null;

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
        Debug.Log("_isWeapon :" + _isWeapon);

        if (_isWeapon)
        {
            // 武器が非アクティブなら処理終了
            if (activeWeapon == null) return;

            // 武器攻撃
            if (Input.GetButton("Attack")) activeWeapon.GetComponent<Weapon>().Attack();

            // 武器を捨てる
            if (Input.GetButtonDown("Throw"))
            {
                activeWeapon.SetActive(false);
                activeWeapon = null; // 武器を空に
                _isWeapon = false; // 武器を持っていない状態にする
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
            if(Input.GetAxisRaw("Vertical") < -0.9f) 
            {
                if (!_isWeapon)
                {
                    _isWeapon = true;

                    string wepName = col.gameObject.name;
                    for (int i = 0; _weapons.Length > i; i++)
                    {
                        if (wepName.Contains(_weapons[i].name))
                        {
                            activeWeapon = _weapons[i];
                            activeWeapon.SetActive(true);
                        }
                    }

                    Destroy(col.gameObject);
                }
            }    
        }
    }

}
