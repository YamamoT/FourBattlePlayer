using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedMuzzle : MonoBehaviour
{
    // 銃口の座標
    [SerializeField]
    private Transform muzzle;
    private Vector3 muzzlePos;

    // 座標固定
    [SerializeField]
    private bool fixedPosX = true;
    [SerializeField]
    private bool fixedPosY = true;
    [SerializeField]
    private bool fixedPosZ = true;

	// 初期化
	void Start ()
    {
        // 初期位置設定
        transform.position = muzzle.position;
        muzzlePos = transform.position;

	}
	
	// 更新
	void Update ()
    {

        // 座標更新
        if (fixedPosX)
            muzzlePos.x = muzzle.position.x;
        if (fixedPosY)
            muzzlePos.y = muzzle.position.y;
        if (fixedPosZ)
            muzzlePos.z = muzzle.position.z;

        fixedPosZ = false;

        transform.position =new Vector3(muzzle.position.x, muzzle.position.y, 0);
	}

    public void SetFixedPosition(bool isX, bool isY, bool isZ)
    {
        fixedPosX = isX;
        fixedPosY = isY;
        fixedPosZ = isZ;
    }
}
