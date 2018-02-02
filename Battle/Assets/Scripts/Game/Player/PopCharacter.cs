using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopCharacter : MonoBehaviour
{

    // 生成するキャラクター
    [SerializeField]
    private GameObject character;

    // プレイヤーUI
    [SerializeField]
    private GameObject playerUI;

	// Use this for initialization
	void Start ()
    { 
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetCreateCharacter(GameObject obj)
    {
        character = obj;
        playerUI.GetComponent<PlayerGauge>().SetPlayer(character);

        Instantiate(character, transform.position, Quaternion.identity);
    }

    public Transform GetPlayeTransform()
    {
        return character.transform;
    }
}
