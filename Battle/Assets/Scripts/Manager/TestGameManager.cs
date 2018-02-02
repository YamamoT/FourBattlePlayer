using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject pop1P;
    [SerializeField]
    private GameObject pop2P;
    [SerializeField]
    private GameObject pop3P;
    [SerializeField]
    private GameObject pop4P;

    [SerializeField]
    private Camera _camera;

    GameObject[] _player;

    private void Awake()
    {
        if(pop1P)
            pop1P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(0));
        if(pop2P)
            pop2P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(1));
        if(pop3P)
            pop3P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(2));
        if(pop4P)
            pop4P.GetComponent<PopCharacter>().SetCreateCharacter(gManager.instance.GetPlayCharacter(3));

        //if (_camera)
        //{
        //    _camera.GetComponent<ZoomCamera>().SetTargetSize(gManager.instance.GetPlayerValue());
        //    _camera.GetComponent<ZoomCamera>().SetTarget(0, pop1P.GetComponent<PopCharacter>().GetPlayeTransform());
        //    _camera.GetComponent<ZoomCamera>().SetTarget(1, pop2P.GetComponent<PopCharacter>().GetPlayeTransform());
        //    _camera.GetComponent<ZoomCamera>().SetTarget(2, pop3P.GetComponent<PopCharacter>().GetPlayeTransform());
        //    _camera.GetComponent<ZoomCamera>().SetTarget(3, pop4P.GetComponent<PopCharacter>().GetPlayeTransform());

        //}

        //_player = GameObject.FindGameObjectsWithTag("Player");

        //for(int i = 0; i < _player.Length; i++)
        //{
        //    _camera.GetComponent<ZoomCamera>().SetTarget(i, _player[i].transform);
        //}
            

    }

    // Update is called once per frame
    void Update () {
		
	}
}
