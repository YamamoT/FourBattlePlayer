using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponGeneration : MonoBehaviour {

    [SerializeField]
    private float _generationTime = 10;

    public GameObject[] _geneWeapon;
    private GameObject _genWep;
    
    float _stackTime;
    int _wepNum;

    // Use this for initialization
    void Start () {
        _wepNum = Random.Range(0, _geneWeapon.Length);
        _genWep = Instantiate(_geneWeapon[_wepNum], transform.position, transform.rotation);
        _genWep.AddComponent<SpinObj>();
        _stackTime = _generationTime;
    }
	
	// Update is called once per frame
	void Update () {
        if(_genWep == null) _generationTime -= Time.deltaTime;

        if(_generationTime <= 0)
        {
            _wepNum = Random.Range(0, _geneWeapon.Length);
            _genWep = Instantiate(_geneWeapon[_wepNum], transform.position, transform.rotation);
            _genWep.AddComponent<SpinObj>();
            _generationTime = _stackTime;
        }
    }
}
