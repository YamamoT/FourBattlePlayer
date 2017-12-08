using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Wepon
{
    // 弾
    public GameObject bullet;
    // 銃口
    public Transform muzzle;

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
            if (Input.GetKey(KeyCode.Space) && !isAttack)
                Attack();

        base.Update();
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    public void Attack()
    {
        base.Attack();

        GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
        bulletInstance.GetComponent<Rigidbody>().useGravity = false;
        bulletInstance.GetComponent<Bullet>().SetDamage(base.GetDamage());

        Vector3 force;
        float randamPos = Random.Range(-0.1f, 0.1f);

        force = gameObject.transform.forward  * attackSpeed * 1000;

        bulletInstance.GetComponent<Rigidbody>().AddForce(force);

        bulletInstance.transform.position = muzzle.position;
        bulletInstance.GetComponent<Bullet>().SetDeviation(randamPos);
    }
}
