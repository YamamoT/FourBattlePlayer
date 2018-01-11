using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour {
    // Animator コンポーネント
    private Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isAttack01 = "IsAttack01";
    private const string key_isAttack02 = "IsAttack02";
    private const string key_isJump = "IsJump";
    private const string key_isDamage = "IsDamage";
    private const string key_isDead = "IsDead";
    // Use this for initialization
    void Start () {
        // 自分に設定されているAnimatorコンポーネントを習得する
        this.animator = GetComponent<Animator>();
        
        transform.rotation = Quaternion.Euler(0,90,0);
    }
	
	// Update is called once per frame
	void Update () {
        // 移動
        if (Input.GetKey("a"))
        {
            // IdleからRunに遷移する
            this.animator.SetBool(key_isRun, true);
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey("d"))
        {
            // IdleからRunに遷移する
            this.animator.SetBool(key_isRun, true);
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            // RunからIdleに遷移する
            this.animator.SetBool(key_isRun, false);
        }

        // ジャンプ
        //if (Input.GetKey("w"))
        //{
        //    //Jumpに遷移する
        //    this.animator.SetBool(key_isJump, true);
        //}
        //else
        //{
        //    // JumpからIdleに遷移する
        //    this.animator.SetBool(key_isJump, false);
        //}

        // パンチ
        if (Input.GetKey("j"))
        {
            //Jumpに遷移する
            this.animator.SetBool(key_isAttack01, true);
        }
        else
        {
            // JumpからIdleに遷移する
            this.animator.SetBool(key_isAttack01, false);
        }
        // キック
        if (Input.GetKey("k"))
        {
            //Jumpに遷移する
            this.animator.SetBool(key_isAttack02, true);
        }
        else
        {
            // JumpからIdleに遷移する
            this.animator.SetBool(key_isAttack02, false);
        }
    }
}
