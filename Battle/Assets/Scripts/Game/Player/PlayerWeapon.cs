using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

    [SerializeField]
    private bool _weapon = false; // 武器持ってるか持ってないか
    [SerializeField]
    private int _attack = 0;

    private EquipManager _charEquipManager;

    private string[] _weaponPath = new string[] 
    {
        "t",
        "e",
        "s",
        "t"
    };

	// Use this for initialization
	void Start() {
        _charEquipManager = GetComponent<EquipManager>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerStay(Collider other)
    {
        
    }

}
