using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceCamera : MonoBehaviour
{
    // 到着予定時間
    [SerializeField, Range(0, 10)]
    private float time = 1;

    [SerializeField]
    private Transform[] targets;

    // 開始地点
    private Vector3 startPosition;
    // 終了地点
    private Vector3 endPosition;

    // 開始時間
    private float startTime;

    // 順番
    private int targetNumber = 0;

    // 終了したか
    private bool isFinished = false;

    // 停止時間
    [SerializeField]
    private float stopTime = 0;
    private float stop;

	/// <summary>
    /// 初期化
    /// </summary>
	void OnEnable ()
    {
		if(time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        stop = stopTime;

        startTime = Time.timeSinceLevelLoad;
        startPosition = targets[targetNumber].position;
        endPosition = targets[targetNumber + 1].position;
	}
	
	/// <summary>
    /// 更新
    /// </summary>
	void Update ()
    {
        if (!isFinished)
            PerformanceMove();
	}

    void PerformanceMove()
    {
        // 経過時間
        float elapsedTime = Time.timeSinceLevelLoad - startTime;

        if (elapsedTime > time)
        {
            // 停止時間
            stop -= Time.deltaTime;

            if(stop < 0)
            {
                if (targetNumber != targets.Length - 2)
                {
                    //isFinished = true;

                    // 開始時間の初期化
                    startTime = Time.timeSinceLevelLoad;

                    // ターゲットの順番変更
                    targetNumber++;

                    // 開始・終了地点の再設定
                    startPosition = targets[targetNumber].position;
                    endPosition = targets[targetNumber + 1].position;
                }

                stop = stopTime;
            }
        }

        float rate = elapsedTime / time;

        // 移動
        transform.position = Vector3.Lerp(startPosition, endPosition, rate);
    }
}
