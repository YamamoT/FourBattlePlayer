using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    private int PlayerID;
    GamepadInput.GamePad.Index playerNo; //コントローラナンバー
    public GamepadInput.GamepadState keyState; //キー情報
    public GamepadInput.GamepadState Trigger; //トリガー処理用(書き方の例→ && !Trigger.X)
    Vector2 axis; //スティック情報

    [SerializeField]
    private GameObject[] _weapons;
    [SerializeField]
    private GameObject _fist;

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
    
    // Use this for initialization
	void Start() {
        //コントローラ情報取得
        PlayerID = this.GetComponent<PlayerStates>().PlayerID;
        switch (PlayerID)
        {
            case 1:
                playerNo = GamepadInput.GamePad.Index.One;
                break;
            case 2:
                playerNo = GamepadInput.GamePad.Index.Two;
                break;
            case 3:
                playerNo = GamepadInput.GamePad.Index.Three;
                break;
            case 4:
                playerNo = GamepadInput.GamePad.Index.Four;
                break;
            default:
                playerNo = GamepadInput.GamePad.Index.Any;
                break;
        }



        playerAnime = GetComponent<Animator>();
        _charEquipManager = GetComponent<EquipManager>();
    }
	
	// Update is called once per frame
	void Update () {

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(playerNo, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, playerNo, false);

        Debug.Log(keyState);

        if (activeWeapon == null) activeWeapon = _fist;

        if (_isWeapon)
        {
            // 武器が非アクティブなら処理終了
            if (activeWeapon == null) return;

            // 武器攻撃
            if (keyState.X || keyState.Y)
            {
                activeWeapon.GetComponent<Weapon>().Attack();
                playerAnime.SetTrigger("attack");
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
            if (keyState.X || keyState.Y)
            {
                if (activeWeapon == _fist)
                {
                    activeWeapon.GetComponent<Weapon>().Attack();
                    playerAnime.SetTrigger("attack");
                }
            }
        }

        //トリガー処理(キー操作系処理はここより上に書く)
        Trigger = keyState;

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
}
