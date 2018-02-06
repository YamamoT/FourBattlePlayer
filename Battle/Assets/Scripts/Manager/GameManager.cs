using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public Timer _timer;
    public ZoomCamera _zoomCamera;
    int count = 0;

    GameObject[] _tagobjs;

    float _maxHp = 0f;
    float _hpRate = 0f;

    // 生き残ったプレイヤーID保管所
    private int _winPlayerid = 0;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (_zoomCamera.GetIsFinished() == false) return;

        if (count == 0)
        {
            _timer.TimerSwitch = true;
            count++;
        }

        if (GameObject.FindGameObjectsWithTag("Player") == null)
        {
            _winPlayerid = 0;
            return;
        }

        _tagobjs = GameObject.FindGameObjectsWithTag("Player");

        if (_tagobjs.Length == 1) _winPlayerid = _tagobjs[0].GetComponent<PlayerStates>().PlayerID;

        if (_timer.GetIsFinished() == false) return;

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

    public int WinPlayerId
    {
        get { return _winPlayerid; }
        set { _winPlayerid = value; }
    }
}
