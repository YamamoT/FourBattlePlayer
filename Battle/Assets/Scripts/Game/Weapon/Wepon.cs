using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 武器の共通仕様
// ・武器は使い切りで消滅する

public class Wepon : MonoBehaviour
{
    [SerializeField]
    private bool isDebugMode = true;

    // 使用回数 ( 飛び道具の場合は弾数 )
    public int attackValue;
    // 攻撃間隔
    protected float Interval;
    // 攻撃間隔セーブ
    public float attackInterval;
    // ダメージ
    public int damage;
    // 攻撃速度 (飛び道具の場合は、弾速)
    public float attackSpeed;

    // 使いきったか
    protected bool isFinished = false;
    // 攻撃中か
    protected bool isAttack = false;

    protected void Update()
    {
        // 攻撃中
        if (isAttack)
        {
            // 攻撃間隔を減少
            Interval -= 0.1f;

            // 攻撃間隔リセット
            if (Interval <= 0)
            {
                isAttack = false;
               Interval = attackInterval;
            }
        }

        // 使用終了
        if (attackValue <= 0)
            Destroy(gameObject);
    }

    protected void Attack()
    {
        // 攻撃中
        isAttack = true;

        // 使用回数を減少
        attackValue--;

        Debug.Log("攻撃");
    }

    protected float GetInterval()
    {
        return Interval;
    }

    protected bool GetIsAttack()
    {
        return isAttack;
    }

    protected bool GetIsDebugMode()
    {
        return isDebugMode;
    }

    public int GetDamage()
    {
        return damage;
    }
}
