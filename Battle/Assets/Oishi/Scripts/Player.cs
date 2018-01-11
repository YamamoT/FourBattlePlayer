using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float speed = 0.5f;
    public float gravity = 2.8f;
    private CharacterController cCon;
    [SerializeField]
    private float jump;
    public float jumpPower;

    // Use this for initialization
    void Start () {
        cCon = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {
        float x = Input.GetAxis("Horizontal");

        Ray ray = new Ray(transform.position, new Vector3(0, -1, 0));
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 1.0f);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.2f))
        {
            if (Input.GetKey(KeyCode.W) && jump <= 0)
            {
                jump = jumpPower;
            }
            if (hit.collider.tag == "MoveObject")
            {
                transform.parent = hit.collider.gameObject.transform;
            }

            if (Input.GetKey(KeyCode.S))
            {
                int sliding_floor_layer = LayerMask.NameToLayer("SlidingFloor");
                if(Physics.Raycast(transform.position, -transform.up, sliding_floor_layer))
                {
                    int player_layer = LayerMask.NameToLayer("Player");
                    Physics.IgnoreLayerCollision(player_layer, sliding_floor_layer);
                }
            }
        }
        else
        {
            transform.parent = null;
            jump -= gravity * Time.deltaTime;
            if (jump <= -gravity) { jump = -gravity/2; }
        }

        

        cCon.Move(new Vector3(x * speed, jump, 0) * Time.deltaTime);
        if(transform.position.y <= -10.0f)
        {
            transform.position = new Vector3(0, 5, 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "MoveObject")
        {
            //Debug.Log("hit");
            transform.parent = other.gameObject.transform;
        }
    }
}
