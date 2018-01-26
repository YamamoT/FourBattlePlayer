using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    
    public GamepadInput.GamepadState keyState; //キー情報
    public GamepadInput.GamepadState Trigger; //キー情報
    Vector2 axis; //スティック情報

    public JudgGround jg;

    Animator animator; //アニメーター

    private Rigidbody rigid;
    private bool ground;

    private Vector3 moveDirection = Vector3.zero;
    private PlayerStates pStates;

    [SerializeField]
    private float _gravity = 300;    

    int playerLayer;
    int slidingFloorLayer;

    // Use this for initialization
    void Start () {
        //アニメーター取得
        animator = GetComponent<Animator>();
        pStates = GetComponent<PlayerStates>();
        rigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {

        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(pStates.ConNum, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, pStates.ConNum, false);
        
        if (jg.flag)
        {
            //// ジャンプ処理
            if (keyState.A && !Trigger.A && axis.y >= -0.5f)
            {
                animator.SetTrigger("jump");
                rigid.AddForce(0, 100f * pStates.JumpPow, 0);
            }
        }

        // ダッシュ状態か否かで速度を変える
        moveDirection.x = axis.x * pStates.Spead;
        
        // 向きの回転
        if (Mathf.Round(axis.x * 10) / 10 < 0) pStates.IsTrun = true;
        else if (Mathf.Round(axis.x * 10) / 10 > 0) pStates.IsTrun = false;

        if (pStates.IsTrun)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 270f, 0), Time.deltaTime * 10);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90f, 0), Time.deltaTime * 10);

        // しゃがんでると移動できないよ
        if (pStates.IsCrouch) moveDirection.x = 0f;
                
        // アニメーター処理
        if (Mathf.Round(axis.x * 10) / 10 == 0)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
        }
        else if (axis.x >= 0.7f || axis.x <= -0.7f)
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
        }
        else if(axis.x < 0.7f || axis.x > -0.7f)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", true);
        }

        //しゃがみ
        if (axis.y < -0.5f && jg.flag)
        {
            if (animator.GetBool("crowch") == false)
            {
                animator.SetTrigger("crowchTrigger");
            }
            animator.SetBool("crowch", true);
            animator.SetBool("walk", false);
            animator.SetBool("run", false);

            pStates.IsCrouch = true;
        }
        else
        {
            animator.SetBool("crowch", false);
            pStates.IsCrouch = false;
        }

        //トリガー処理
        Trigger = keyState;
    }

    public void FixedUpdate()
    {
        rigid.MovePosition(transform.position + moveDirection * Time.deltaTime);
        rigid.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
    }
}
