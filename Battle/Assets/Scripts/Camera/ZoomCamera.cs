using System.Collections;
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

    // 対象
    private Vector3 target1 = new Vector3(0 ,0, 0);
    private Vector3 target2 = new Vector3(0, 0, 0);

    // 
    private int target1CntX = 0, target1CntY = 0;
    private int target2CntX = 0, target2CntY = 0;

	// Use this for initialization
	void Awake ()
    {
        screenAspect = (float)Screen.height / Screen.width;
        camera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateCameraPosition();
        UpdateOrthographicSize();
	}

    /// <summary>
    /// カメラの位置更新
    /// </summary>
    void UpdateCameraPosition()
    {

        for (int i = 0; i < target.Length; i++)
        {
            // target1の座標設定
            if (target1.x < target[i].position.x)
            {
                target1CntX = i;
            }
            if(target1.y < target[i].position.y)
            {
                target1CntY = i;
            }

            // target2の座標設定
            if(target2.x > target[i].position.x)
            {
                target2CntX = i;
            }
            if(target2.y > target[i].position.y)
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

        Vector3 center = Vector3.Lerp(target1, target2, 0.5f);

        float correction = 0;

        if (Mathf.Abs(center.x) >= Mathf.Abs(center.y))
        {
            correction = Mathf.Abs(center.x);
        }
        else if (Mathf.Abs(center.y) >= Mathf.Abs(center.x))
        {
            correction = Mathf.Abs(center.y) * 1.3f;
        }

        transform.position = center + Vector3.forward * (-10 - correction);
    }

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

    Vector3 AbsPositionDiff(Vector3 target1, Vector3 target2)
    {
        Vector3 targetDiff = target1 - target2;
        return new Vector3(Mathf.Abs(targetDiff.x), Mathf.Abs(targetDiff.y));
    }
}
