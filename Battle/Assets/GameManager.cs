using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public Timer _timer;
    public ZoomCamera _zoomCamera;
    int count = 0;

    GameObject[] _tagobjs;

    float _nowHp = 0;
    float _pMaxHp = 0;

    float _maxHp = 0f;
    float _hpRate = 0f;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (_zoomCamera.GetIsFinished() == false) return;

        if(count == 0)
        {
            _timer.TimerSwitch = true;
            count++;
        }

        Debug.Log(_maxHp);
        
        if (_timer.GetIsFinished() == false) return;

        if (GameObject.FindGameObjectsWithTag("Player") == null) return;

        _tagobjs = GameObject.FindGameObjectsWithTag("Player");

        if (_tagobjs.Length == 0) return;

        for (int i = 0; i < _tagobjs.Length;i++)
        {
            
            _hpRate = ((float)_tagobjs[i].GetComponent<PlayerStates>().Hp / (float)_tagobjs[i].GetComponent<PlayerStates>().MaxHp) * 100;

            if (_maxHp == 0f)
            {
                _maxHp = _hpRate;
                break;
            }

            if(_maxHp > _hpRate)
            {
                _tagobjs[i].GetComponent<PlayerStates>().Hp = 0;
            }
            else if(_maxHp < _hpRate)
            {
                _maxHp = ((float)_tagobjs[i].GetComponent<PlayerStates>().Hp / (float)_tagobjs[i].GetComponent<PlayerStates>().MaxHp) * 100;
                _tagobjs[i - 1].GetComponent<PlayerStates>().Hp = 0;
            }
            
        }

        
    }
}
