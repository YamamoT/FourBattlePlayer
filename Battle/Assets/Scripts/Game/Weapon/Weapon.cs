using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// c_ が付いている変数は値が変更される

public class Weapon : MonoBehaviour
{
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
    private float swordDuration;
    private float c_swordDuration;

    // 弓
    [SerializeField]
    private float arrowGravity = 1.0f;


    // 攻撃種類
    enum TYPE
    {
        Melee,
        Range,
        Special
    };
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

        if(type == TYPE.Melee)
        {
            isMeleeAttack = false;
        }

        // 剣の攻撃持続時間が攻撃間隔よりも長い時、持続時間を攻撃間隔と同じにする
        if(swordDuration >= attackInterval)
        {
            swordDuration = attackInterval;
        }
	}
	
	/// <summary>
    /// 更新
    /// </summary>
	protected void Update ()
    {
		// 攻撃間隔の処理
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
            c_swordDuration -= 0.1f;

            if(c_swordDuration <= 0.0f)
            {
                isMeleeAttack = false;
                c_swordDuration = swordDuration;
            }
        }

        if(isDebugMode)
        {

            if (type == TYPE.Melee)
            {
                if (Input.GetKey(KeyCode.Space))
                    Attack();
            }
            else if (type == TYPE.Range)
            {
                if (Input.GetKey(KeyCode.Space))
                    Attack();

                if (Input.GetKey(KeyCode.R))
                    ReLoad();
            }
            else if (type == TYPE.Special)
            {
                if (Input.GetKey(KeyCode.Space))
                    ArrowSetStatus(0.2f, 0.02f);

                if (Input.GetKeyUp(KeyCode.Space))
                    Attack();
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
                    force = gameObject.transform.forward * attackSpeed * 1000;

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

                GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
                bulletInstance.GetComponent<Rigidbody>().mass = arrowGravity;
                bulletInstance.GetComponent<Bullet>().SetDamage((int)power);

                Vector3 force;

                force = gameObject.transform.forward * attackSpeed * 1000;

                bulletInstance.GetComponent<Rigidbody>().AddForce(force);

                bulletInstance.transform.position = muzzle.position;

                power = 0;
                attackSpeed = 0.0f;
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
            c_swordDuration = swordDuration;
        }
    }

    /// <summary>
    /// 弓の攻撃速度とダメージを設定 (チャージ)
    /// </summary>
    /// <param name="damage">ダメージ</param>
    /// <param name="speed">攻撃速度</param>
    /// note : 弓の攻撃時はEnterを押している時はこの関数、離した時にAttackを呼んでほしい
    public void ArrowSetStatus(float damage, float speed)
    {
        power += damage;
        attackSpeed += speed;
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
}
