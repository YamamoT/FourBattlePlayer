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

    // 弾の生存時間
    [SerializeField]
    private float lifeTime = 1.0f;

    //プレイヤー着弾エフェクト
    [SerializeField]
    private GameObject hitEffect = null;

    //その他着弾エフェクト
    [SerializeField]
    private GameObject spark = null;

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        gameObject.transform.Translate(new Vector3(0.0f, deviation, 0.0f));

        lifeTime -= 0.1f;

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
    /// 衝突判定
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("当たった : " + other.name);

        //// 衝突対象のタグがBulletでない場合
        //if (other.tag != "Bullet")
        //{
        //    Debug.Log("弾以外のものに当たった");
        //    Destroy(gameObject);
        //}
        //if (other.tag != "Weapon")
        //{
        //    Debug.Log("武器以外のものに当たった");
        //    Destroy(gameObject);
        //}

        if(other.tag != "Weapon")
        {
            if(other.tag != "Bullet")
            {
                if (other.tag == "Player")
                {
                    //パーティクル
                    GameObject effect = GameObject.Instantiate(hitEffect) as GameObject;
                    effect.transform.position = this.transform.position;
                }
                else
                {
                    //パーティクル
                    GameObject effect = GameObject.Instantiate(spark) as GameObject;
                    effect.transform.position = this.transform.position;
                }

                Destroy(gameObject);
            }
        }
    }

}
