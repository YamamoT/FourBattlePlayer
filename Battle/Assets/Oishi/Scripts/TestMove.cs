﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

    //アニメーター
    Animator animator;

    private CharacterController charaCon;

    private Transform charaRay;
    private float charaRayRange = 1;
    private Rigidbody rigid;
    private Vector3 velocity;
    private bool ground;

    private Vector3 moveDirection = Vector3.zero;
    private PlayerStates pStates;

    [SerializeField]
    [Range(0.01f, 0.1f)]
    private float _dushIntervalTime; // 走り判定をとるまでの時間
    private float dushTime;

    [SerializeField]
    [Range(1f, 20f)]
    private float _gravity = 15f;

    private int count = 0;
    private float dushDir = 0f;

    public JudgGround jg;
    public float power = 100.0f;

    // 参考元 https://gametukurikata.com/program/run

    private bool push = false;           //　最初に移動ボタンを押したかどうか
    private float nextButtonDownTime;    //　次に移動ボタンが押されるまでの時間
    private float nowTime = 0f;			//　最初に移動ボタンが押されてからの経過時間

    int playerLayer;
    int slidingFloorLayer;

    // Use this for initialization
    void Start()
    {
        //アニメーター取得
        animator = GetComponent<Animator>();
        pStates = GetComponent<PlayerStates>();
        charaCon = GetComponent<CharacterController>();
        rigid = GetComponent<Rigidbody>();
        velocity = Vector3.zero;
        dushTime = 0f;

        playerLayer = LayerMask.NameToLayer("Player");
        slidingFloorLayer = LayerMask.NameToLayer("Sliding");
    }

    // Update is called once per frame
    void Update()
    {


        float inputAxis = Input.GetAxis("Horizontal");
        float inputAxisRaw = Input.GetAxisRaw("Horizontal");

        if (jg.flag)
        {
            //moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") >= 0f)
            {
                //moveDirection.y = pStates.JumpPow;
                rigid.AddForce(0, power, 0);
                animator.SetTrigger("jump");
            }
            // コントローラー用
            GamePadDush(inputAxis, inputAxisRaw);
            // キーボード用
            KeyboardDush(inputAxisRaw);
        }
        else
        {
            //moveDirection.y -= _gravity * Time.deltaTime;
        }

        // ダッシュ状態か否かで速度を変える
        //if (!pStates.IsDash)
        //    moveDirection.x = inputAxis * pStates.WalkSpd;
        //else
        moveDirection.x = inputAxis * pStates.Spead;

        // 向きの回転
        if (Mathf.Round(inputAxis * 10) / 10 < 0) pStates.IsTrun = true;
        else if (Mathf.Round(inputAxis * 10) / 10 > 0) pStates.IsTrun = false;

        if (pStates.IsTrun)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 270f, 0), Time.deltaTime * 10);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90f, 0), Time.deltaTime * 10);

        // しゃがんでると移動できないよ
        if (animator.GetBool("crowch") || Input.GetAxisRaw("Vertical") < -0.5f) moveDirection.x = 0f;

        // しゃがんだ時にすり抜け床なら降りる
        if (Input.GetAxisRaw("Vertical") < -0.9)
        {
            if (Physics.Raycast(transform.position, -transform.up, LayerMask.NameToLayer("Sliding")))
            {
                Physics.IgnoreLayerCollision(playerLayer, LayerMask.NameToLayer("Sliding"));
            }
        }


        // アニメーター処理
        if (Mathf.Round(inputAxis * 10) / 10 == 0)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
        }
        else if (inputAxis >= 0.7f || inputAxis <= -0.7f)
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
        }
        else if (inputAxis < 0.7f || inputAxis > -0.7f)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", true);
        }

        //しゃがみ
        if (Input.GetAxisRaw("Vertical") < -0.5f && pStates.IsGround)
        {
            animator.SetBool("crowch", true);
        }
        else
        {
            animator.SetBool("crowch", false);
        }
    }

    public void FixedUpdate()
    {
        rigid.MovePosition(transform.position + moveDirection * Time.deltaTime);
    }

    //void OnCollisionStay(Collision col)
    //{
    //    if (Physics.Linecast(transform.position, -transform.up, LayerMask.GetMask("Stage", "Sliding")))
    //    {
    //        pStates.IsGround = true;
    //    }
    //}
    //private void OnCollisionExit(Collision col)
    //{
    //    pStates.IsGround = false;
    //}

    void GamePadDush(float Axis, float AxisRaw)
    {
        if (AxisRaw == 0) { pStates.IsDash = false; count = 0; }
        if (dushTime <= 0) dushTime = 0;

        // 移動入力されたらカウントを増やして時間を設定する
        if (Axis != 0 && count == 0) { dushTime = _dushIntervalTime; count++; }

        if (dushTime != 0)
        {
            dushTime -= Time.deltaTime;
            if (Axis == 1 || Axis == -1) pStates.IsDash = true;
        }
    }

    void KeyboardDush(float AxisRaw)
    {
        if (!pStates.IsDash)
        {

            //　移動キーを押した
            if ((Input.GetButtonDown("Horizontal")))
            {
                Debug.Log("登録");
                //　最初に1回押していない時は押した事にする
                if (!push)
                {
                    push = true;
                    //　最初に移動キーを押した時にその方向ベクトルを取得
                    dushDir = AxisRaw;
                    nowTime = 0f;
                    //　2回目のボタンだったら1→２までの制限時間内だったら走る

                }
                else
                {
                    //　2回目に移動キーを押した時の方向ベクトルを取得
                    var nowDirection = AxisRaw;

                    //　押した方向がリミットの角度を越えていない　かつ　制限時間内に移動キーが押されていれば走る
                    if (nowDirection == dushDir && nowTime <= nextButtonDownTime)
                    {
                        pStates.IsDash = true;
                    }
                }
            }
            //　走っている時にキーを押すのをやめたら走るのをやめる
        }
        else
        {
            if (!Input.GetButton("Horizontal"))
            {
                //_dush = false;
                push = false;
            }
        }
        //　最初の移動キーを押していれば時間計測
        if (push)
        {
            //　時間計測
            nowTime += Time.deltaTime;

            if (nowTime > nextButtonDownTime)
            {
                push = false;
            }
        }
    }
}