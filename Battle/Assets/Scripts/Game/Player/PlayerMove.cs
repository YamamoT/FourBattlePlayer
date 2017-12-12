using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    //アニメーター
    Animator animator;
    
    private CharacterController charaCon;
    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private float _runSpeed;
    [SerializeField]
    private float _walkSpeed;
    [SerializeField][Range(0.01f, 0.1f)]
    private float _dushIntervalTime; // 走り判定をとるまでの時間
    private float dushTime;
    [SerializeField]
    private bool _isDush = false;

    [SerializeField][Range(1f, 10f)]
    private float _jumpPower;
    [SerializeField][Range(1f, 20f)]
    private float _gravity = 15f;
    
    private int count = 0;
    private float dushDir = 0f;

    private bool _isJump = false;

    // 参考元 https://gametukurikata.com/program/run

    private bool push = false;           //　最初に移動ボタンを押したかどうか
    private float nextButtonDownTime;    //　次に移動ボタンが押されるまでの時間
    private float nowTime = 0f;			//　最初に移動ボタンが押されてからの経過時間

    // Use this for initialization
    void Start () {
        //アニメーター取得
        animator = GetComponent<Animator>();

        charaCon = GetComponent<CharacterController>();
        dushTime = 0f;
	}
	
	// Update is called once per frame
	void Update () {

        
        float inputAxis = Input.GetAxis("Horizontal");
        float inputAxisRaw = Input.GetAxisRaw("Horizontal");

        if(!_isJump)
        {
            // コントローラー用
            GamePadDush(inputAxis, inputAxisRaw);
            // キーボード用
            KeyboardDush(inputAxisRaw);
        }
       

        // ダッシュ状態か否かで速度を変える
        if (!_isDush)
        {
            moveDirection.x = inputAxis * _walkSpeed;
        }
        else
        {
            moveDirection.x = inputAxis * _runSpeed;
        }

        // アニメーター処理
        if (Mathf.Round(inputAxis * 10) / 10 == 0)
        {
            animator.SetBool("run", false);
            animator.SetBool("walk", false);
        }
        else if ((inputAxis == 1 || inputAxis == -1) && _isDush)
        {
            animator.SetBool("run", true);
        }
        else if(inputAxis > 0 && inputAxis < 1 || inputAxis < 0 && inputAxis > -1)
        {
            animator.SetBool("walk", true);
        }

        if(animator.GetBool("run") == true) animator.SetBool("walk", false);


        // ジャンプ処理
        if (charaCon.isGrounded)
        {
            _isJump = false;
            if (Input.GetButtonDown("Jump")) moveDirection.y = _jumpPower;
        }
        else
        {
            _isJump = true;
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                moveDirection.y -= (_gravity * 4) * Time.deltaTime;
            }
            else
            {
                moveDirection.y -= _gravity * Time.deltaTime;
            }
        }

        // 移動するよ
        charaCon.Move(moveDirection * Time.deltaTime);

        // しゃがんだ時にすり抜け床なら降りる
        if (Input.GetAxisRaw("Vertical") < -0.9 && !_isJump)
        {
            int slidingFloorLayer = LayerMask.NameToLayer("Sliding");
            if (Physics.Raycast(transform.position, -transform.up, slidingFloorLayer))
            {
                int playerLayer = LayerMask.NameToLayer("Player");
                Physics.IgnoreLayerCollision(playerLayer, slidingFloorLayer);
            }
        }

    }

    public bool CheckGrounded()
    {
        if (charaCon.isGrounded) { return true; }

        Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);

        float tolerance = 0.3f;

        return Physics.Raycast(ray, tolerance);
    }

    void GamePadDush(float Axis,float AxisRaw)
    {
        if (AxisRaw == 0) { _isDush = false; count = 0; }
        if (dushTime <= 0) dushTime = 0;

        // 移動入力されたらカウントを増やして時間を設定する
        if (Axis != 0 && count == 0) { dushTime = _dushIntervalTime; count++; }

        if (dushTime != 0)
        {
            dushTime -= Time.deltaTime;
            if (Axis == 1 || Axis == -1) _isDush = true;
        }
    }

    void KeyboardDush(float AxisRaw)
    {
        if (!_isDush)
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
                        _isDush = true;
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
