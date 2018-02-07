using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// c_ が付いている変数は値が変更される

public class Weapon : MonoBehaviour
{
    // プレイヤー
    [SerializeField]
    private GameObject possesor;

    // 攻撃回数
    [SerializeField]
    private int attackValue;
    private int c_attackValue;

    // 攻撃間隔
    [SerializeField]
    private float attackInterval;
    private float c_attackInterval;

    // 威力
    [SerializeField]
    private float power;
    private float c_power;

    // 攻撃速度
    private float attackSpeed = 500;

    // 拡散率
    [SerializeField]
    private float diffusion;

    // 攻撃フラグ
    [SerializeField]
    private bool isAttack = false;

    // デバッグモード
    [SerializeField]
    private bool isDebugMode = false;

    // オプション ================================

    // ショットガン用 (一度に発射する弾の数)
    [SerializeField]
    private int shellInValue = 1;

    // 近接攻撃中かどうか
    private bool isMeleeAttack;

    //近接の持続時間
    [SerializeField]
    private float meleeDuration;
    private float c_meleeDuration;

    // 近接攻撃発生タイミング
    [SerializeField]
    private float occursTime;
    private float c_occursTime;

    // 近接攻撃が発生しているか
    private bool meleeIsOccurs = false;

    // 撃ちだす弾の大きさ
    [SerializeField]
    private float shotSize;
    private float c_shotSize = 0.1f;

    // チャージ時のダメージ上限
    [SerializeField]
    private float damageLimit;

    // チャージ時間
    private float chargeTime;
    // チャージ中に切り替わるまでの時間
    [SerializeField]
    private float changeTime;

    // チャージした状態かどうか
    private bool isCharge = false;

    // 18/02/02 更新分
    public float _chargeUpVal; // チャージダメージの上昇値
    public float _sizeUpVal; // チャージサイズの上昇値

    // 武器種類
    enum TYPE
    {
        Melee,
        Range,
        Special
    };

    // 現在の武器種
    [SerializeField]
    private TYPE type = TYPE.Range;

    // 弾
    [SerializeField]
    private GameObject bullet = null;

    // 銃口
    [SerializeField]
    private Transform muzzle = null;

    //発射エフェクト
    [SerializeField]
    private GameObject flash = null;

    //サウンド
    public AudioSource Sound;
    public AudioClip SE;
    public AudioClip SE2;


    /// <summary>
    /// 初期化
    /// </summary>
    void Start ()
    {
        Debug.Log("呼ばれた" + this.name);
        // 攻撃回数の初期化
        c_attackValue = attackValue;
        // 攻撃間隔の初期化
        c_attackInterval = attackInterval;
        // ダメージの初期化
        c_power = power;
        // 攻撃発生タイミングの初期化
        c_occursTime = occursTime;

        // 近接処理
        if(type == TYPE.Melee)
        {
            isMeleeAttack = false;
            if (possesor)
                gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            Debug.Log("melee以外");
            if (possesor)
            {
                Debug.Log("FixedMuzzle変更");
                muzzle.GetComponent<FixedMuzzle>().SetFixedPosition(true, true, false);
            }
                
        }

        // 剣の攻撃持続時間が攻撃間隔よりも長い時、持続時間を攻撃間隔と同じにする
        if(meleeDuration >= attackInterval)
        {
            meleeDuration = attackInterval;
        }
	}
	
