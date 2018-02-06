using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] respownPoints;

    [SerializeField]
    private int playerValue;

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private GameObject[] _player;

    [SerializeField]
    private GameObject[] _playerGauge;

    private void Awake()
    {
        playerValue = gManager.instance.GetPlayerValue();

        //if(pop1P)
        //    pop1P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(0));
        //if(pop2P)
        //    pop2P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(1));
        //if(pop3P)
        //    pop3P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(2));
        //if(pop4P)
        //    pop4P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(3));

        for(int i = 0; i < playerValue; i++)
        {
            respownPoints[i].GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(i));
        }

        _camera.GetComponent<ZoomCamera>().SetTargetSize(gManager.instance.GetPlayerValue());

        Debug.Log("キャラクターのフィールド生成とCameraにサイズを渡すことができた");
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < playerValue; i++)
        {
            _player[i].GetComponent<PlayerStates>().PlayerID = i + 1;
        }

        for(int i = 0; i < playerValue; i++)
        {
            _playerGauge[i].SetActive(true);
            _playerGauge[i].GetComponent<PlayerGauge>().SetPlayer(_player[i]);
        }

        Debug.Log("TestManagerのStartが呼ばれた");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
