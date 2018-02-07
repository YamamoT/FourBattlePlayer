using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

public class MultiControllerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject manager;

    // コントローラー番号
    [SerializeField]
    private int controllerNum;
    // コントローラー
    GamepadInput.GamePad.Index controller;

    // キー情報
    public GamepadInput.GamepadState keyState;
    // スティック情報
    Vector2 axis;

    // 各プレイヤーの参加確認
    [SerializeField]
    private bool playerIsJoin = false;

    // 各プレイヤーのプレイキャラクター
    [SerializeField]
    private int playerIsPlayCharacter = 1;
    [SerializeField]
    private int maxCharaValue = 4;

    // 各プレイヤーのキャラクター決定
    [SerializeField]
    private bool playerIsReady = false;

    // キャラセレ移動制限用
    private bool isSelectMoved = false;
    [SerializeField]
    private float moveTime;
    private float c_moveTime;

    // キャラ画像
    [SerializeField]
    private Image[] charaImage;
    // 決定画像
    [SerializeField]
    private Image decideImage;


	// Use this for initialization
	void Start ()
    {
        // コントローラー設定
        ControllerSetUp(controllerNum);
        // 移動時間初期化
        c_moveTime = moveTime;

        for (int i = 0; i < maxCharaValue; i++)
        {
            if (playerIsPlayCharacter - 1 == i)
                charaImage[i].color = new Color(1, 1, 1, 1);
            else
                charaImage[i].color = new Color(0, 0, 0, 0);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        // キー・スティック情報取得
        keyState = GamepadInput.GamePad.GetState(controller, false);
        axis = GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, controller, false);

        // キャラクターセレクトがもう一度呼べるようになるまで
        if(isSelectMoved)
        {
            c_moveTime -= Time.deltaTime;

            if(c_moveTime <= 0.0f)
            {
                isSelectMoved = false;
                c_moveTime = moveTime;
            }
        }

        // 参加している時
        if (playerIsJoin)
        {
            // キャラクター決定がまだの時
            if (!playerIsReady)
            {
                // キャラクターセレクト移動
                if (axis.x >= 0.7f)
                    CharacterSelect(false);
                else if (axis.x <= -0.7f)
                    CharacterSelect(true);

                // キャラクター決定
                if (keyState.X)
                {
                    decideImage.color = new Color(1,1,1, 1);
                    playerIsReady = true;
                    manager.GetComponent<CreateManager>().EntryCharacter(controllerNum, playerIsPlayCharacter);
                    manager.GetComponent<CreateManager>().CharacterIsDecide();
                    Debug.Log(controllerNum + "Pが " + playerIsPlayCharacter + "を選びました");
                }
                    
            }
            // キャラクター決定が済んでいる時
            else
            {
                // キャンセル
                if (keyState.A)
                {
                    decideImage.color = new Color(0, 0, 0, 0);
                    playerIsReady = false;
                    manager.GetComponent<CreateManager>().CharacterCancel();
                    Debug.Log("キャンセルされました");
                }

                if(keyState.Start)
                {
                    Debug.Log("ゲームを開始します");
                    manager.GetComponent<CreateManager>().GameStart();
                }
            }
        }
        else
        {
            // 決定ボタン
            if (keyState.Start)
            {
                playerIsJoin = true;
                manager.GetComponent<CreateManager>().PlayerJoined();
                Debug.Log("参加しました : " + controllerNum);
            }
        }
    }

    /// <summary>
    /// コントローラー設定
    /// </summary>
    /// <param name="num">コントローラー番号</param>
    private void ControllerSetUp(int num)
    {
        switch(num)
        {
            case 1:
                controller = GamePad.Index.One;
                break;
            case 2:
                controller = GamePad.Index.Two;
                break;
            case 3:
                controller = GamePad.Index.Three;
                break;
            case 4:
                controller = GamePad.Index.Four;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// キャラクターセレクト
    /// </summary>
    /// <param name="isLR">true : 左移動　false : 右移動</param>
    private void CharacterSelect(bool isLR)
    {
        if (isSelectMoved) return;

        isSelectMoved = true;

        if (isLR)
        {
            playerIsPlayCharacter--;

            if (playerIsPlayCharacter == 0)
                playerIsPlayCharacter = 4;
        }
        else
        {
            playerIsPlayCharacter++;

            if (playerIsPlayCharacter == 5)
                playerIsPlayCharacter = 1;
        }

        for(int i = 0; i < maxCharaValue; i++)
        {
            if (playerIsPlayCharacter - 1 == i)
                charaImage[i].color = new Color(1, 1, 1, 1);
            else
                charaImage[i].color = new Color(0, 0, 0, 0);
        }

        Debug.Log(playerIsPlayCharacter);
    }
        
    public void PlayerIsJoin()
    {
        playerIsJoin = true;

        Debug.Log("参加しました : " + controllerNum);
    }

    /// <summary>
    /// キャラクターを決定します
    /// </summary>
    public void PlayerIsCharacterDecide()
    {
        playerIsReady = true;

        Debug.Log(controllerNum + "Pが " + playerIsPlayCharacter + "を選びました");
    }


}
