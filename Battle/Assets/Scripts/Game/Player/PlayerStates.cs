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

    bool _isTurn = false;

    List<GameObject> _list;

    private void Start()
    {
        _time = _invincibleTime; // 無敵時間の登録
        
        _list = GetAll(gameObject);
    }

    private void Update()
    {
        // ダメージ発生時の無敵時間
        if(_isDamage)
        {
            _time -= Time.deltaTime;

            // ダメージ受けた時の無敵描画処理
            foreach (GameObject obj in _list)
            {
                if (obj.GetComponent<SkinnedMeshRenderer>() != null)
                {
                    obj.GetComponent<SkinnedMeshRenderer>().enabled = !obj.GetComponent<SkinnedMeshRenderer>().enabled;
                }
                if (obj.GetComponent<MeshRenderer>() != null)
                {
                    obj.GetComponent<MeshRenderer>().enabled = !obj.GetComponent<MeshRenderer>().enabled;
                }
            }

            if (_time <= 0f)
            {
                foreach (GameObject obj in _list)
                {

                    if (obj.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        obj.GetComponent<SkinnedMeshRenderer>().enabled = true;
                    }
                    if (obj.GetComponent<MeshRenderer>() != null)
                    {
                        obj.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
                _time = _invincibleTime;
                _isDamage = false;
            }
        }
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
    public bool IsTrun
    {
        get { return _isTurn; }
        set { _isTurn = value; }
    }
}
