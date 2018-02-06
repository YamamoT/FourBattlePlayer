using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {

    [SerializeField][Range(1, 4)]
    public int PlayerID; //プレイヤーナンバー

    [SerializeField][Range(0,300)]
    private int _hp; // 体力
    [SerializeField][Range(1f, 10f)]
    private float _speed; // 移動速度
    [SerializeField][Range(1f, 10f)]
    private float _jumpPower; // ジャンプ力
    [SerializeField]
    private float _nomalAttack; // 素手の攻撃力
    [SerializeField]
    private float _weaponAttack; // 武器の攻撃力
    [SerializeField]
    private float _bulletDense; // 弾の密集率
    [SerializeField][Range(0f, 5f)]
    private float _invincibleTime; // 無敵時間
    [SerializeField]
    private int _flinch = 15;
   
    private int _maxHp;

    GamepadInput.GamePad.Index _conNum;

    float _time; // 時間

    // 生死判定
    [SerializeField]
    bool _isDead = false;
    // 攻撃判定(攻撃中かどうか)
    [SerializeField]
    bool _isAttack = false;
    // ダメージ判定(無敵時間の処理とか作る用)
    [SerializeField]
    bool _isDamage = false;
    // 走っているか
    [SerializeField]
    bool _isDash = false;
    // ジャンプしているか
    [SerializeField]
    bool _isJump = false;
    // しゃがみかどうか
    [SerializeField]
    bool _isCrouch = false;
    [SerializeField]
    bool _isGround = false;
    bool _isTurn = false;

    SkinnedMeshRenderer skinMeshRen;
    MeshRenderer meshRen;
    List<GameObject> _list;

    private void Start()
    {
        _time = _invincibleTime; // 無敵時間の登録
        _list = GetAll(gameObject);
        PlayerNum();
        _maxHp = _hp;
    }
    private void Update()
    {
        if (_isDead) Destroy(this.gameObject);
    }

    private void FixedUpdate()
    {
        DamageExpression(_isDamage);
    }

    /// <summary>
    /// 全ての子要素をリストに入れ、取得する方法
    /// 参考サイト：http://kazuooooo.hatenablog.com/entry/2015/08/07/010938
    /// </summary>
    public static List<GameObject> GetAll(GameObject obj)
    {
        List<GameObject> allChild = new List<GameObject>();
        GetChildren(obj, ref allChild);
        return allChild;
    }
    public static void GetChildren(GameObject obj, ref List<GameObject> allChild)
    {
        Transform children = obj.GetComponentInChildren<Transform>();
        if (children.childCount == 0) return;

        foreach (Transform tObj in children)
        {
            allChild.Add(tObj.gameObject);
            GetChildren(tObj.gameObject, ref allChild);
        }
    }

    /// <summary>
    /// 一定ダメージを受けた時の無敵処理
    /// </summary>
    private void DamageExpression(bool isDamage)
    {

        if (!isDamage) return;

        _time -= Time.deltaTime;

        // ダメージ受けた時の無敵描画処理
        foreach (GameObject obj in _list)
        {
            // 武器は点滅しないようにする
            if (obj.tag == "Weapon") break;

            // メッシュの表示非表示処理
            skinMeshRen = obj.GetComponent<SkinnedMeshRenderer>();
            meshRen = obj.GetComponent<MeshRenderer>();
            if (skinMeshRen != null)skinMeshRen.enabled = !skinMeshRen.enabled;
            if (meshRen != null) meshRen.enabled = !meshRen.enabled;
        }
        if (_time <= 0f)
        {
            // 全メッシュの表示処理
            foreach (GameObject obj in _list)
            {
                skinMeshRen = obj.GetComponent<SkinnedMeshRenderer>();
                meshRen = obj.GetComponent<MeshRenderer>();
                if (skinMeshRen != null) skinMeshRen.enabled = true;
                if (meshRen != null) meshRen.enabled = true;
            }
            _time = _invincibleTime;
            _isDamage = false;
        }
    
}

    /// <summary>
    /// プレイヤーのコントローラ番号を設定する
    /// </summary>
    public void PlayerNum()
    {
        switch (PlayerID)
        {
            case 1:
                _conNum = GamepadInput.GamePad.Index.One;
                break;
            case 2:
                _conNum = GamepadInput.GamePad.Index.Two;
                break;
            case 3:
                _conNum = GamepadInput.GamePad.Index.Three;
                break;
            case 4:
                _conNum = GamepadInput.GamePad.Index.Four;
                break;
            default:
                _conNum = GamepadInput.GamePad.Index.Any;
                break;
        }
    }

    public GamepadInput.GamePad.Index ConNum
    {
        get { return _conNum; }
    }

    // 現在のHP
    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }

    // 初期HP
    public int MaxHp
    {
        get { return _maxHp; }
    }

    // 怯み値
    public int Flinch
    {
        get { return _flinch; }
        set { _flinch = value; }
    }

    // 移動速度
    public float Speed
    {
        get { return _speed; }
    }

    // ジャンプ力
    public float JumpPow
    {
        get { return _jumpPower; }
    }

    // パンチの攻撃力
    public float NomalATK
    {
        get { return _nomalAttack; }
    }

    // 銃の攻撃力
    public float WeaponATK
    {
        get { return _weaponAttack; }
    }

    // 武器の集弾率
    public float BulletDense
    {
        get { return _bulletDense; }
        set { _bulletDense = value; }
    }

    // 死亡判定
    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }

    // 攻撃判定
    public bool IsAttack
    {
        get { return _isAttack; }
        set { _isAttack = value; }
    }

    // ダメージ判定(怯み)
    public bool IsDamage
    {
        get { return _isDamage; }
        set { _isDamage = value; }
    }

    // 走っているか
    public bool IsDash
    {
        get { return _isDash; }
        set { _isDash = value; }
    }

    // ジャンプ中か
    public bool IsJump
    {
        get { return _isJump; }
        set { _isJump = value; }
    }

    // しゃがみ中か
    public bool IsCrouch
    {
        get { return _isCrouch; }
        set { _isCrouch = value; }
    }

    // 向きの判定(左右どちらを見ているか)
    public bool IsTrun
    {
        get { return _isTurn; }
        set { _isTurn = value; }
    }

    // 地面の上か
    public bool IsGround
    {
        get { return _isGround; }
        set { _isGround = value; }
    }
}
