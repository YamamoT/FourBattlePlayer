using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGauge : MonoBehaviour
{

    // 対象となるプレイヤー
    [SerializeField]
    private GameObject player;
    // プレイヤーの現在HP
    private int currentHp;
    // プレイヤーのHP上限
    private int hpLimit;


    // 緑ゲージ
    [SerializeField]
    private Image greenGauge;
    // 赤ゲージ
    [SerializeField]
    private Image redGauge;

    // 所持中の武器
    [SerializeField]
    private GameObject wepon = null;
    // 武器画像
    [SerializeField]
    private Image[] WeponsImage;
    enum Wepons
    {
        Fist,
        HandGun,
        MachineGun,
        ShotGun,
        Bow,
        Sword
    }
    // 格納用攻撃回数
    private int attack;
    
    // 現在所持中の武器
    [SerializeField]
    private Image currentWeponImage;

    // 残弾数表示用のテキスト
    [SerializeField]
    private Text bulletValueText = null;

    
	/// <summary>
    /// 初期化
    /// </summary>
	void Start ()
    {
		if(player)
        {
            // イメージスクリプト取得
            currentWeponImage = currentWeponImage.GetComponent<Image>();
            // テキスト初期化
            bulletValueText.text = "00";
        }
    }
	
	/// <summary>
    /// 更新
    /// </summary>
	void Update ()
    {
        GaugeProcess();

        // 武器の切り替え表示
        WeponsPossessed();
        // 武器の残弾表示
        WeponRemainBullet();
    }

   /// <summary>
   /// ゲージ処理
   /// </summary>
    private void GaugeProcess()
    {
        // 緑ゲージ
        greenGauge.fillAmount = ((float)currentHp / (float)hpLimit);

        // 赤ゲージ減少
        if (redGauge.fillAmount > greenGauge.fillAmount)
            redGauge.fillAmount -= 0.003f;
    }

    /// <summary>
    /// 武器の切替表示
    /// </summary>
    public void WeponsPossessed()
    {
        if(wepon)
        {
             // ハンドガン
            if (wepon.name == "HandGun")
                currentWeponImage.sprite = WeponsImage[(int)Wepons.HandGun].sprite;
            // マシンガン
            else if (wepon.name == "MachineGun")
                currentWeponImage.sprite = WeponsImage[(int)Wepons.MachineGun].sprite;
            // ショットガン
            else if (wepon.name == "ShotGun")
                currentWeponImage.sprite = WeponsImage[(int)Wepons.ShotGun].sprite;
            // 弓
            else if (wepon.name == "Bow")
                currentWeponImage.sprite = WeponsImage[(int)Wepons.Bow].sprite;
            // 刀
            else if (wepon.name == "SamuraiSword")
                currentWeponImage.sprite = WeponsImage[(int)Wepons.Sword].sprite;
        }
        // 武器を持っていない
        else
            currentWeponImage.sprite = WeponsImage[(int)Wepons.Fist].sprite;
           
           
    }

    /// <summary>
    /// 所持中の武器の残弾数
    /// </summary>
    private void WeponRemainBullet()
    {
        // 武器
        if(wepon)
        {
            // 現在装備している武器の攻撃回数を取得
            int attackValue = wepon.GetComponent<Weapon>().GetAttackValue();

            // 攻撃回数の値が0なら何も表示しない
            if (attackValue == 100)
                bulletValueText.text = "∞";
            // 攻撃回数の値をテキストに
            else
                bulletValueText.text = attackValue.ToString();
        }
        else
            bulletValueText.text = "　";
    }


    /// <summary>
    /// HP設定
    /// </summary>
    /// <param name="hpValue">現在のHP</param>
    /// <param name="limitValue">HP上限</param>
    public void SetHp(int hpValue, int limitValue)
    {
        currentHp = hpValue;
        hpLimit = limitValue;
    }

    /// <summary>
    /// 所持中の武器設定
    /// </summary>
    /// <param name="weponObject">所持中の武器</param>
    public void SetWepon(GameObject weponObject)
    {
        wepon = weponObject;
    }
}
