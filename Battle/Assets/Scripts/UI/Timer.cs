using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // 分 (設定用) ----------------------------------------------------------
    [SerializeField]
    private int minutes;
    // 分 (変更用)
    private int minutesTime;
    // 分 (テキスト)
    [SerializeField]
    private Text minuteText;

    // 秒 (設定用) ----------------------------------------------------------
    [SerializeField]
    private int seconds;
    // 秒 (変更用)
    private int secondsTime;
    // 秒 (テキスト)
    [SerializeField]
    private Text secondsText;

    // ミリ秒 ---------------------------------------------------------------
    private float milliSeconds;
    [SerializeField]
    // ミリ秒 (テキスト)
    private Text milliSecondsText;

    // タイマー作動中
    private bool isTimerProgress = false;
    // 終了したか
    private bool isTimerFinished = false;

	/// <summary>
    /// 初期化
    /// </summary>
	void Start ()
    {
        // テキスト取得
        minuteText = minuteText.gameObject.GetComponent<Text>();
        secondsText = secondsText.gameObject.GetComponent<Text>();
        milliSecondsText = milliSecondsText.gameObject.GetComponent<Text>();

        // テキスト初期化
        minuteText.text = "00";
        secondsText.text = "00";
        milliSecondsText.text = "00";

        // 値初期化
        minutesTime = minutes;
        secondsTime = seconds;
        milliSeconds = 0;

    }
	
	/// <summary>
    /// 更新
    /// </summary>
	void Update ()
    {
        // デバッグ用
        if (Input.GetKeyDown(KeyCode.A))
            isTimerProgress = true;
        if (Input.GetKeyDown(KeyCode.R))
            TimerReset();

        // テキスト更新
        minuteText.text = minutesTime.ToString("D2");
        secondsText.text = secondsTime.ToString("D2");
        milliSecondsText.text = ((int)milliSeconds).ToString("00");

        // 作動中
        if(isTimerProgress)
            CountDown();
    }

    /// <summary>
    /// 制限時間
    /// </summary>
    private void CountDown()
    {
        // ミリ秒
        if (milliSeconds > 0)
            milliSeconds--;
        else if(minutesTime == 0 && secondsTime == 0 && milliSeconds == 0)
        {
            milliSeconds = 0;
            isTimerFinished = true;
            isTimerProgress = false;
        }
                
            

        // 60ミリ秒経ったら値をリセットして、1秒経たせる
        if(milliSeconds <= 0 && secondsTime != 0)
        {
            milliSeconds = 60;
            secondsTime--;
        }

        // 60秒経ったら値をリセットして、1分経たせる
        if(secondsTime <= 0 && minutesTime != 0)
        {
            secondsTime = 60;
            minutesTime--;
        }
    }

    /// <summary>
    /// 終了したか
    /// </summary>
    /// <returns>タイマー終了</returns>
    public bool GetIsFinished()
    {
        return isTimerFinished;
    }

    /// <summary>
    /// タイマー開始＆停止
    /// </summary>
    public void TimerSwitch()
    {
        isTimerProgress = !isTimerProgress;
    }

    /// <summary>
    /// タイマー設定リセット
    /// </summary>
    public void TimerReset()
    {
        isTimerProgress = false;
        isTimerFinished = false;
        minutesTime = minutes;
        secondsTime = seconds;
        milliSeconds = 60;
    }
}
