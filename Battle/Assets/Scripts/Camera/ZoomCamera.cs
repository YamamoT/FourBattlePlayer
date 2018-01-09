﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    // ターゲット
    [SerializeField]
    Transform[] target;
    // オフセット
    [SerializeField]
    Vector2 offset = new Vector2(1, 1);

    // アスペクト比
    private float screenAspect = 0;
    // カメラ
    private Camera camera = null;
    // オブジェクトからの距離
    [SerializeField]
    private float distance;

    // 最端対象
    private Vector3 target1 = new Vector3(0 ,0, 0);
    private Vector3 target2 = new Vector3(0, 0, 0);

    // 最端対象の番号格納用
    private int target1CntX = 0, target1CntY = 0;
    private int target2CntX = 0, target2CntY = 0;

    // 中心位置
    private Vector3 center;
    private Vector3 c_center;

    // 演出 -----------------------------------------------------------------

    // 到着予定時間
    [SerializeField, Range(0, 10)]
    private float time = 1;

    // 開始地点
    private Vector3 startPosition;
    // 終了地点
    private Vector3 endPosition;

    // 開始時間
    private float startTime;

    // 順番
    private int order = 0;

    // 終了したか
    private bool isFinished = false;

    // 停止時間
    [SerializeField]
    private float stopTime = 0;
    private float c_stopTime;

	/// <summary>
    /// 初期化
    /// </summary>
	void Awake ()
    {
        // アスペクト比を設定
        screenAspect = (float)Screen.height / Screen.width;
        // カメラを取得
        camera = GetComponent<Camera>();

        // 位置初期化
        center = new Vector3(0, 0, 0);

        c_stopTime = stopTime;

        if(time <= 0)
        {
            transform.position = endPosition;
            enabled = false;
            return;
        }

        c_stopTime = stopTime;

        startTime = Time.timeSinceLevelLoad;

        startPosition = new Vector3(target[order].position.x, target[order].position.y, -5);
        endPosition = new Vector3(target[order + 1].position.x, target[order + 1].position.y, -5);
	}
	
	/// <summary>
    /// 更新
    /// </summary>
	void Update ()
    {
        // 中心位置の計算
        CenterCalc();

        if(!isFinished)
        {
            CameraPerformance();
        }
        else
        {
            // カメラの位置更新
            UpdateCameraPosition();

            // カメラの描画範囲更新
            UpdateOrthographicSize();
        }
    }

    /// <summary>
    ///  カメラ演出
    /// </summary>
    void CameraPerformance()
    {
        // 経過時間
        float elapsedTime = Time.timeSinceLevelLoad - startTime;

        Debug.Log("経過時間 : " + elapsedTime);
        Debug.Log("到着予定時間 : " + time);

        // 経過時間が到着予定時間を越えたら
        if (elapsedTime > time)
        {
            // 停止時間が0になるまで時間を減らす
            c_stopTime -= Time.deltaTime;

            // 停止時間が0になったら
            if (c_stopTime < 0)
            {
                // 各キャラを見せるカメラワークを終了させる
                if (order == target.Length - 1)
                    isFinished = true;

                // カメラワークが終了するまで処理を続ける
                if (order < target.Length - 2)
                {
                    // 開始時間の初期化
                    startTime = Time.timeSinceLevelLoad;

                    // ターゲットの順番変更
                    order++;

                    // 開始地点と終了地点の変更
                    startPosition = new Vector3(target[order].position.x, target[order].position.y, -5);
                    endPosition = new Vector3(target[order + 1].position.x, target[order + 1].position.y, -5);

                }
                // 各キャラを見せるカメラワークの移動処理をこれで最後にする
                else if (order == target.Length - 2)
                {
                    startTime = Time.timeSinceLevelLoad;
                  
                    startPosition = new Vector3(target[order].position.x, target[order].position.y, -5);
                    endPosition = c_center;

                    order++;
                }

                c_stopTime = stopTime;
            }
        }

        float rate = elapsedTime / time;

        // 移動
        transform.position = Vector3.Lerp(startPosition, endPosition, rate);
    }

    /// <summary>
    /// 中心位置を計算
    /// </summary>
    void CenterCalc()
    {
        for (int i = 0; i < target.Length; i++)
        {
            // target1の座標設定
            if (target1.x < target[i].position.x)
            {
                target1CntX = i;
            }
            if (target1.y < target[i].position.y)
            {
                target1CntY = i;
            }

            // target2の座標設定
            if (target2.x > target[i].position.x)
            {
                target2CntX = i;
            }
            if (target2.y > target[i].position.y)
            {
                target2CntY = i;
            }
        }

        // x 座標設定
        target1.x = target[target1CntX].position.x;
        target1.y = target[target1CntY].position.y;

        // y 座標設定
        target2.x = target[target2CntX].position.x;
        target2.y = target[target2CntY].position.y;

        // カメラに設定する中心位置を設定
        center = Vector3.Lerp(target1, target2, 0.5f);

        // パースペクティブ用 (z 軸補正)
        float correction = 0;

        // 中心位置の x と y の絶対値を比較し、大きい値を補正値に設定
        if (Mathf.Abs(center.x) >= Mathf.Abs(center.y))
        {
            correction = Mathf.Abs(center.x);
        }
        else if (Mathf.Abs(center.y) >= Mathf.Abs(center.x))
        {
            correction = Mathf.Abs(center.y) * 1.3f;
        }

        c_center = center + Vector3.forward * (-distance - correction);
    }

    /// <summary>
    /// カメラの位置更新
    /// </summary>
    void UpdateCameraPosition()
    {
        // カメラの位置を設定
        transform.position = c_center;
    }

    /// <summary>
    /// // カメラの描画範囲更新
    /// </summary>
    void UpdateOrthographicSize()
    {
        // 2点間のベクトルを取得
        Vector3 targetVector = AbsPositionDiff(target1, target2) + (Vector3)offset;

        // アスペクト比が縦長なら y の半分、横長なら x とアスペクト比でカメラのサイズを設定
        float targetAspect = targetVector.y / targetVector.x;

        float targetOrthographicSize = 0;

        if(screenAspect < targetAspect)
        {
            targetOrthographicSize = targetVector.y * 0.5f;
        }
        else
        {
            targetOrthographicSize = targetVector.x * (1 / camera.aspect) * 0.5f;
        }

        camera.orthographicSize = targetOrthographicSize;
    }

    /// <summary>
    ///  2点間の距離の絶対値を返す
    /// </summary>
    /// <param name="target1">座標1</param>
    /// <param name="target2">座標2</param>
    /// <returns>距離の絶対値</returns>
    Vector3 AbsPositionDiff(Vector3 target1, Vector3 target2)
    {
        Vector3 targetDiff = target1 - target2;
        return new Vector3(Mathf.Abs(targetDiff.x), Mathf.Abs(targetDiff.y));
    }
}
