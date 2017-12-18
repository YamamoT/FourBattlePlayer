using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : Wepon
{
    // 弾
    [SerializeField]
    private GameObject bullet;
    // 銃口
    [SerializeField]
    private Transform muzzle;
    // 矢の重力
    [SerializeField]
    private float arrowGravity = 1.0f;
    // チャージ中
    [SerializeField]
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
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
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
