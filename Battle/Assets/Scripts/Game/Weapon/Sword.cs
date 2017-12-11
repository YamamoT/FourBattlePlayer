using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Wepon
{
    // 攻撃している時間
    private float time = 0.0f;
    [SerializeField]
    private float attackTime = 0.0f;

    private bool isSwordAttack = false;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        Interval = attackInterval;
        time = attackTime;

        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        if (base.GetIsDebugMode())
            if (Input.GetKey(KeyCode.Space) && !isAttack)
                Attack();

        base.Update();

        // 剣で攻撃中
        if(isSwordAttack)
        {
            time -= 0.1f;

            // リセット
            if(time <= 0.0f)
            {
                isSwordAttack = false;
                time = attackTime;

               // 衝突判定をfalseに
               gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        isAttack = true;

        isSwordAttack = true;

        // 衝突判定をtrueに
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    /// <summary>
    /// ダメージ取得
    /// </summary>
    /// <returns></returns>
    public int GetDamage()
    {
        return base.GetDamage();
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(isSwordAttack);

        if(other.tag == "DebugObject" && isSwordAttack)
        {
            // ダメージ処理

            isSwordAttack = false;
            time = attackTime;

            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