	/// <summary>
    /// 更新
    /// </summary>
	protected void Update ()
    {
		// 攻撃中の処理
        if(isAttack)
        {
            // 攻撃間隔を減少
            c_attackInterval -= 6.0f * Time.deltaTime;
            
            // 攻撃間隔をリセット
            if(c_attackInterval <= 0)
            {
                isAttack = false;
                c_attackInterval = attackInterval;
            }
        }

        // 剣で攻撃中
        if(isMeleeAttack)
        {
            // 剣の持続時間を減らす
            c_meleeDuration -= Time.deltaTime;

            // 近接攻撃の持続時間が0になったらリセット
            if(c_meleeDuration <= 0.0f)
            {
                isMeleeAttack = false;
                c_meleeDuration = meleeDuration;
                gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        // 近接攻撃発生
        if(meleeIsOccurs)
        {
            c_occursTime -= Time.deltaTime;

            if(c_occursTime <= 0.0f)
            {
                isAttack = true;
                isMeleeAttack = true;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                meleeIsOccurs = false;
                c_occursTime = occursTime;

                //サウンド再生
                Sound.PlayOneShot(SE);
            }
        }

        // デバッグモード
        if(isDebugMode)
            DebugMode();
       
	}

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        if(!isAttack)
        {
            // 遠距離攻撃 (銃)
            if(type == TYPE.Range)
            {
                isAttack = true;

                if (c_attackValue <= 0)
                {
                    //サウンド再生
                    Sound.PlayOneShot(SE2);
                    return;
                }

                for (int i = 0; i < shellInValue; i++)
                {
                    GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;

                    bulletInstance.GetComponent<Rigidbody>().useGravity = false;

                    // 弾に銃毎のダメージを設定
                    bulletInstance.GetComponent<Bullet>().SetDamage((int)power);
                    // 射撃者の名前を設定
                    bulletInstance.GetComponent<Bullet>().SetPossesorId(possesor.GetComponent<PlayerStates>().PlayerID);

                    Vector3 force;

                    // 拡散率を設定
                    float diffusivity = Random.Range(-diffusion, diffusion);

                    // 弾速を設定
                    if (possesor == null)
                        force = gameObject.transform.forward * ((float)attackSpeed * 100f);
                    else
                        force = possesor.transform.forward * ((float)attackSpeed * 100f);

                    // 弾の弾速を設定
                    bulletInstance.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime);

                    // 弾の発射場所を設定
                    bulletInstance.transform.position = muzzle.position;

                    // 弾の拡散率を設定
                    bulletInstance.GetComponent<Bullet>().SetDeviation(diffusivity);
                }

                //パーティクル
                GameObject muzzleFlash = GameObject.Instantiate(flash) as GameObject;
                muzzleFlash.transform.position = muzzle.position;
                muzzleFlash.transform.localRotation = possesor.transform.localRotation;

                //サウンド再生
                Sound.PlayOneShot(SE);

                c_attackValue--;

            }
            // 近接攻撃 (剣)
            else if(type == TYPE.Melee)
            {
                //isAttack = true;
                //isMeleeAttack = true;
                //gameObject.GetComponent<BoxCollider>().enabled = true;
                meleeIsOccurs = true;
            }
            else if(type == TYPE.Special)
            {
                // 攻撃中
                isAttack = true;

                if (c_attackValue <= 0)
                {
                    //サウンド再生
                    Sound.PlayOneShot(SE2);
                    return;
                }
                // 弾を生成
                GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
                // 弾に重力を掛けないようにする
                bulletInstance.GetComponent<Rigidbody>().useGravity = false;
                // 弾にダメージを設定する
                bulletInstance.GetComponent<Bullet>().SetDamage((int)c_power);
                // 射撃者の名前を設定
                bulletInstance.GetComponent<Bullet>().SetPossesorId(possesor.GetComponent<PlayerStates>().PlayerID);
                // 弾にチャージした分だけの大きさを設定する
                bulletInstance.GetComponent<Transform>().localScale = new Vector3(c_shotSize, c_shotSize, c_shotSize);

                Debug.Log(c_shotSize);
                Vector3 force;

                // 弾速を設定
                if (possesor == null)
                    force = gameObject.transform.forward * ((float)attackSpeed * 100f);
                else
                    force = possesor.transform.forward * ((float)attackSpeed * 100f);

                // 弾に速度を設定
                bulletInstance.GetComponent<Rigidbody>().AddForce(force * Time.deltaTime);
                // 弾の発射位置を設定
                bulletInstance.transform.position = muzzle.position;

                c_power = power;
                c_shotSize = shotSize;
                changeTime = 0.0f;
                isCharge = false;

                //パーティクル
                GameObject muzzleFlash = GameObject.Instantiate(flash) as GameObject;
                muzzleFlash.transform.position = muzzle.position;
                muzzleFlash.transform.localRotation = possesor.transform.localRotation;

                //サウンド再生
                Sound.PlayOneShot(SE);

                c_attackValue--;
            }
        }
    }

