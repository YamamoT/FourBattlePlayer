using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // ぶれ
    private float deviation = 0.0f;
    // 弾のダメージ
    [SerializeField]
    private int bulletDamage = 0;

    // 弾の速度
    [SerializeField]
    private float bulletSpeed;

    // 弾の生存時間
    [SerializeField]
    private float lifeTime = 1.0f;

    //プレイヤー着弾エフェクト
    [SerializeField]
    private GameObject hitEffect = null;

    //その他着弾エフェクト
    [SerializeField]
    private GameObject spark = null;

    // 射撃者の名前格納用
    private int possesorID;

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        gameObject.transform.Translate(new Vector3(0.0f, deviation, 0.0f));

        lifeTime -= Time.deltaTime * 0.1f;

        if(lifeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// ぶれ設定
    /// </summary>
    /// <param name="value"></param>
    public void SetDeviation(float value)
    {
        deviation = value;
    }

    /// <summary>
    /// 弾一発分のダメージを取得
    /// </summary>
    /// <param name="damage"></param>
    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    /// <summary>
    /// 弾一発分のダメージを取得
    /// </summary>
    /// <returns>bulletDamage</returns>
    public int GetDamage()
    {
        return bulletDamage;
    }

    /// <summary>
    /// 射撃者の名前設定
    /// </summary>
    /// <param name="name"></param>
    public void SetPossesorId(int id)
    {
        possesorID = id;
    }

    public int GetPossesorId()
    {
        return possesorID;
    }

    public void SetBulletSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    /// <summary>
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {

        if (LayerMask.LayerToName(other.gameObject.layer) == "Stage"||
            LayerMask.LayerToName(other.gameObject.layer) == "Gimmick"||
            other.tag == "Player")
        {
            if (other.tag == "Player" && 
                other.GetComponent<PlayerStates>().PlayerID != possesorID)
            {
                //パーティクル
                GameObject effect = GameObject.Instantiate(hitEffect) as GameObject;
                effect.transform.position = this.transform.position;
                Debug.Log("射撃者とは違う人に当たった" + other.name);
                Destroy(gameObject);
            }
            else
            {
                //パーティクル
                GameObject effect = GameObject.Instantiate(spark) as GameObject;
                effect.transform.position = this.transform.position;
                Destroy(gameObject);
                Debug.Log("Player以外に当たった" + other.name);
            }
        }
    }

    }
