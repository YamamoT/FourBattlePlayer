using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargeBar : MonoBehaviour {

    public GameObject _chargeBar;
    public GameObject _player;

    // 一度保管しておく武器
    private GameObject _weapon;
    private GameObject _stackBar;

    // チャージショット用のバー
    private Slider _rayBar;

	// Use this for initialization
	void Start () {
        _rayBar = _chargeBar.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        if (_weapon = null) return;

        _weapon = _player.GetComponent<PlayerWeapon>().Weapon;
        

        if (_weapon.name.Contains("Ray") && (_weapon.GetComponent<Weapon>().GetAttackValue() != 0))
        {
            _chargeBar.SetActive(true);
        }
        else
        {
            _chargeBar.SetActive(false);
            return;
        }

        _rayBar.value = (_weapon.GetComponent<Weapon>().GetChargeSize() / 1f);

        transform.rotation = Camera.main.transform.rotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, -6);
    }
}
