using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour {

    public string WeaponTemplateName;

    private GameObject _weaponTemplateName;
    private GameObject _weapon = null;

	// Use this for initialization
	void Start () {
        var children = GetComponentInChildren<Transform>(true);
        foreach(Transform transform in children)
        {
            if(transform.name == WeaponTemplateName)
            {
                _weaponTemplateName = transform.gameObject;
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipWeapons(string name)
    {
        if(_weapon != null)
        {
            Destroy(_weapon);
            _weapon = null;
            Resources.UnloadUnusedAssets();
        }
        _weapon = Instantiate(Resources.Load("Prefab/" + name), _weaponTemplateName.transform.position, _weaponTemplateName.transform.rotation) as GameObject;
        _weapon.transform.parent = _weaponTemplateName.transform;
    }
}
