using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class MultiControllerManager : MonoBehaviour
{
    [SerializeField]
    private int gamepadMaxSize = 4;

    private GamepadState gamePad1;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        // 各ゲームパッドの状態取得
        gamePad1 = GamepadInput.GamePad.GetState(GamePad.Index.One);

        if(GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.One))
            Debug.Log("Startボタンが押された : コントローラー1");

        if(GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Two))
            Debug.Log("Startボタンが押された : コントローラー2");
    }
}
