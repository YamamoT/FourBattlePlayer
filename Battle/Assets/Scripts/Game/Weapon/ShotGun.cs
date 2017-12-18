using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Wepon
{
    // 弾
    [SerializeField]
    private GameObject bullet;
    // 銃口
    [SerializeField]
    private Transform muzzle;
    // 一度に発射する弾の数
    [SerializeField]
    private int shellInValue;

    // 拡散範囲
    [SerializeField]
    private float diffusivity;

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
        if(base.GetIsDebugMode())
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
        if (isAttack && GetAttackValue() >= 0)
        {
            for (int i = 0; i < shellInValue; i++)
            {
                GameObject bulletInstance = GameObject.Instantiate(bullet) as GameObject;
                bulletInstance.GetComponent<Rigidbody>().useGravity = false;
                bulletInstance.GetComponent<Bullet>().SetDamage(base.GetDamage());

                Vector3 force;
                float randamPos = Random.Range(-diffusivity, diffusivity);

                force = gameObject.transform.forward * attackSpeed * 1000;

                bulletInstance.GetComponent<Rigidbody>().AddForce(force);

                bulletInstance.transform.position = muzzle.position;
                bulletInstance.GetComponent<Bullet>().SetDeviation(randamPos);
            }
        }
    }
}
