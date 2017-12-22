using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates : MonoBehaviour {

    [SerializeField][Range(0,100)]
    private int _hp; // 体力
    [SerializeField][Range(1f, 10f)]
    private float _walkSpead; // 移動速度(歩き)
    [SerializeField][Range(1f, 10f)]
    private float _dushSpead; // 移動速度(走り)
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

    float _time; // 時間

    // 生死判定
    bool _isDead = false;
    // 攻撃判定(攻撃中かどうか)
    bool _isAttack = false;
    // ダメージ判定(無敵時間の処理とか作る用)
    bool _isDamage = false;
    // 走っているか
    bool _isDash = false;
    // ジャンプしているか
    bool _isJump = false;
    // しゃがみかどうか
    bool _isCrouch = false;

    private void Start()
    {
        _time = _invincibleTime; // 無敵時間の登録
    }

    private void Update()
    {
        // ダメージ発生時の無敵時間
        if(_isDamage)
        {
            _time -= Time.deltaTime;
            if (_time <= 0f)
            {
                _time = _invincibleTime;
                _isDamage = false;
            }
        }
    }

    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
    }
    
    public float WalkSpd
    {
        get { return _walkSpead; }
    }
    public float DushSpd
    {
        get { return _dushSpead; }
    }
    public float JumpPow
    {
        get { return _jumpPower; }
    }
    public float NomalATK
    {
        get { return _nomalAttack; }
    }
    public float WeaponATK
    {
        get { return _weaponAttack; }
    }
    public float BulletDense
    {
        get { return _bulletDense; }
        set { _bulletDense = value; }
    }
    public bool IsDead
    {
        get { return _isDead; }
        set { _isDead = value; }
    }
    public bool IsAttack
    {
        get { return _isAttack; }
        set { _isAttack = value; }
    }
    public bool IsDamage
    {
        get { return _isDamage; }
        set { _isDamage = value; }
    }
    public bool IsDash
    {
        get { return _isDash; }
        set { _isDash = value; }
    }
    public bool IsJump
    {
        get { return _isJump; }
        set { _isJump = value; }
    }
    public bool IsCrouch
    {
        get { return _isCrouch; }
        set { _isCrouch = value; }
    }
}
