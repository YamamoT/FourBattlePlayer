using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    private float attackSpeed;

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

    // 剣が攻撃中かどうか
    private bool isMeleeAttack;

    // 剣の攻撃持続時間
    [SerializeField]
    private float meleeDuration;
    private float c_meleeDuration;

    // 撃ちだす弾の大きさ
    [SerializeField]
    private float shotSize;
    private float c_shotSize;

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

	/// <summary>
    /// 初期化
    /// </summary>
	void Start ()
    {
        // 攻撃回数の初期化
        c_attackValue = attackValue;
        // 攻撃間隔の初期化
        c_attackInterval = attackInterval;
        // ダメージの初期化
        c_power = power;

        // 近接処理
        if(type == TYPE.Melee)
        {
            isMeleeAttack = false;
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
            c_attackInterval -= 0.1f;
            
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
            c_meleeDuration -= 0.1f;

            // 近接攻撃の持続時間が0になったらリセット
            if(c_meleeDuration <= 0.0f)
            {
                isMeleeAttack = false;
                c_meleeDuration = meleeDuration;
            }
        }

        // デバッグモード
        if(isDebugMode)
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
                {
                    chargeTime += 0.1f;

                    if (chargeTime > changeTime)
                    {
                        Charge(0.2f, 0.01f);
                    }

                    if (isCharge)
                        Attack();  
                }

                if (Input.GetKeyUp(KeyCode.Space))
                {
                    if (chargeTime > changeTime)
                        isCharge = true;

                    if (chargeTime < changeTime && !isCharge)
                        Attack();
                }
            }
        }
       
	}

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        if(!isAttack && c_attackValue > 0)
        {
            // 遠距離攻撃 (銃)
            if(type == TYPE.Range)
            {
                isAttack = true;

                for (int i = 0; i < shellInValue; i++)
                {
                    GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;

                    bulletInstance.GetComponent<Rigidbody>().useGravity = false;

                    // 弾に銃毎のダメージを設定
                    bulletInstance.GetComponent<Bullet>().SetDamage((int)power);

                    Vector3 force;

                    // 拡散率を設定
                    float diffusivity = Random.Range(-diffusion, diffusion);

                    // 弾速を設定
                    if (possesor == null)
                        force = gameObject.transform.forward * attackSpeed * 1000;
                    else
                        force = possesor.transform.forward * attackSpeed * 1000;

                    // 弾の弾速を設定
                    bulletInstance.GetComponent<Rigidbody>().AddForce(force);

                    // 弾の発射場所を設定
                    bulletInstance.transform.position = muzzle.position;

                    // 弾の拡散率を設定
                    bulletInstance.GetComponent<Bullet>().SetDeviation(diffusivity);
                }

                c_attackValue--;
            }
            // 近接攻撃 (剣)
            else if(type == TYPE.Melee)
            {
                isAttack = true;
                isMeleeAttack = true;
            }
            else if(type == TYPE.Special)
            {
                // 攻撃中
                isAttack = true;
                // 弾を生成
                GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
                // 弾に重力を掛けないようにする
                bulletInstance.GetComponent<Rigidbody>().useGravity = false;
                // 弾にダメージを設定する
                bulletInstance.GetComponent<Bullet>().SetDamage((int)c_power);
                // 弾にチャージした分だけの大きさを設定する
                bulletInstance.GetComponent<Transform>().localScale = new Vector3(c_shotSize, c_shotSize, c_shotSize);

                Vector3 force;

                // 弾速を設定
                if (possesor == null)
                    force = gameObject.transform.forward * attackSpeed * 1000;
                else
                    force = possesor.transform.forward * attackSpeed * 1000;

                // 弾に速度を設定
                bulletInstance.GetComponent<Rigidbody>().AddForce(force);
                // 弾の発射位置を設定
                bulletInstance.transform.position = muzzle.position;

                c_power = power;
                c_shotSize = shotSize;
                changeTime = 0.0f;
                isCharge = false;
            }
        }
    }

    /// <summary>
    /// 近接専用衝突判定
    /// </summary>
    /// <param name="other">衝突対象</param>
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && isMeleeAttack && isAttack)
        {
            isMeleeAttack = false;
            c_meleeDuration = meleeDuration;
        }
    }

    /// <summary>
    /// レイガンの弾のダメージと弾の大きさを設定
    /// </summary>
    /// <param name="damage">ダメージ</param>
    /// <param name="shotSize">弾の大きさ</param>
    public void Charge(float damage, float size)
    {
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
    }

    public void SetDamage(int damage)
    {
        power += damage;
    }

    /// <summary>
    /// 近接のダメージ取得
    /// </summary>
    /// <returns>ダメージ</returns>
    public int GetDamage()
    {
        return (int)power;
    }
}