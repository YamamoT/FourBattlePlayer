using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeponGeneration : MonoBehaviour {

    [SerializeField]
    private float _generationTime = 10;

    public GameObject[] _genePos;
    private int _posNum;

    public GameObject[] _geneWeapon;
    private GameObject _genWep;
    
    float _stackTime;

    // ステージに生成したい武器の最大値
    public int _generationWeaponNum;
    // 生成した武器の数
    int _geneWepNum;

    int _wepNum;

    // Use this for initialization
    void Start () {
        _wepNum = Random.Range(0, _geneWeapon.Length);
        _genWep = Instantiate(_geneWeapon[_wepNum], transform.position, transform.rotation);
        _genWep.AddComponent<SpinObj>();
        _stackTime = Random.Range(0, 10f) + 10f;
        _stackTime = _generationTime;
    }
	
	// Update is called once per frame
	void Update () {

        //for (int i = 0; _geneWepNum == _generationWeaponNum;i++)
        //{
        //    // 生成する座標番号
        //    _posNum = Random.Range(0, _genePos.Length);
        //    Debug.Log("生成する番号：" + _posNum);
        //    // 武器が未生成タグなら生成済タグに
        //    if (_genePos[_posNum].tag == "NoGeneration")
        //    {
        //        _genePos[_posNum].tag = "Generation";
        //    }
        //    else
        //    {
        //        continue; // 生成状態ならもう一度
        //    }
        //    _genePos[_posNum] = Instantiate(_geneWeapon[_wepNum], _genePos[_posNum].transform.position, _genePos[_posNum].transform.rotation);
        //    _genePos[_posNum].AddComponent<SpinObj>();
        //    _geneWepNum++; // 生成数を増やす
        //}

        if (_genWep == null) _generationTime -= Time.deltaTime;

        if (_generationTime <= 0)
        {
            _wepNum = Random.Range(0, _geneWeapon.Length);
            _genWep = Instantiate(_geneWeapon[_wepNum], transform.position, transform.rotation);
            _genWep.AddComponent<SpinObj>();
            _generationTime = _stackTime;
        }
    }
}
