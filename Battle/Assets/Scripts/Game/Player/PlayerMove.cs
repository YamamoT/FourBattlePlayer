using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    
    public GamepadInput.GamepadState keyState; //キー情報
    public GamepadInput.GamepadState Trigger; //キー情報
    Vector2 axis; //スティック情報

    public JudgGround jg;
    public WallJudg wall;

    Animator animator; //アニメーター

    //プレイヤー着弾エフェクト
    [SerializeField]
    private GameObject hitEffect = null;

    private Rigidbody rigid;
    private bool ground;

    private Vector3 moveDirection = Vector3.zero;
    private PlayerStates pStates;

    [SerializeField]
    private float _gravity = 300;

    int damegeCount = 0;
    int stackDamage = 0;
    float stackTime = 0f;

    Collider[] col;

    private enum pCols
    {
        Stand,
        Crouch,
    }

    // Use this for initialization
    void Start () {
        //アニメーター取得
        animator = this.GetComponent<Animator>();
        pStates = this.GetComponent<PlayerStates>();
        rigid = this.GetComponent<Rigidbody>();
        col = this.GetComponents<Collider>();

        int i = 0;
        foreach(Collider cols in col) col[i++] = cols;
        col[(int)pCols.Stand].enabled = true;
        col[(int)pCols.Crouch].enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        //キー情報取得
        keyState = GamepadInput.GamePad.GetState(pStates.ConNum, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, pStates.ConNum, false);

        // コライダー変更処理
        if (pStates.IsCrouch)
        {
            col[(int)pCols.Stand].enabled = false;
            col[(int)pCols.Crouch].enabled = true;
        }
        else
        {
            col[(int)pCols.Stand].enabled = true;
            col[(int)pCols.Crouch].enabled = false;
        }

        if (jg.flag)
        {
            animator.SetBool("isground", true);
            // ジャンプ処理
            if (keyState.A && !Trigger.A && axis.y >= -0.5f)
            {
                animator.SetTrigger("jump");
                rigid.AddForce(0, 100f * pStates.JumpPow, 0);
            }
        }
        else
        {
            animator.SetBool("isground", false);
        }

        // ダッシュ状態か否かで速度を変える
        moveDirection.x = axis.x * pStates.Spead;

        // 向きの回転
        if (Mathf.Round(axis.x * 10) / 10 < 0) pStates.IsTrun = true;
        else if (Mathf.Round(axis.x * 10) / 10 > 0) pStates.IsTrun = false;

        if (pStates.IsTrun)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 270f, 0), Time.deltaTime * 100);
        else
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90f, 0), Time.deltaTime * 100);

        transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        // しゃがんでると移動できないよ
        if (pStates.IsCrouch || wall.jg == true) moveDirection.x = 0f;
        
        Debug.Log("stackDamage : " + stackDamage);

        // ダメージのスタック処理
        if (stackDamage != 0)
        {
            stackTime += Time.deltaTime;
        }
        else
        {
            stackTime = 0;
        }

        if (stackTime >= 1f) stackDamage = 0;

        if(stackDamage >= pStates.Flinch)
        {
            pStates.IsDamage = true;
            stackDamage = 0;
        }

                
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

        if (pStates.IsDamage && damegeCount == 0)
        {
            damegeCount++;
            animator.SetTrigger("damage");
        }
        else if(!pStates.IsDamage)
        {
            damegeCount = 0;
        }

        //トリガー処理
        Trigger = keyState;
    }

    public void FixedUpdate()
    {
        rigid.MovePosition(transform.position + moveDirection * Time.deltaTime);
        rigid.AddForce(Vector3.down * _gravity, ForceMode.Acceleration);
    }
    private void OnTriggerEnter(Collider col)
    {

        if (pStates.IsDamage) return;

        GameObject bull = col.gameObject;

        if (col.tag == "Bullet")
        {
            stackDamage += bull.GetComponent<Bullet>().GetDamage();
            pStates.Hp -= bull.GetComponent<Bullet>().GetDamage();
        }
        else if(col.tag == "Fist")
        {
            stackDamage += bull.GetComponent<Weapon>().GetDamage();
            pStates.Hp -= bull.GetComponent<Weapon>().GetDamage();

            //パーティクル
            GameObject effect = GameObject.Instantiate(hitEffect) as GameObject;
            effect.transform.position = col.transform.position;
        }
    }
}
