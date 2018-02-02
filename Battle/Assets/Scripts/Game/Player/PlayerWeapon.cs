using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {
    
    public GamepadInput.GamepadState keyState; //キー情報
    public GamepadInput.GamepadState Trigger; //トリガー処理用(書き方の例→ && !Trigger.X)
    Vector2 axis; //スティック情報

    [SerializeField]
    private GameObject[] _weapons;
    [SerializeField]
    private GameObject _fist;

    private Weapon _weaponFunc;

    [SerializeField]
    private bool _isWeapon = false; // 武器持ってるか持ってないか
    [SerializeField]
    private bool _isSword = false;
    [SerializeField]
    private int _attack = 0;

    Animator playerAnime;
    
    private PlayerStates pStates;
    private GameObject activeWeapon = null;

    GameObject camera;
    ZoomCamera zoomCamera;

    // Use this for initialization
    void Start() {
        pStates = GetComponent<PlayerStates>();
        playerAnime = GetComponent<Animator>();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        zoomCamera = camera.GetComponent<ZoomCamera>();

    }

    // Update is called once per frame
    void Update () {

        if (zoomCamera.GetIsFinished() == false) return;

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(pStates.ConNum, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, pStates.ConNum, false);
        
        if (activeWeapon == null) activeWeapon = _fist;

        _weaponFunc = activeWeapon.GetComponent<Weapon>();

        if (_isWeapon)
        {
            // 武器が非アクティブなら処理終了
            if (activeWeapon == null) return;

            // 武器攻撃
            if (activeWeapon.name.Contains("Machine"))
            {
                if(keyState.X && !pStates.IsDamage)
                {
                    if(_weaponFunc.GetIsAttack() == false)
                    {
                        _weaponFunc.Attack();
                        playerAnime.SetTrigger("attack");
                    }
                }
            }
            else if (activeWeapon.name.Contains("Ray") && !pStates.IsDamage)
            {
                if(keyState.X)_weaponFunc.Charge(_weaponFunc.ChargeDamage, _weaponFunc.ChargeSize);

                if (GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.X, pStates.ConNum))
                {
                    _weaponFunc.Attack();
                    playerAnime.SetTrigger("attack");
                }
            }
            else
            {
                if (keyState.X && !Trigger.X && !pStates.IsDamage)
                {
                    activeWeapon.GetComponent<Weapon>().Attack();
                    playerAnime.SetTrigger("attack");
                }
            }
           
            // 武器を捨てる
            if (keyState.LeftShoulder)
            {
                activeWeapon.GetComponent<Weapon>().ReLoad();
                activeWeapon.SetActive(false);
                activeWeapon = null; // 武器を空に
                _isWeapon = false; // 武器を持っていない状態にする
            }
        } 
        else
        {
            //　武器持ってないときの攻撃
            if (keyState.X && !Trigger.X && !pStates.IsDamage)
            {
                if (activeWeapon == _fist)
                {
                    if (activeWeapon.GetComponent<Weapon>().GetIsAttack() == false)
                    {
                        activeWeapon.GetComponent<Weapon>().Attack();
                        playerAnime.SetTrigger("attack");
                    }
                }
            }
        }

        //トリガー処理(キー操作系処理はここより上に書く)
        Trigger = keyState;

        // アニメーション用
        if (_isWeapon)
        {
           if(activeWeapon.name.Contains("Sword"))
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
            if(axis.y < -0.9f) 
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

    public Weapon GetCurrentWeapon()
    {
        return _weaponFunc;
    }
}
