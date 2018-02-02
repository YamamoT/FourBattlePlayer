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
    private Weapon weapon = null;
    // 武器画像
    [SerializeField]
    private Image[] WeaponsImage;
    enum Weapons
    {
        Fist,
        HandGun,
        MachineGun,
        ShotGun,
        RayGun,
        Sword
    }
    // 格納用攻撃回数
    private int attack;
    
    // 現在所持中の武器
    [SerializeField]
    private Image currentWeaponImage;

    // 残弾数表示用のテキスト
    [SerializeField]
    private Text bulletValueText = null;

    // HP上限保存用
    private int HpLimit;


    
	/// <summary>
    /// 初期化
    /// </summary>
	void Start ()
    {
		if(player)
        {

            // ゲージ初期化
            greenGauge.fillAmount = 1;
            redGauge.fillAmount = 1;

            // イメージスクリプト取得
            currentWeaponImage = currentWeaponImage.GetComponent<Image>();
            // テキスト初期化
            bulletValueText.text = "00";
            // HP初期化
            HpLimit = player.GetComponent<PlayerStates>().Hp;
            SetHp(player.GetComponent<PlayerStates>().Hp, HpLimit);
        }
    }
	
	/// <summary>
    /// 更新
    /// </summary>
	void Update ()
    {

        if (player)
        {
            SetWeapon(player.GetComponent<PlayerWeapon>().GetCurrentWeapon());

            //GaugeProcess();

            // 武器の切り替え表示
            WeponsPossessed();
            // 武器の残弾表示
            WeponRemainBullet();

            SetHp(player.GetComponent<PlayerStates>().Hp, HpLimit);
        }
        else
        {
            greenGauge.fillAmount = 0;
        }

        GaugeProcess();
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
        if (weapon)
        {
            if (weapon.name == "Fist")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.Fist].sprite;
            // ハンドガン
            else if (weapon.name == "HandGun")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.HandGun].sprite;
            // マシンガン
            else if (weapon.name == "MachineGun")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.MachineGun].sprite;
            // ショットガン
            else if (weapon.name == "ShotGun")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.ShotGun].sprite;
            // レイガン
            else if (weapon.name == "RayGun")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.RayGun].sprite;
            // 刀
            else if (weapon.name == "SamuraiSword")
                currentWeaponImage.sprite = WeaponsImage[(int)Weapons.Sword].sprite;
        }
    }

    /// <summary>
    /// 所持中の武器の残弾数
    /// </summary>
    private void WeponRemainBullet()
    {
        // 武器
        if(weapon)
        {
            // 現在装備している武器の攻撃回数を取得
            int attackValue = weapon.GetComponent<Weapon>().GetAttackValue();

            // 攻撃回数の値が0なら何も表示しない
            if (attackValue == 100)
                bulletValueText.text = "　";
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
    public void SetWeapon(Weapon weaponObject)
    {
        weapon = weaponObject;
    }

    public void SetPlayer(GameObject _player)
    {
        player = _player;
    }
}
