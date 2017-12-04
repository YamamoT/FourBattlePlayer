using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private CharacterController charaCon;
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _walkSpeed;

    [SerializeField]
    private bool _dush = false;

    // Use this for initialization
    void Start () {
        charaCon = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {


        //if(Input.GetAxis("Horizontal") == 0f)

        if (_dush == false)
        {
            moveDirection.x = Input.GetAxis("Horizontal") * _walkSpeed;
        }
        else
        {
            moveDirection.x = Input.GetAxis("Horizontal") * _runSpeed;
        }

        // しゃがんだ時に武器があれば拾う
        //if(Input.GetAxis("Vertical") < 0)

        charaCon.Move(moveDirection * Time.deltaTime);
       
    }
}