    /// <summary>
    /// 武器の試運転用
    /// </summary>
    private void DebugMode()

    {
        // 現在の武器種が近接なら
        if (type == TYPE.Melee)
        {
            if (Input.GetKey(KeyCode.Space))
                Attack();
        }
        // 現在の武器種が遠距離なら
        else if (type == TYPE.Range)
        {
            if (Input.GetKey(KeyCode.Space))
                Attack();

            if (Input.GetKey(KeyCode.R))
                ReLoad();
        }
        // 現在の武器種が特殊なら
        else if (type == TYPE.Special)
        {
            if (Input.GetKey(KeyCode.Space))
                Charge(0.05f, 0.01f);

            if (Input.GetKeyUp(KeyCode.Space))
                Attack();
        }
    }
    /// <summary>
    /// 近接専用衝突判定
    /// </summary>
    /// <param name="other">衝突対象</param>
    public void OnTriggerEnter(Collider other)
    {
        if(type == TYPE.Melee && other.tag == "Player")
        {
            isMeleeAttack = false;
            c_meleeDuration = meleeDuration;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    /// <summary>
    /// レイガンの弾のダメージと弾の大きさを設定
    /// </summary>
    /// <param name="damage">ダメージ</param>
    /// <param name="shotSize">弾の大きさ</param>
    public void Charge(float damage, float size)
    {
        chargeTime += Time.deltaTime;

        if (chargeTime > changeTime)
            isCharge = true;

        if (isCharge)
            if (c_power < damageLimit)
            {
                c_power += damage;
                c_shotSize += size;
            }
    }

    /// <summary>
    /// 攻撃回数取得
    /// </summary>
    /// <returns></returns>
    public int GetAttackValue()
    {
        return c_attackValue;
    }

    /// <summary>
    /// リロード
    /// </summary>
    public void ReLoad()
    {
        c_attackValue = attackValue;

        if(type == TYPE.Special)
        {
            c_power = power;
            c_shotSize = shotSize;
            changeTime = 0.0f;
            isCharge = false;
        }
    }

    /// <summary>
    /// ダメージの補設定
    /// </summary>
    /// <param name="damage">元のパワーにどれだけ足し引きするか</param>
    public void SetDamage(int damage)
    {
        power += damage;
    }

    /// <summary>
    /// 銃の集弾率を設定
    /// </summary>
    /// <param name="diff">集弾率</param>
    /// <note> 0に近いほど集弾率が良い
    public void SetDiffusion(float diff)
    {
        diffusion = diff;
    }

    /// <summary>
    /// 近接のダメージ取得
    /// </summary>
    /// <returns>ダメージ</returns>
    public int GetDamage()
    {
        return (int)power;
    }

    /// <summary>
    /// 攻撃間隔の値を取する
    /// </summary>
    /// <returns>攻撃間隔の値</returns>
    public float GetAttackInterval()
    {
        return c_attackInterval;
    }

    /// <summary>
    /// 攻撃中か
    /// </summary>
    /// <returns></returns>
    public bool GetIsAttack()
    {
        return isAttack;
    }

    /// <summary>
    /// チャージ時に変わる弾のサイズ
    /// </summary>
    /// <returns>弾のサイズ</returns>
    public float ChargeSize
    {
        get{ return _sizeUpVal; }
        set { _sizeUpVal = value; }
    }
    /// <summary>
    /// チャージ時に変わる弾のダメージ
    /// </summary>
    /// <returns>弾のダメージ</returns>
    public float ChargeDamage
    {
        get { return _chargeUpVal; }
        set { _chargeUpVal = value; }
    }
    

    public float GetChargeSize()
    {
        return c_shotSize;
    }
}