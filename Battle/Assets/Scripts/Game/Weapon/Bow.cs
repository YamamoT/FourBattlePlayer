using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Wepon
{
    // 弾
    public GameObject bullet;
    // 銃口
    public Transform muzzle;
    // 矢の重力
    public float arrowGravity = 1.0f;
    // チャージ中
    private bool isCharge = false;

    /// <summary>
    /// 初期化
    /// </summary>
    void Start()
    {
        Interval = attackInterval;
    }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {
        if (base.GetIsDebugMode())
        {
            if (Input.GetKey(KeyCode.Space))
            {
                isCharge = true;
                attackSpeed += 0.02f;
                damage += 1;
            }
                

            if (Input.GetKeyUp(KeyCode.Space))
            {
                Attack();
                isCharge = false;
                damage = 0;
                attackSpeed = 0.0f;
            }
                
        }

        Debug.Log(isCharge);
        Debug.Log(attackSpeed);
        //// 攻撃中
        //if (isAttack)
        //{
        //    // 攻撃間隔を減少
        //    Interval -= 0.1f;
        //    attackSpeed += 0.1f;

        //    // 攻撃間隔リセット
        //    if (Interval <= 0)
        //    {
        //        isAttack = false;
        //        Interval = attackInterval;
        //        attackSpeed = 0.0f;
        //    }
        //}

        //base.Update();
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        //base.Attack();
        // 攻撃中
        isAttack = true;

        GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().mass = arrowGravity;
        bulletInstance.GetComponent<Bullet>().SetDamage(base.GetDamage());

        Vector3 force;

        force = gameObject.transform.forward * attackSpeed * 1000;

        bulletInstance.GetComponent<Rigidbody>().AddForce(force);

        bulletInstance.transform.position = muzzle.position;
    }
}
